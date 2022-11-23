using System;
using UnityEngine;
using Group8.TrashDash.Event;
using UnityEngine.EventSystems;
using Lnco.Unity.Module.EventSystems;

namespace Group8.TrashDash.TrashBin
{
    [RequireComponent(typeof(Animator))]
    public sealed class TrashBinLayoutController : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action<DropableData> OnDrop = default;

        private Animator _animator = default;

        private readonly int _openAnimParam = Animator.StringToHash("openTrigger");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        void IDropHandler.OnDrop(PointerEventData eventData)
        {
            var selectedObj = eventData.selectedObject;
            if (!selectedObj.TryGetComponent<IDropable<DropableData>>(out var item)) return;
            OnDrop?.Invoke(item.Data);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerEnter is null) return;
            _animator.SetTrigger(_openAnimParam);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _animator.SetTrigger(_openAnimParam);
        }
    }
}