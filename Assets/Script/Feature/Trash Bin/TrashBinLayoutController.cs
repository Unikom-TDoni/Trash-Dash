using System;
using UnityEngine;
using Group8.TrashDash.Event;
using UnityEngine.EventSystems;
using Lnco.Unity.Module.EventSystems;

namespace Group8.TrashDash.TrashBin
{
    public sealed class TrashBinLayoutController : MonoBehaviour, IDropHandler
    {
        public event Action<DropableData> OnDrop = default;

        void IDropHandler.OnDrop(PointerEventData eventData)
        {
            var selectedObj = eventData.selectedObject;
            if (!selectedObj.TryGetComponent<IDropable<DropableData>>(out var item)) return;
            OnDrop?.Invoke(item.Data);
        }
    }
}