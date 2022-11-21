using UnityEngine;
using UnityEngine.UI;
using Lnco.Unity.Module.Storage;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace Group8.TrashDash.Inventory
{
    using Group8.TrashDash.Item.Trash;
    using Group8.TrashDash.Module.Detector;
    using Player.Controller;
    /// <summary>
    /// Example Usage Of Module
    /// </summary>
    public sealed class InventoryHandler : MonoBehaviour
    {
        private PlayerAction playerControls;

        [SerializeField]
        private Toggle _inventoryToggle = default;

        [SerializeField]
        private Inventory<TrashContentInfo> _inventory = default;

        [SerializeField]
        private InventoryLayoutGroupController _inventoryLayoutController = default;

        private void Awake()
        {
            _inventory.Init(new List<TrashContentInfo>());
            _inventoryLayoutController.InitInventoryLayout(_inventory.MaxCapacity);
            _inventoryToggle.onValueChanged.AddListener((value) =>
            {
                if (value)
                {
                    _inventoryLayoutController.TryRefreshContent(_inventory.GetItems());
                    InputManager.ToggleActionMap(InputManager.playerAction.Panel);
                }
                else
                    InputManager.ToggleActionMap(InputManager.playerAction.Gameplay);
                _inventoryLayoutController.SetActiveLayout(value);
            });
        }

        private void Start()
        {
            playerControls = InputManager.playerAction;
            RegisterInputCallback();
        }

        private void OnEnable()
        {
            RegisterInputCallback();
        }

        private void OnDisable()
        {
            UnregisterInputCallback();
        }

        public bool StoreItem(TrashContentInfo trashContentInfo)
        {
            if (!_inventory.TryAdd(trashContentInfo))
            {
                Debug.Log("Inventory Have Max Capacity");
                return false;
            }

            _inventoryLayoutController.TryRefreshContent(_inventory.GetItems());
            return true;
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
            _inventoryToggle.isOn = true;
        }
        private void OnInventoryPanel(InputAction.CallbackContext context)
        {
            _inventoryToggle.isOn = false;
        }
        #endregion
        }
}