using Group8.TrashDash.TrashBin;
using UnityEngine;
using UnityEngine.AI;

public class AIMovingToPointState : StateBehaviour {
    AIManager manager;
    NavMeshAgent agent;
    Transform chair;

    public override void Start(Transform transform) {
        manager = GameObject.FindObjectOfType<AIManager>();
        agent = transform.GetComponent<NavMeshAgent>();
    }

    public override void OnStateFixedUpdate(Transform transform) {
        if (Vector3.SqrMagnitude(agent.destination - transform.position) <= Mathf.Epsilon) {
            Collider[] cols = Physics.OverlapSphere(transform.position, 2f);
            foreach (var col in cols) {
                if (col.CompareTag("Seat")) {
                    chair = col.transform;
                    transform.GetComponent<Animator>().CrossFade("Sitting", .25f);
                    return;
                }
            }

            if (Random.Range(0, 2) == 0) {
                transform.GetComponent<Animator>().CrossFade("Sitting Down", .25f);
            } else {
                transform.GetComponent<Animator>().CrossFade("Texting", .25f);
            }
        }        
    }

    public override void OnStateExit(Transform transform, StateBehaviour newState) {
        base.OnStateExit(transform, newState);
        ((AISittingState) newState).chair = chair;
    }

    public override void OnDrawGizmos(Transform transform) {
        Gizmos.DrawLine(transform.position + Vector3.up, agent.destination + Vector3.up);
    }
}