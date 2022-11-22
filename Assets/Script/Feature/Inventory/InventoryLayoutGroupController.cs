using Lnco.Unity.Module.Layout;

namespace Group8.TrashDash.Inventory
{
    public sealed class InventoryLayoutGroupController : LayoutGroupController<InventoryLayoutGroupItem, TrashContentInfo>
    {
        public void InitLayout(int count) 
        {
            for (int i = 0; i < count; i++) 
                Create();
        }

        protected override InventoryLayoutGroupItem InstatiateGroupItem() =>
             Instantiate(GroupItem, transform);
    }
}