using UnityEngine;
using Group8.TrashDash.Event;
using Group8.TrashDash.TrashBin;
using Group8.TrashDash.Inventory;

namespace Group8.TrashDash.Coordinator
{
    public sealed class TrashBinInventoryEventHub : MonoBehaviour
    {
        [SerializeField]
        private TrashBinHandler _trashBinHandler = default;

        [SerializeField]
        private InventoryHandler _inventoryHandler = default;

        private void Awake()
        {
            _trashBinHandler.Subscribe(OnDrop, OnInteract);
        }

        private void OnDestroy()
        {
            _trashBinHandler.Unsubscribe(OnDrop, OnInteract);
        }

        public void OnDrop(DropableData args)
        {
            if (!_trashBinHandler.ActiveTrashBinType.Equals(args.TrashContentInfo.TrashBinType)) return;
            _inventoryHandler.RemoveItem(args.TrashContentInfo, args.InventoryLayoutGroupItem);
        }

        public void OnInteract(TrashBinTypes args)
        {
            _inventoryHandler.SetActiveInventory(true);
        }
    }
}