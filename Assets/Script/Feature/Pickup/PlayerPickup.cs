using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Group8.TrashDash.Player.Pickup
{
    using Module.Detector;

    public class PlayerPickup : MonoBehaviour
    {
        public PlayerAction playerInput;

        [Header("Parameters")]
        public float pickUpRadius = 5;
        [Range(0, 360)] public float pickUpAngle = 125;

        public LayerMask targetMask;
        public List<GameObject> takenObjects;

        private void Awake()
        {
            targetMask = LayerMask.GetMask("Pickup");
        }

        private void Start()
        {
            playerInput = new PlayerAction();
            InitializeCallback();
        }

        #region Callback
        private void InitializeCallback()
        {
            playerInput.Gameplay.Enable();
            playerInput.Gameplay.Pickup.performed += OnPickup;
        }

        private void OnPickup(InputAction.CallbackContext context)
        {
            takenObjects = ColliderDetector.Find<GameObject>(transform.position, pickUpRadius, targetMask, transform.forward, pickUpAngle);
        }
        #endregion

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, pickUpRadius);
        }
    }
}