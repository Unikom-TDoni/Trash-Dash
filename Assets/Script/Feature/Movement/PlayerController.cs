using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Group8.TrashDash.Player.Controller
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        public float moveSpeed = 5f;
        public float sprintSpeed = 8f;
        public float gravity = -9.8f;
        public float turnSmoothTime = 0.1f;

        public PlayerAction playerInput;

        CharacterController controller;

        Vector3 rawInputMovement;
        Vector3 velocity;
        Vector3 moveDirection;

        // Movement Parameters
        float speed;
        float turnSmoothVelocity;

        bool canMove = true;

        void Awake()
        {
            controller = GetComponent<CharacterController>();

            playerInput = new PlayerAction();

            InitializePlayerInputCallbacks();
        }

        void Start()
        {
            speed = moveSpeed;
        }

        void Update()
        {
            moveDirection = GetMovementInputDirection();
            velocity = new Vector3(moveDirection.x * speed, velocity.y, moveDirection.z * speed);

            // Gravity
            if (controller.isGrounded)
            {
                if (velocity.y < 0f)
                    velocity.y = -2f;
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        #region Callbacks
        private void InitializePlayerInputCallbacks()
        {
            playerInput.Gameplay.Enable();
            playerInput.Gameplay.Move.performed += OnMove;
            playerInput.Gameplay.Move.canceled += OnMoveCanceled;
            playerInput.Gameplay.Sprint.performed += OnSprint;
            playerInput.Gameplay.Sprint.canceled += OnSprintCanceled;
            playerInput.Gameplay.Interact.performed += OnInteract;
            playerInput.Gameplay.Inventory.performed += OnInventory;
            playerInput.Gameplay.Pause.performed += OnPause;
        }
        #endregion

        #region Movement
        // Return Vector3 Move Input Direction
        private Vector3 GetMovementInputDirection()
        {
            if (rawInputMovement.magnitude > 0.1f)
            {
                float targetAngle = Mathf.Atan2(rawInputMovement.x, rawInputMovement.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                return moveDirection;
            }

            return Vector3.zero;
        }
        #endregion

        #region Callback Functions
        public void OnMove(InputAction.CallbackContext context)
        {
            if (!canMove)
            {
                rawInputMovement = Vector3.zero;
                return;
            }

            Vector2 inputMovement = context.ReadValue<Vector2>();
            rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
        }
        public void OnMoveCanceled(InputAction.CallbackContext context)
        {
            rawInputMovement = Vector3.zero;
        }
        public void OnSprint(InputAction.CallbackContext context)
        {
            if (!controller.isGrounded) return;

            speed = sprintSpeed;
        }
        public void OnSprintCanceled(InputAction.CallbackContext context)
        {
            speed = moveSpeed;
        }
        public void OnInteract(InputAction.CallbackContext context)
        {
            Debug.Log("Interact Button pressed");
        }
        public void OnInventory(InputAction.CallbackContext context)
        {
            Debug.Log("Inventory Button pressed");
        }
        public void OnPause(InputAction.CallbackContext context)
        {
            Debug.Log("Pause Button pressed");
        }
        #endregion
    }
}