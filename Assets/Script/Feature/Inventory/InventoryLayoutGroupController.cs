using Lnco.Unity.Module.Layout;
using UnityEngine;

namespace Group8.TrashDash.Inventory
{
    public sealed class InventoryLayoutGroupController : LayoutGroupController<InventoryLayoutGroupItem, TrashContentInfo>
    {
        public void InitInventoryLayout(int count) 
        {
            for (int i = 0; i < count; i++)
                Create();
        }

        public void SetActiveLayout(bool value) =>
            gameObject.SetActive(value);

        protected override InventoryLayoutGroupItem InstatiateGroupItem() =>
            Instantiate(GroupItem, transform);
    }
}