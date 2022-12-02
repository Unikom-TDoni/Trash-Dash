using UnityEngine;
using UnityEngine.AI;

public class AIWalkingAwayState : StateBehaviour {
    Vector3 target;

    public override void OnStateEnter(Transform transform) {
        AIManager manager = GameObject.FindWithTag("Manager").GetComponent<AIManager>();
        target = manager.exitPosition;

        NavMeshAgent agent = transform.GetComponent<NavMeshAgent>();
        agent.SetDestination(target);

        manager.pointList.Add(transform.GetComponent<CustomerAI>().targetPoint.gameObject);
    }

    public override void OnStateFixedUpdate(Transform transform) {

        if (Vector3.SqrMagnitude(target - transform.position) <= 1f) {
            GameObject.Destroy(transform.gameObject);
        }
    }
}