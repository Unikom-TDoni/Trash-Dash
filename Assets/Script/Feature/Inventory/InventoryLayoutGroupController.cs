using Lnco.Unity.Module.Layout;
using Group8.TrashDash.Inventory;
using UnityEditor;

namespace Group8.TrashDash.Level
{
    public sealed class InventoryLayoutGroupController : LayoutGroupController<InventoryLayoutGroupItem, TrashContentInfo>
    {
        public void InitLayout(int count)
        {
            for (int i = 0; i < count; i++)
                Create();
        }

        public void ResetItems()
        {
            foreach (var item in GeneratedGroupItems)
                item.SnapToPosition();
        }

        public void ActivateGroupItem(int index) =>
            GeneratedGroupItems[index].gameObject.SetActive(true);

        public bool IsNeedToRefreshLayout(int inventoryCount)
        {
            for (int i = 0; i < inventoryCount; i++)
                if (!GeneratedGroupItems[i].gameObject.activeInHierarchy) return true;
            return default;
        }

        protected override InventoryLayoutGroupItem InstatiateGroupItem()
        {
            var obj = Instantiate(GroupItem, transform);
            obj.gameObject.SetActive(default);
            return obj;
        }
    }
}