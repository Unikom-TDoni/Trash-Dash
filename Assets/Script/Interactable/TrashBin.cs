using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Group8.TrashDash.Interactable
{
    using Group8.TrashDash.TrashBin;
    using Player.Interaction;

    public class TrashBin : MonoBehaviour, IInteractable
    {
        [SerializeField] private TrashBinTypes TrashBinTypes;

        public void Interact()
        {
            Debug.Log("Interacting with " + gameObject.name + " (TrashCan)");
            // TO DO : Open Trash Can Panel, switching Input
        }
    }
}