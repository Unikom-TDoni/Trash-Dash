using System;
using System.Linq;
using UnityEngine;
using Group8.TrashDash.Event;
using System.Collections.Generic;
using Group8.TrashDash.Core;
using TMPro;

namespace Group8.TrashDash.TrashBin
{
    public sealed class TrashBinHandler : MonoBehaviour
    {
        [SerializeField]
        private GameObject _trashBinLayout = default;

        [SerializeField]
        private TrashBinLayoutController _layoutController = default;

        [SerializeField]
        private Texture[] _trashBinTextures = default;

        [SerializeField]
        private Material _trashBinMaterial = default;

        private TrashBinController[] _activeTrashBins = default;

        public TutorialManager tutorialManager;

        public TrashBinTypes ActiveTrashBinType { get; private set; } = default;
        public void OnAwake(Action<DropableData> onDrop, Action<TrashBinTypes> onInteract)
        {
            _activeTrashBins = GameObject.FindGameObjectsWithTag("TrashBin").Select(item => item.GetComponent<TrashBinController>()).ToArray();
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

        public void SetActiveTrashBinLayout(bool value)
        {
            _trashBinLayout.SetActive(value);

            if (tutorialManager != null && _trashBinLayout.activeInHierarchy)
            {
                tutorialManager.OpenTrashBin();
            }
        }

        public TrashBinTypes[] GetActiveTrashBinTypes()
        {
            var activeTrashBinTypes = new List<TrashBinTypes>();
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
            ChangeMaterialTexture();
        }

        private void ChangeMaterialTexture()
        {
            switch (ActiveTrashBinType)
            {
                case TrashBinTypes.B3:
                    _trashBinMaterial.SetTexture("_MainTex", _trashBinTextures[default]);
                    break;
                case TrashBinTypes.Anorganik:
                    _trashBinMaterial.SetTexture("_MainTex", _trashBinTextures[1]);
                    break;
                case TrashBinTypes.Organik:
                    _trashBinMaterial.SetTexture("_MainTex", _trashBinTextures[2]);
                    break;
                case TrashBinTypes.Kertas:
                    _trashBinMaterial.SetTexture("_MainTex", _trashBinTextures[3]);
                    break;
            }
        }
    }
}