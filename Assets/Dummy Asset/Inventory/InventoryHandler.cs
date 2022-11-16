using UnityEngine;
using UnityEngine.UI;
using Lnco.Unity.Module.Storage;
using System.Collections.Generic;

namespace Group8.TrashDash.Inventory
{
    /// <summary>
    /// Example Usage Of Module
    /// </summary>
    public sealed class InventoryHandler : MonoBehaviour
    {
        [SerializeField]
        private Toggle _inventoryToggle = default;

        [SerializeField]
        private List<TrashContentInfo> _trashes = new();

        [SerializeField]
        private Inventory<TrashContentInfo> _inventory = default;

        [SerializeField]
        private InventoryLayoutGroupController _inventoryLayoutController = default;

        private void Awake()
        {
            _inventory.Init(_trashes);
            _inventoryLayoutController.InitInventoryLayout(_inventory.MaxCapacity);
            _inventoryToggle.onValueChanged.AddListener((value) =>
            {
                if(value)
                    _inventoryLayoutController.TryRefreshContent(_inventory.GetItems());
                _inventoryLayoutController.SetActiveLayout(value);
            });
        }

        public void StoreItem(TrashContentInfo trashContentInfo)
        {
            if (!_inventory.TryAdd(trashContentInfo))
                Debug.Log("Inventory Have Max Capacity");
        }
    }
}