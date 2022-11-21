using UnityEngine;
using UnityEngine.EventSystems;
using Group8.TrashDash.Inventory;

namespace Group8.TrashDash.TrashBin
{
    public sealed class TrashBinLayoutController : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private TrashBinTypes _trashBinType = default;
        [SerializeField] private Animator trashBinAnim;
        public bool dragTrash = false;

        public void OnDrop(PointerEventData eventData)
        {
            var selectedObj = eventData.selectedObject;
            if (!selectedObj.TryGetComponent<InventoryLayoutGroupItem>(out var item)) return;
            if (_trashBinType != item.GetTrashBinTypes()) return;
            item.Reset();
            dragTrash = false;
        }

        public void SetTrashBinType(TrashBinTypes type) =>
            _trashBinType = type;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (dragTrash)
            {
                AnimOpen();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            AnimClose();
        }

        public void AnimOpen()
        {
            trashBinAnim.SetBool("isOpening", true);
            trashBinAnim.SetBool("isClosing", false);
        }

        public void AnimClose()
        {
            trashBinAnim.SetBool("isClosing", true);
            trashBinAnim.SetBool("isOpening", false);
        }
    }
}