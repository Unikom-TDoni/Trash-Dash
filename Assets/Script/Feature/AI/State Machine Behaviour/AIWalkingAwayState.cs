using UnityEngine;
using UnityEngine.AI;

public class AIWalkingAwayState : StateBehaviour {
    Vector3 target;
    bool stoppedTrashing;

    public override void OnStateEnter(Transform transform) {
        AIManager manager = transform.GetComponent<CustomerAI>().aiManager;
        target = manager.exitPosition;

        NavMeshAgent agent = transform.GetComponent<NavMeshAgent>();
        agent.enabled = true;
        agent.SetDestination(target);

        manager.pointList.Add(transform.GetComponent<CustomerAI>().targetPoint.gameObject);
        stoppedTrashing = false;
    }

    public override void OnStateFixedUpdate(Transform transform) {
        float sqrMag = Vector3.SqrMagnitude(target - transform.position);
        if (sqrMag <= 1f) {
            GameObject.Destroy(transform.gameObject);
        } else if (!stoppedTrashing && sqrMag <= 225f) {
            transform.GetComponent<CustomerAI>().StopTrashing();
            stoppedTrashing = true;
        }
    }

    public override void OnDrawGizmos(Transform transform) {
        base.OnDrawGizmos(transform);
        
        Gizmos.color = stoppedTrashing ? Color.red : Color.white;
        Gizmos.DrawLine(transform.position + Vector3.up, target + Vector3.up);
    }
}