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
    using Group8.TrashDash.Item.Audio;

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
        [SerializeField] Animator playerBinAnim_1, playerBinAnim_2;
        bool binIsOpen = false;
        public int trashJumpingToBin;
        [SerializeField] Transform playerBinTransform;

        [SerializeField]
        private PlayerAudioController _playerAudioController = default;

        public TutorialManager tutorialManager;

        private void Awake()
        {
            playerAnimator = GetComponent<Animator>();
        }

        private void Start()
        {
            playerControls = InputManager.playerAction;
            RegisterInputCallback();
        }

        private void Update()
        {
            if (binIsOpen)
            {
                if (trashJumpingToBin <= 0)
                {
                    if (playerBinAnim_1 != null && playerBinAnim_2 != null)
                    {
                        playerBinAnim_1.SetTrigger("trigClose");
                        playerBinAnim_2.SetTrigger("trigClose");
                    }
                    binIsOpen = false;
                }
            }
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

            if (takenObjects.Count == 0 || inventory.IsFull())
                return;

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
                    if (playerBinTransform != null)
                    {
                        trash.MoveToTarget(playerBinTransform);
                    }
                    else
                    {
                        trash.MoveToTarget(transform);
                    }

                    takenObjects.Remove(obj);
                    scoreManager?.UpdateScore(ScoreState.Collect);

                    if (!binIsOpen)
                    {
                        binIsOpen = true;
                        if (playerBinAnim_1 != null && playerBinAnim_2 != null)
                        {
                            playerBinAnim_1.SetTrigger("trigOpen");
                            playerBinAnim_2.SetTrigger("trigOpen");
                        }
                    }
                    trashJumpingToBin++;

                    if (tutorialManager != null)
                    {
                        tutorialManager.PickupTrash();
                    }
                }

                _playerAudioController.PlayPickupSfx();
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