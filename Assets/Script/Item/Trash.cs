using Unity.VisualScripting;
using UnityEngine;
using Group8.TrashDash.Player.Pickup;
using Group8.TrashDash.Player.Controller;

namespace Group8.TrashDash.Item.Trash
{
    [RequireComponent(typeof(Rigidbody))]
    public class Trash : SpawnObject
    {
        public TrashContentInfo trashContentInfo;
        Rigidbody rb;
        Transform target;
        MeshRenderer meshRenderer;
        bool moveTowards;
        float initialDistance;
        private PlayerAction playerControls;
        bool secondPhase;
        bool secondJump;
        bool anotherJump;

        Collider[] colliders;
        PlayerPickup pp;
        PlayerController pc;
        Target targetIndicator;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            meshRenderer = GetComponent<MeshRenderer>();
            playerControls = InputManager.playerAction;
            GameObject player = GameObject.FindWithTag("Player");
            pp = player.GetComponent<PlayerPickup>();

            pc = player.GetComponent<PlayerController>();

            targetIndicator = GetComponent<Target>();
        }

        public void Initialize()
        {
            colliders = GetComponents<Collider>();
            rb.isKinematic = false;
            meshRenderer.enabled = true;
            colliders[1].enabled = true;
        }

        public void MoveToTarget(Transform _target)
        {
            if (!moveTowards)
            {
                target = _target;
                initialDistance = Vector3.Distance(target.position, transform.position);
                colliders[0].enabled = false;
                colliders[1].enabled = false;
                transform.rotation = Quaternion.identity;
                rb.velocity = Vector3.zero;
                rb.AddForce(transform.up * Mathf.Clamp(initialDistance * 150, 800, 1000));
                targetIndicator.enabled = false;
                moveTowards = true;
            }
        }

        public override void Release()
        {
            Reset();
            base.Release();
        }

        void Update()
        {
            if (transform.position.y < -10f) Debug.LogWarning("Trash Out of Bound");
            if (moveTowards)
            {
                transform.rotation = Quaternion.identity;

                if (target == null)
                {
                    moveTowards = false;
                    colliders[0].enabled = false;
                    colliders[1].enabled = true;
                    rb.velocity = Vector3.zero;
                    return;
                }

                var distanceValue = Vector3.Distance(target.position, transform.position);
                if (playerControls.Gameplay.Move.IsPressed())
                {
                    distanceValue *= 2 * pc.SpeedMultiplier;
                }
                if (secondPhase && anotherJump && transform.position.y < 1)
                {
                    rb.AddForce(transform.up * 100);
                    secondJump = true;
                    anotherJump = false;
                }
                if (secondJump)
                {
                    colliders[0].enabled = true;
                    distanceValue *= 2;
                }

                transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, transform.position.y, target.position.z), distanceValue * Time.deltaTime);

                if (secondPhase)
                {
                    anotherJump = true;
                    return;
                }

                if (transform.position.y >= 2)
                {
                    secondPhase = true;
                    anotherJump = true;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!moveTowards) return;

            if (other.gameObject.tag == "Player" || other.gameObject.tag == "PlayerBin")
            {
                pp.trashJumpingToBin--;
                targetIndicator.enabled = true;
                Release();
            }
        }

        private void Reset()
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
            moveTowards = false;
            secondPhase = false;
            secondJump = false;
            anotherJump = false;
            meshRenderer.enabled = false;
            if (colliders == null) return;
            colliders[0].enabled = false;
            colliders[1].enabled = false;
        }
    }
}