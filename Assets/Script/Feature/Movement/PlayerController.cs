using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Group8.TrashDash.Player.Controller
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour, IPowerUpMultiply
    {
        [Header("Movement")]
        public float moveSpeed = 5f;
        public float sprintSpeed = 8f;
        public float gravity = -9.8f;
        public float turnSmoothTime = 0.1f;

        private PlayerAction playerControls;

        CharacterController controller;

        Vector3 rawInputMovement = Vector3.zero;
        Vector3 velocity;
        Vector3 moveDirection;

        // Movement Parameters
        float speed;
        float turnSmoothVelocity;

        bool canMove = true;

        Animator animator;
        public float transitionSpeed = 2f;

        [Header("Force")]
        public float pushForce;

        [SerializeField] private PowerUpValue[] powerUpParameters;
        private Dictionary<string, float> powerUpValues = new Dictionary<string, float>();

        void Awake()
        {
            controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            InitializePowerUpValues();
        }

        void Start()
        {
            speed = moveSpeed;
            playerControls = InputManager.playerAction;
            RegisterInputCallbacks();
        }

        private void OnEnable()
        {
            RegisterInputCallbacks();
        }

        private void OnDisable()
        {
            UnregisterInputCallbacks();
        }

        public IEnumerator OnPowerUpMultiply(string parameterName, float multiplier, float duration)
        {
            float initialValue = powerUpValues[parameterName];
            powerUpValues[parameterName] = multiplier;

            yield return new WaitForSeconds(duration);

            powerUpValues[parameterName] = initialValue;
        }

        void Update()
        {
            moveDirection = GetMovementInputDirection();
            velocity = new Vector3(moveDirection.x * speed * powerUpValues["speed"], velocity.y, moveDirection.z * speed * powerUpValues["speed"]);

            // Gravity
            if (controller.isGrounded)
            {
                if (velocity.y < 0f)
                    velocity.y = -2f;
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            animator.SetFloat("magnitude", Mathf.MoveTowards(animator.GetFloat("magnitude"), (speed * powerUpValues["speed"] / sprintSpeed) * moveDirection.magnitude, Time.deltaTime * transitionSpeed));
        }

        private void InitializePowerUpValues()
        {
            foreach(PowerUpValue powerUpValue in powerUpParameters)
            {
                powerUpValues.Add(powerUpValue.name, powerUpValue.value);
            }
        }

        #region Callbacks
        private void RegisterInputCallbacks()
        {
            if (playerControls == null) return;

            playerControls.Gameplay.Move.performed += OnMove;
            playerControls.Gameplay.Move.canceled += OnMoveCanceled;
            playerControls.Gameplay.Sprint.performed += OnSprint;
            playerControls.Gameplay.Sprint.canceled += OnSprintCanceled;
        }
        private void UnregisterInputCallbacks()
        {
            if (playerControls == null) return;

            playerControls.Gameplay.Move.performed -= OnMove;
            playerControls.Gameplay.Move.canceled -= OnMoveCanceled;
            playerControls.Gameplay.Sprint.performed -= OnSprint;
            playerControls.Gameplay.Sprint.canceled -= OnSprintCanceled;
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
        #endregion

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody rb = hit.collider.attachedRigidbody;
            if (rb && !rb.isKinematic)
            {
                rb.velocity = hit.moveDirection * pushForce * (speed / moveSpeed);
            }
        }
    }
}