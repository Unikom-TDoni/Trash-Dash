using UnityEngine;
using UnityEngine.AI;

public class AIWalkingAwayState : StateBehaviour {
    Transform transform;
    Vector3 target;

    public override void Start(Transform transform) {
        this.transform = transform;
    }

    public override void OnStateEnter() {
        base.OnStateEnter();

        AIManager manager = GameObject.FindWithTag("Manager").GetComponent<AIManager>();
        target = manager.exitPosition;

        NavMeshAgent agent = transform.GetComponent<NavMeshAgent>();
        agent.SetDestination(target);

        manager.pointList.Add(transform.GetComponent<CustomerAI>().targetPoint.gameObject);
    }

    public override void OnStateFixedUpdate() {
        base.OnStateFixedUpdate();

        if (Vector3.SqrMagnitude(target - transform.position) <= 1f) {
            GameObject.Destroy(transform.gameObject);
        }
    }
}