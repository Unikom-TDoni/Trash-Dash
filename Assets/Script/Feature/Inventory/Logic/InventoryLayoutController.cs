using Lnco.Unity.Module.Layout;

namespace Group8.TrashDash.Inventory
{
    public sealed class InventoryLayoutController : LayoutGroupController<InventoryViewHolder, TrashContentInfo>
    {
        public void SetActiveLayout(bool value) =>
            gameObject.SetActive(value);

        public void InitInventoryLayout(int count)
        {
            for (int i = 0; i < count; i++)
                Create();
        }

        protected override InventoryViewHolder InstatiateViewHolder() =>
            Instantiate(ViewHolder);
    }
}