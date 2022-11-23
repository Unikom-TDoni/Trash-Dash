using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Group8.TrashDash.Player.Interaction
{
    using Module.TransformMod;
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
            playerControls.Gameplay.Interact.performed += OnInteract;
        }

        private void UnregisterInputCallback()
        {
            if (playerControls == null) return;
            playerControls.Gameplay.Interact.performed -= OnInteract;
        }

        private void Update()
        {
            if (nearestInteractable == null) return;
            if (!playerControls.Gameplay.enabled) return;

            if (playerControls.Gameplay.Move.IsPressed())
            {
                StopAllCoroutines();
                CancelInteraction();
            }
        }

        private void OnInteract(InputAction.CallbackContext context)
        {
            detectedInteractables = ColliderDetector.Find<GameObject>(transform.position, interactRadius, targetMask, transform.forward, interactAngle);
            nearestInteractable = null;

            if (detectedInteractables.Count > 0)
            {
                nearestInteractable = detectedInteractables.OrderBy(
                    obj => (transform.position - obj.transform.position).sqrMagnitude).ToArray()[0];

                IInteractable interactable = nearestInteractable.GetComponent<IInteractable>();
                if (interactable == null)
                {
                    Debug.Log(nearestInteractable.name + " does not contain IInteractable interface.");
                    return;
                }

                interactable.Interact(gameObject);

                StopAllCoroutines();

                if (faceInteractable)
                   StartCoroutine(TransformModule.FaceTarget(transform, nearestInteractable.transform.position, rotateSpeed));
            }
        }
        #endregion

        private void CancelInteraction()
        {
            if (nearestInteractable)
            {
                IInteractable interactable = nearestInteractable.GetComponent<IInteractable>();
                if (interactable == null)
                {
                    Debug.Log(nearestInteractable.name + " does not contain IInteractable interface.");
                    return;
                }

                interactable.ExitInteract();
            }

            nearestInteractable = null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, interactRadius);
        }
    }
}