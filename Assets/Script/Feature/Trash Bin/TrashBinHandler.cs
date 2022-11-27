using System;
using UnityEngine;
using Group8.TrashDash.Event;
using Group8.TrashDash.Score;
using System.Collections.Generic;

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

        public TrashBinTypes[] GetActiveTrashBinTypes()
        {
            List<TrashBinTypes> activeTrashBinTypes = new List<TrashBinTypes>();
            foreach (var item in _activeTrashBins)
            {
                activeTrashBinTypes.Add(item.GetTrashBinType());
            }

            return activeTrashBinTypes.ToArray();
        }

        private void OnInteract(TrashBinTypes type)
        {
            ActiveTrashBinType = type;
            SetActiveTrashBinLayout(true);
        }
    }
}