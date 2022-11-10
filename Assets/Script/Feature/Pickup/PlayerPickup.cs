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
        public PlayerAction playerInput;

        [Header("Parameters")]
        public float pickUpRadius = 5;
        [Range(0, 360)] public float pickUpAngle = 125;

        public LayerMask targetMask;
        public List<GameObject> takenObjects;

        [SerializeField] InventoryHandler inventory;

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