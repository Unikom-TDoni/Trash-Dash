using UnityEngine;
using UnityEngine.AI;

public class AIQueueingState : StateBehaviour {
    CustomerAI customerAI;
    Transform transform;
    AIManager aiManager;
    Animator animator;

    Transform lastQueue;

    public override void Start(Transform transform) {
        base.Start(transform);
        this.transform = transform;
        customerAI = transform.GetComponent<CustomerAI>();
        animator = transform.GetComponent<Animator>();
        aiManager = GameObject.FindObjectOfType<AIManager>();
    }

    public override void OnStateEnter() {
        base.OnStateEnter();
        lastQueue = aiManager.customerQueue.Count == 0 ? null : aiManager.customerQueue[aiManager.customerQueue.Count - 1].transform;
        aiManager.customerQueue.Add(customerAI);
    }

    public override void OnStateFixedUpdate() {
        base.OnStateFixedUpdate();

        if (aiManager.customerQueue[0] == customerAI) {
            transform.position = Vector3.MoveTowards(transform.position, aiManager.stallPosition, Time.fixedDeltaTime * aiManager.speed);

            if (Vector3.SqrMagnitude(aiManager.stallPosition - transform.position) <= Mathf.Epsilon) {
                animator.CrossFade("Idle", .25f);
            }
        } else {
            transform.position = Vector3.MoveTowards(transform.position, lastQueue.position - (lastQueue.position - transform.position).normalized * 2f, Time.fixedDeltaTime * aiManager.speed);
        }
    }
}