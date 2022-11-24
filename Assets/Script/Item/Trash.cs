using UnityEngine;

namespace Group8.TrashDash.Item.Trash
{
    [RequireComponent(typeof(Rigidbody))]
    public class Trash : SpawnObject
    {
        public TrashContentInfo trashContentInfo;
        Rigidbody rb;
        Transform player;
        bool moveTowards;
        Vector3 targetPosition;
        float initialDistance;
        private PlayerAction playerControls;
        bool secondPhase;
        bool secondJump;
        bool anotherJump;

        Collider[] colliders;
 
        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            player = GameObject.FindWithTag("Player").transform;
            playerControls = InputManager.playerAction;
        }

        private void Start()
        {
            colliders = GetComponents<Collider>();
        }

        public override void Release()
        {
            if (!moveTowards)
            {
                initialDistance = Vector3.Distance(player.position, transform.position);
                colliders[0].enabled = false;
                colliders[1].enabled = false;
                transform.rotation = Quaternion.identity;
                rb.AddForce(transform.up * Mathf.Clamp(Vector3.Distance(player.position, transform.position) * 150, 900, 10000));
                moveTowards = true;
            }
        }

        void Update()
        {
            if (moveTowards)
            {
                transform.rotation = Quaternion.identity;
                targetPosition = player.position;
                var distanceValue = Vector3.Distance(player.position, transform.position);
                if (playerControls.Gameplay.Sprint.IsPressed())
                {
                    distanceValue *= 2;
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

                transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPosition.x, transform.position.y, targetPosition.z), distanceValue * Time.deltaTime);

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
            if (other.gameObject.tag == "Player")
            {
                base.Release();
                rb.velocity = Vector3.zero;
                transform.rotation = Quaternion.identity;
                moveTowards = false;
                secondPhase = false;
                secondJump = false;
                anotherJump = false;
                colliders[0].enabled = false;
                colliders[1].enabled = true;
            }
        }
    }
}