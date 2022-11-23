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

        target = GameObject.FindWithTag("Manager").GetComponent<AIManager>().exitPosition;

        NavMeshAgent agent = transform.GetComponent<NavMeshAgent>();
        agent.SetDestination(target);
    }

    public override void OnStateFixedUpdate() {
        base.OnStateFixedUpdate();

        if (Vector3.SqrMagnitude(target - transform.position) <= .01f) {
            GameObject.Destroy(transform.gameObject);
        }
    }
}