using Group8.TrashDash.TrashBin;
using UnityEngine;
using UnityEngine.AI;

public class AIMovingToPointState : StateBehaviour {
    AIManager manager;
    NavMeshAgent agent;

    public override void Start(Transform transform) {
        manager = GameObject.FindObjectOfType<AIManager>();
        agent = transform.GetComponent<NavMeshAgent>();
    }

    public override void OnStateFixedUpdate(Transform transform) {
        if (Vector3.SqrMagnitude(agent.destination - transform.position) <= Mathf.Epsilon) {
            Collider[] cols = Physics.OverlapSphere(transform.position, 2f);
            foreach (var col in cols) {
                if (col.CompareTag("Seat")) {
                    transform.GetComponent<Animator>().CrossFade("Sitting", .25f);
                    return;
                }
            }

            transform.GetComponent<Animator>().CrossFade("Sitting Down", .25f);
        }        
    }

    public override void OnDrawGizmos(Transform transform) {
        Gizmos.DrawLine(transform.position + Vector3.up, agent.destination + Vector3.up);
    }
}