using UnityEngine;
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
        private InventoryLayoutGroupController _inventoryLayoutController = default;

        private void Awake()
        {
            _inventory.Init(new List<TrashContentInfo>());
            _inventoryLayoutController.InitLayout(_inventory.MaxCapacity);
        }

        private void OnEnable()
        {
            RegisterInputCallback();
        }

        private void Start()
        {
            playerControls = InputManager.playerAction;
        }

        private void OnDisable()
        {
            UnregisterInputCallback();
        }

        public void AddItem(TrashContentInfo trashContentInfo)
        {
            if (!_inventory.TryAdd(trashContentInfo)) return;
            _inventoryLayoutController.TryRefreshContent(_inventory.GetItems());
        }

        public void RemoveItem(TrashContentInfo trashContentInfo, InventoryLayoutGroupItem inventoryLayoutGroupItem)
        {
            if (!_inventory.TryRemove(trashContentInfo)) return;
            _inventoryLayoutController.TryUpdateContent(default, inventoryLayoutGroupItem);
        }

        public void SetActiveInventory(bool value)
        {
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