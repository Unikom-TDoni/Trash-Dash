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
        public TrashBinTypes GetTrashBinType() => Types;
        public void Interact()
        {
            OnInteract(Types);
        }
    }
}