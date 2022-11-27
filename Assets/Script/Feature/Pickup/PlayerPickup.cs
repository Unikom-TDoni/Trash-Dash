using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace Group8.TrashDash.Player.Pickup
{
    using Module.Detector;
    using Inventory;
    using Item.Trash;
    using System.Linq;
    using Group8.TrashDash.Score;

    public class PlayerPickup : MonoBehaviour
    {
        private PlayerAction playerControls;

        [Header("Parameters")]
        public float pickUpRadius = 5;
        [Range(0, 360)] public float pickUpAngle = 125;

        public LayerMask targetMask;
        public List<GameObject> takenObjects;

        [SerializeField] InventoryHandler inventory;
        [SerializeField] ScoreManager scoreManager;

        Animator playerAnimator;

        private void Awake()
        {
            playerAnimator = GetComponent<Animator>();
        }

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

            foreach (GameObject obj in takenObjects.OrderBy(
                    obj => (transform.position - obj.transform.position).sqrMagnitude).ToArray())
            {
                Trash trash = obj.GetComponent<Trash>();
                if (trash == null)
                {
                    Debug.Log(obj.name + " does not contain TrashInfo.");
                    continue;
                }

                if (inventory.TryAddItem(trash.trashContentInfo))
                {
                    trash.MoveToTarget(transform);
                    takenObjects.Remove(obj);

                    scoreManager?.UpdateScore(ScoreState.Collect);
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