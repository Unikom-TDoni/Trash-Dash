using UnityEngine;

namespace Group8.TrashDash.Player.Interaction
{
    public interface IInteractable
    {
        void Interact() { }
        void Interact(GameObject other = default) { Interact(); }
        public void ExitInteract() { }
    }
}