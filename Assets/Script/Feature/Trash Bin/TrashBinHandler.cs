using System;
using UnityEngine;
using Group8.TrashDash.Event;
using Group8.TrashDash.Score;

namespace Group8.TrashDash.TrashBin
{
    public sealed class TrashBinHandler : MonoBehaviour
    {
        [SerializeField]
        private GameObject _trashBinObj = default;

        [SerializeField]
        private TrashBinLayoutController _layoutController = default;

        [SerializeField]
        private TrashBinController[] _activeTrashBins = default;

        public TrashBinTypes ActiveTrashBinType { get; private set; } = default;

        public void Subscribe(Action<DropableData> onDrop, Action<TrashBinTypes> onInteract)
        {
            _layoutController.OnDrop += onDrop;
            foreach (var item in _activeTrashBins)
            {
                item.OnInteract += onInteract;
                item.OnInteract += OnInteract;
            }
        }

        public void Unsubscribe(Action<DropableData> onDrop, Action<TrashBinTypes> onInteract)
        {
            _layoutController.OnDrop -= onDrop;
            foreach (var item in _activeTrashBins)
            {
                item.OnInteract -= onInteract;
                item.OnInteract -= OnInteract;
            }
        }

        public void SetActiveTrashBinLayout(bool value) =>
            _trashBinObj.SetActive(value);

        private void OnInteract(TrashBinTypes type)
        {
            ActiveTrashBinType = type;
            SetActiveTrashBinLayout(true);
        }
    }
}