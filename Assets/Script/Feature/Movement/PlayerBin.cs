using Group8.TrashDash.Coordinator;
using Group8.TrashDash.Player.Controller;
using Group8.TrashDash.Player.Interaction;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBin : MonoBehaviour, IInteractable
{
    [SerializeField]
    private PlayerController target;

    [SerializeField]
    private TrashBinInventoryEventHub eventHub;

    [SerializeField]
    private float speedMultiplier = .75f;

    [SerializeField]
    private float stopDistance = 3f;

    [SerializeField]
    private float rotateSpeed = 5f;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stopDistance;
        agent.updateRotation = false;
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);
    }

    public void Interact()
    {
        eventHub.OnInventory(default);
    }

    private void Update()
    {
        if(target != null)
        {
            agent.speed = target.Speed * target.SpeedMultiplier * speedMultiplier;

            agent.SetDestination(target.transform.position);

            if (agent.remainingDistance < agent.stoppingDistance + .1f)
            {
                agent.updateRotation = false;
                FaceTarget();
            }
            else agent.updateRotation = true;
        }
    }
}
