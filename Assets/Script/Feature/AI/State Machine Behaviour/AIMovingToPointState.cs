using Group8.TrashDash.TrashBin;
using UnityEngine;
using UnityEngine.AI;

public class AIMovingToPointState : StateBehaviour {
    AIManager manager;
    NavMeshAgent agent;
    Transform transform;

    public override void Start(Transform transform) {
        manager = GameObject.FindObjectOfType<AIManager>();
        agent = transform.GetComponent<NavMeshAgent>();
        this.transform = transform;
    }

    public override void OnStateEnter() {
        
    }

    public override void OnStateFixedUpdate() {
        base.OnStateFixedUpdate();

        if (Vector3.SqrMagnitude(agent.destination - transform.position) <= Mathf.Epsilon) {
            Collider[] cols = Physics.OverlapSphere(transform.position, 2f);
            foreach (var col in cols) {
                if (col.CompareTag("Chair")) {
                    transform.GetComponent<Animator>().CrossFade("Sitting", .25f);
                    return;
                }
            }

            transform.GetComponent<Animator>().CrossFade("Sitting Down", .25f);
        }        
    }

    public override void OnStateExit() {
        base.OnStateExit();
    }

    public override void OnDrawGizmos() {
        base.OnDrawGizmos();
        Gizmos.DrawLine(transform.position + Vector3.up, agent.destination + Vector3.up);
    }
}