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
            transform.position = Vector3.MoveTowards(transform.position, aiManager.queuePosition, Time.fixedDeltaTime * 10f);

            Vector3 dir = aiManager.queuePosition - transform.position;
            dir.y = 0;
            if (Vector3.SqrMagnitude(dir) <= 0.01f) {
                animator.CrossFade("Idle", .25f);
                Debug.Log("In Position : " + transform.position);
            }
        } else {
            transform.position = Vector3.MoveTowards(transform.position, lastQueue.position - (lastQueue.position - transform.position).normalized * 2f, Time.fixedDeltaTime * 10f);
        }
    }
}