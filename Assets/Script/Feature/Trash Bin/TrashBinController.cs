using System;
using UnityEngine;
using Group8.TrashDash.Player.Interaction;

namespace Group8.TrashDash.TrashBin
{
    public sealed class TrashBinController : MonoBehaviour, IInteractable
    {
        public event Action<TrashBinTypes> OnInteract = default;

        [SerializeField]
        private TrashBinTypes Types = default;

        private AudioSource _audioSource = default;

        public TrashBinTypes GetTrashBinType() => Types;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Interact()
        {
            OnInteract(Types);
            _audioSource.Play();
        }
    }
}