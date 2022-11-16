using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Group8.TrashDash.Player.Interaction
{
    using Module.Detector;
    using System.Linq;

    public class PlayerInteraction : MonoBehaviour
    {
        private PlayerAction playerControls;

        [Header("Parameters")]
        public float interactRadius = 5;
        [Range(0, 360)] public float interactAngle = 125;

        public LayerMask targetMask;

        public bool faceInteractable;
        public float rotateSpeed = 70;
        public GameObject nearestInteractable;

        private List<GameObject> detectedInteractables;

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

        IEnumerator FaceTarget(Vector3 targetPos)
        {
            if (!faceInteractable) yield break;

            Vector3 targetDirection;
            Quaternion lookRotation = new Quaternion();
            while (Quaternion.Angle(transform.rotation, lookRotation) > 0.01f)
            {
                targetDirection = (targetPos - transform.position).normalized;
                lookRotation = Quaternion.LookRotation(new Vector3(targetDirection.x, 0f, targetDirection.z));
                transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);

                yield return new WaitForEndOfFrame();
            }
        }

        #region Callbacks
        private void RegisterInputCallback()
        {
            if (playerControls == null) return;
            playerControls.Gameplay.Move.performed += OnMove;
            playerControls.Gameplay.Interact.performed += OnInteract;
        }

        private void UnregisterInputCallback()
        {
            if (playerControls == null) return;
            playerControls.Gameplay.Move.performed -= OnMove;
            playerControls.Gameplay.Interact.performed -= OnInteract;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            StopAllCoroutines();
        }

        private void OnInteract(InputAction.CallbackContext context)
        {
            detectedInteractables = ColliderDetector.Find<GameObject>(transform.position, interactRadius, targetMask, transform.forward, interactAngle);
            nearestInteractable = null;

            if (detectedInteractables.Count > 0)
            {
                nearestInteractable = detectedInteractables.OrderBy(
                    obj => (transform.position - obj.transform.position).sqrMagnitude).ToArray()[0];

                StopAllCoroutines();
                StartCoroutine(FaceTarget(nearestInteractable.transform.position));
            }
        }
        #endregion

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, interactRadius);
        }
    }
}