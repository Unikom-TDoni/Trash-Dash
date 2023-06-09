using UnityEngine;
using UnityEngine.AI;

public class AIQueueingState : StateBehaviour {
    CustomerAI customerAI;
    Transform lastQueue;
    NavMeshAgent agent;
    Animator animator;

    public override void Start(Transform transform) {
        base.Start(transform);
        customerAI = transform.GetComponent<CustomerAI>();
        agent = transform.GetComponent<NavMeshAgent>();
        animator = transform.GetComponent<Animator>();
    }

    public override void OnStateEnter(Transform transform) {
        lastQueue = customerAI.spawnConfiguration.customerQueue.Count == 0 ? null : customerAI.spawnConfiguration.customerQueue[customerAI.spawnConfiguration.customerQueue.Count - 1].transform;
        customerAI.spawnConfiguration.customerQueue.Add(customerAI);
    }

    public override void OnStateFixedUpdate(Transform transform) {
        if (customerAI.spawnConfiguration.customerQueue[0] == customerAI) {
            agent.destination = customerAI.spawnConfiguration.queuePosition; // EXPENSIVE, make duration

            Vector3 dir = customerAI.spawnConfiguration.queuePosition - transform.position;
            dir.y = 0;
            if (Vector3.SqrMagnitude(dir) <= 0.01f) {
                animator.CrossFade("Idle", .25f);
            }
        } else {
            agent.destination = lastQueue.position - (lastQueue.position - transform.position).normalized * 2f; // EXPENSIVE, make duration
        }
        animator.SetFloat("Magnitude", (agent.destination - transform.position).magnitude); // EXPENSIVE, change only to when moving and stopped
    }
}