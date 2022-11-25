using UnityEngine;
using Group8.TrashDash.Level;
using UnityEngine.InputSystem;
using Lnco.Unity.Module.Storage;
using System.Collections.Generic;

namespace Group8.TrashDash.Inventory
{
    public sealed class InventoryHandler : MonoBehaviour
    {
        private PlayerAction playerControls;

        [SerializeField]
        private GameObject _inventoryObj = default;

        [SerializeField]
        private Inventory<TrashContentInfo> _inventory = default;

        [SerializeField]
        private InventoryLayoutGroupController _inventoryLayoutGroupController = default;

        private void Awake()
        {
            _inventory.Init(new List<TrashContentInfo>());
            _inventoryLayoutGroupController.InitLayout(_inventory.MaxCapacity);
        }

        private void OnEnable()
        {
            RegisterInputCallback();
        }

        private void Start()
        {
            playerControls = InputManager.playerAction;
            RegisterInputCallback();
        }

        private void OnDisable()
        {
            UnregisterInputCallback();
        }

        public bool TryAddItem(TrashContentInfo trashContentInfo)
        {
            if (!_inventory.TryAdd(trashContentInfo)) return default;
            var index = _inventory.ItemCount() - 1;
            _inventoryLayoutGroupController.TryUpdateContent(trashContentInfo, index);
            return true;
        }

        public void RemoveItem(TrashContentInfo trashContentInfo, InventoryLayoutGroupItem inventoryLayoutGroupItem)
        {
            if (!_inventory.TryRemove(trashContentInfo)) return;
            _inventoryLayoutGroupController.TryUpdateContent(new(), inventoryLayoutGroupItem);
        }

        public void SetActiveInventory(bool value)
        {
            if (!value) 
                _inventoryLayoutGroupController.ResetItems(new List<TrashContentInfo>(_inventory.GetItems()));
            _inventoryObj.SetActive(value);
            InputManager.ToggleActionMap(value ? InputManager.playerAction.Panel : InputManager.playerAction.Gameplay);
        }

        #region Callbacks
        private void RegisterInputCallback()
        {
            if (playerControls == null) return;
            playerControls.Gameplay.Inventory.performed += OnInventory;
            playerControls.Panel.Cancel.performed += OnInventoryPanel;
        }

        private void UnregisterInputCallback()
        {
            if (playerControls == null) return;
            playerControls.Gameplay.Inventory.performed -= OnInventory;
            playerControls.Panel.Cancel.performed -= OnInventoryPanel;
        }

        private void OnInventory(InputAction.CallbackContext context)
        {
            SetActiveInventory(true);
        }
        private void OnInventoryPanel(InputAction.CallbackContext context)
        {
            SetActiveInventory(default);
        }
        #endregion
    }
}