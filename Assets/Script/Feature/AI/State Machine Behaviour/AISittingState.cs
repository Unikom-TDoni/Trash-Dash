using UnityEngine;
using UnityEngine.AI;

/// TODO: Make sit in chair direction and not table.
public class AISittingState : StateBehaviour {
    public Transform chair;
    float duration;
    bool standingUp;
    Vector3 targetPos;

    public override void OnStateEnter(Transform transform) {
        if (chair) {
            targetPos = chair.position + chair.forward * .1f;
            targetPos.y = transform.position.y;
            transform.GetComponent<NavMeshAgent>().enabled = false;
        }

        AIManager manager = transform.GetComponent<CustomerAI>().aiManager;
        duration = manager.sittingDuration;

        transform.GetComponent<Animator>().SetBool("Stand Up", false);
        standingUp = false;
    }

    public override void OnStateFixedUpdate(Transform transform) {
        if (chair) {
            Utility.LerpLookTowardsDirection(transform, chair.forward);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.fixedDeltaTime * 10f);
        }

        if (!standingUp) {
            duration -= Time.fixedDeltaTime;
            if (duration <= 0) {
                transform.GetComponent<Animator>().SetBool("Stand Up", true);
                standingUp = true;
            }
        }
    }

    public override void OnStateExit(Transform transform, StateBehaviour newState) {
        if (newState is AIStandingUpState) {
            ((AIStandingUpState) newState).chair = chair;
        }
    }
}