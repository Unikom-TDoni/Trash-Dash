using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Group8.TrashDash.Player.Pickup
{
    using Module.Detector;
    using Inventory;
    using Item.Trash;

    public class PlayerPickup : MonoBehaviour
    {
        private PlayerAction playerControls;

        [Header("Parameters")]
        public float pickUpRadius = 5;
        [Range(0, 360)] public float pickUpAngle = 125;

        public LayerMask targetMask;
        public List<GameObject> takenObjects;

        [SerializeField] InventoryHandler inventory;

        private void Start()
        {
            playerControls = InputManager.playerAction;
            RegisterInputCallback();
        }

        private void OnEnable()
        {
            RegisterInputCallback();
        }

        private void OnDisable()
        {
            UnregisterInputCallback();
        }

        #region Callbacks
        private void RegisterInputCallback()
        {
            if (playerControls == null) return;
            playerControls.Gameplay.Pickup.performed += OnPickup;
        }
        private void UnregisterInputCallback()
        {
            if (playerControls == null) return;
            playerControls.Gameplay.Pickup.performed -= OnPickup;
        }

        private void OnPickup(InputAction.CallbackContext context)
        {
            takenObjects = ColliderDetector.Find<GameObject>(transform.position, pickUpRadius, targetMask, transform.forward, pickUpAngle);

            foreach (GameObject obj in takenObjects.ToArray())
            {
                TrashInfo trashInfo = obj.GetComponent<TrashInfo>();
                if (trashInfo == null)
                {
                    Debug.Log(obj.name + " does not contain TrashInfo.");
                    continue;
                }

                if (inventory.StoreItem(trashInfo.trashContentInfo))
                {
                    Destroy(obj);
                    takenObjects.Remove(obj);
                }
            }
        }
        #endregion

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, pickUpRadius);
        }
    }
}