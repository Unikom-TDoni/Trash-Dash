using UnityEngine;
using UnityEngine.EventSystems;
using Group8.TrashDash.Inventory;

namespace Group8.TrashDash.TrashBin
{
    public sealed class TrashBinLayoutController : MonoBehaviour, IDropHandler
    {
        private TrashBinTypes _trashBinType = default;

        public void OnDrop(PointerEventData eventData)
        {
            var selectedObj = eventData.selectedObject;
            if (!selectedObj.TryGetComponent<InventoryLayoutGroupItem>(out var item)) return;
            if (_trashBinType != item.GetTrashBinTypes()) return;
            item.Reset();
        }

        public void SetTrashBinType(TrashBinTypes type) =>
            _trashBinType = type;
    }
}