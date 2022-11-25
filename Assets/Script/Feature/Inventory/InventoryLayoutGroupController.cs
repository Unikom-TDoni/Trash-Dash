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

        public void ActivateGroupItem(int index) =>
            GeneratedGroupItems[index].gameObject.SetActive(true);

        protected override InventoryLayoutGroupItem InstatiateGroupItem()
        {
            var obj = Instantiate(GroupItem, transform);
            obj.gameObject.SetActive(default);
            return obj;
        }

        private bool IsNeedToRefreshLayout(int lastIndex)
        {
            for (int i = 0; i < lastIndex; i++)
                if (!GeneratedGroupItems[i].gameObject.activeInHierarchy) return true;
            return default;
        }
    }
}