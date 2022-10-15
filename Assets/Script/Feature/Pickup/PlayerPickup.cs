using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Group8.TrashDash.Player.Pickup
{
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

        public Vector3 DirectionFromAngle(float angle, bool globalAngle)
        {
            if (!globalAngle) angle += transform.eulerAngles.y;
            return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
        }

        private void FindTargets()
        {
            takenObjects.Clear();
            Collider[] targetsInRadius = Physics.OverlapSphere(transform.position, pickUpRadius, targetMask);

            foreach (Collider targetCollider in targetsInRadius)
            {
                Transform target = targetCollider.transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, directionToTarget) < (pickUpAngle / 2))
                {
                    takenObjects.Add(target.gameObject);
                }
            }
        }

        #region Callback
        private void InitializeCallback()
        {
            playerInput.Gameplay.Enable();
            playerInput.Gameplay.Pickup.performed += OnPickup;
        }

        private void OnPickup(InputAction.CallbackContext context)
        {
            FindTargets();
        }
        #endregion

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, pickUpRadius);
        }
    }
}