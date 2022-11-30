using UnityEngine;
using UnityEngine.AI;

/// TODO: Make sit in chair direction and not table.
public class AISittingState : StateBehaviour {
    float duration;
    Transform chair;
    bool standingUp;

    public override void OnStateEnter(Transform transform) {
        chair = null;
        Collider[] cols = Physics.OverlapSphere(transform.position, 2f);
        foreach (var col in cols) {
            if (col.CompareTag("Seat")) {
                chair = col.gameObject.transform;
                transform.GetComponent<NavMeshAgent>().SetDestination(transform.position + chair.forward * .1f);
                break;
            }
        }

        AIManager manager = GameObject.FindWithTag("Manager").GetComponent<AIManager>();
        duration = manager.sittingDuration;

        transform.GetComponent<Animator>().SetBool("Stand Up", false);
        standingUp = false;
    }

    public override void OnStateFixedUpdate(Transform transform) {
        if (chair) {
            Utility.LerpLookTowardsDirection(transform, chair.forward);
        }

        if (!standingUp) {
            duration -= Time.fixedDeltaTime;
            if (duration <= 0) {
                transform.GetComponent<Animator>().SetBool("Stand Up", true);
                standingUp = true;
            }
        }
        
    }
}