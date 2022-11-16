using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Group8.TrashDash.Interactable
{
    using Player.Interaction;
    public class TrashCan : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            Debug.Log("Interacting with " + gameObject.name + " (TrashCan)");
            // TO DO : Open Trash Can Panel, switching Input
        }
    }
}