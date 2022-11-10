using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Group8.TrashDash.Player.Interaction
{
    public class PlayerInteraction : MonoBehaviour
    {
        public PlayerAction playerInput;

        private void Start()
        {
            playerInput = new PlayerAction();
            InitializeCallback();
        }

        #region Callback
        private void InitializeCallback()
        {
            playerInput.Gameplay.Enable();
            playerInput.Gameplay.Pickup.performed += OnInteract;
        }
        private void OnInteract(InputAction.CallbackContext context)
        {

        }
        #endregion
    }
}