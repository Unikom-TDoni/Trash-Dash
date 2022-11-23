using UnityEngine;

public class AISittingState : StateBehaviour {
    Transform transform;
    Animator animator;

    float duration;
    Vector3 lookAtPos;

    bool standingUp;

    public override void Start(Transform transform) {
        this.transform = transform;
        animator = transform.GetComponent<Animator>();
    }

    public override void OnStateEnter() {
        base.OnStateEnter();

        Collider[] cols = Physics.OverlapSphere(transform.position, 2f);
        foreach (var col in cols) {
            if (col.CompareTag("Table")) {
                lookAtPos = col.gameObject.transform.position;
                lookAtPos.y = transform.position.y;
                break;
            }
        }

        AIManager manager = GameObject.FindWithTag("Manager").GetComponent<AIManager>();
        duration = manager.sittingDuration;

        standingUp = false;
    }

    public override void OnStateFixedUpdate() {
        base.OnStateFixedUpdate();

        Utility.LerpLookTowardsTarget(transform, lookAtPos);

        if (!standingUp) {
            duration -= Time.fixedDeltaTime;
            if (duration <= 0) {
                animator.CrossFade("Sitting Stand Up", .25f);
                standingUp = true;
            }
        }
        
    }
}