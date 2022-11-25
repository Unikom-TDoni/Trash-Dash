using Lnco.Unity.Module.Layout;
using Group8.TrashDash.Inventory;
using System.Collections.Generic;

namespace Group8.TrashDash.Level
{
    public sealed class InventoryLayoutGroupController : LayoutGroupController<InventoryLayoutGroupItem, TrashContentInfo>
    {
        public void InitLayout(int count)
        {
            for (int i = 0; i < count; i++)
                Create();
        }

        public void ResetItems(ICollection<TrashContentInfo> content)
        {
            foreach (var item in GeneratedGroupItems)
                item.SnapToPosition();

            if(IsNeedToRefreshLayout(content.Count))
                TryRefreshContent(content);
        }

        protected override InventoryLayoutGroupItem InstatiateGroupItem() =>
            Instantiate(GroupItem, transform);

        private bool IsNeedToRefreshLayout(int lastIndex)
        {
            for (int i = 0; i < lastIndex; i++)
                if (!GeneratedGroupItems[i].IsImageEnabled()) return true;
            return default;
        }
    }
}