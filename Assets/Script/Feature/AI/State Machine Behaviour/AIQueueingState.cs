using UnityEngine;
using UnityEngine.AI;

public class AIQueueingState : StateBehaviour {
    CustomerAI customerAI;
    Transform transform;
    AIManager aiManager;
    Transform lastQueue;
    NavMeshAgent agent;

    public override void Start(Transform transform) {
        base.Start(transform);
        this.transform = transform;
        customerAI = transform.GetComponent<CustomerAI>();
        aiManager = GameObject.FindObjectOfType<AIManager>();
        agent = transform.GetComponent<NavMeshAgent>();

        customerAI.spawnConfiguration = aiManager.GetSpawnConfiguration();
        transform.position = customerAI.spawnConfiguration.spawnPosition;
        transform.rotation = customerAI.spawnConfiguration.spawnRotation;
    }

    public override void OnStateEnter() {
        base.OnStateEnter();
        lastQueue = customerAI.spawnConfiguration.customerQueue.Count == 0 ? null : customerAI.spawnConfiguration.customerQueue[customerAI.spawnConfiguration.customerQueue.Count - 1].transform;
        customerAI.spawnConfiguration.customerQueue.Add(customerAI);
    }

    public override void OnStateFixedUpdate() {
        base.OnStateFixedUpdate();

        if (customerAI.spawnConfiguration.customerQueue[0] == customerAI) {
            agent.destination = customerAI.spawnConfiguration.queuePosition;

            Vector3 dir = customerAI.spawnConfiguration.queuePosition - transform.position;
            dir.y = 0;
            if (Vector3.SqrMagnitude(dir) <= 0.01f) {
                transform.GetComponent<Animator>().CrossFade("Idle", .25f);
            }
        } else {
            agent.destination = lastQueue.position - (lastQueue.position - transform.position).normalized * 2f;
        }
    }
}