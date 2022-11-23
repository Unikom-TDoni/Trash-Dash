using System;
using UnityEngine;
using Group8.TrashDash.Event;
using UnityEngine.EventSystems;
using Lnco.Unity.Module.EventSystems;

namespace Group8.TrashDash.TrashBin
{
    public sealed class TrashBinLayoutController : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action<DropableData> OnDrop = default;

        [SerializeField] private Animator trashBinAnim;
        public bool dragTrash = false;

        private readonly int _openAnimParam = Animator.StringToHash("openTrigger");

        void IDropHandler.OnDrop(PointerEventData eventData)
        {
            var selectedObj = eventData.selectedObject;
            if (!selectedObj.TryGetComponent<IDropable<DropableData>>(out var item)) return;
            OnDrop?.Invoke(item.Data);
            dragTrash = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerEnter is null) return;
            if (dragTrash) AnimOpen();
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