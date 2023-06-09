using Group8.TrashDash.Inventory;

namespace Group8.TrashDash.Event
{
    public struct DropableData
    {
        public readonly TrashContentInfo TrashContentInfo;

        public readonly InventoryLayoutGroupItem InventoryLayoutGroupItem;

        public DropableData(TrashContentInfo trailContentInfo, InventoryLayoutGroupItem inventoryLayoutGroupItem)
        {
            TrashContentInfo = trailContentInfo;
            InventoryLayoutGroupItem = inventoryLayoutGroupItem;
        }
    }
}