using System;
using System.Linq;
using UnityEngine;
using Group8.TrashDash.Event;
using System.Collections.Generic;
using Group8.TrashDash.Core;

namespace Group8.TrashDash.TrashBin
{
    public sealed class TrashBinHandler : MonoBehaviour
    {
        [SerializeField]
        private GameObject _trashBinLayout = default;

        [SerializeField]
        private TrashBinLayoutController _layoutController = default;

        private TrashBinController[] _activeTrashBins = default;

        public TrashBinTypes ActiveTrashBinType { get; private set; } = default;

        public void OnAwake(Action<DropableData> onDrop, Action<TrashBinTypes> onInteract)
        {
            _activeTrashBins = GameObject.FindGameObjectsWithTag(GameManager.Instance.Tags.TrashBin).Select(item => item.GetComponent<TrashBinController>()).ToArray();
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
            _trashBinLayout.SetActive(value);

        public TrashBinTypes[] GetActiveTrashBinTypes()
        {
            List<TrashBinTypes> activeTrashBinTypes = new List<TrashBinTypes>();
            foreach (var item in _activeTrashBins)
            {
                activeTrashBinTypes.Add(item.GetTrashBinType());
            }

            Debug.Log(activeTrashBinTypes.Count);

            return activeTrashBinTypes.ToArray();
        }

        private void OnInteract(TrashBinTypes type)
        {
            ActiveTrashBinType = type;
            SetActiveTrashBinLayout(true);
        }

    }
}