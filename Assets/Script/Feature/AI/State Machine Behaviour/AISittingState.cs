using UnityEngine;

/// TODO: Make sit in chair direction and not table.
public class AISittingState : StateBehaviour {
    Transform transform;
    Animator animator;

    float duration;
    Transform lookAtTransform;

    bool standingUp;

    public override void Start(Transform transform) {
        this.transform = transform;
        animator = transform.GetComponent<Animator>();
    }

    public override void OnStateEnter() {
        base.OnStateEnter();
        lookAtTransform = null;
        Collider[] cols = Physics.OverlapSphere(transform.position, 2f);
        foreach (var col in cols) {
            if (col.CompareTag("Table")) {
                lookAtTransform = col.gameObject.transform;
                break;
            }
        }

        AIManager manager = GameObject.FindWithTag("Manager").GetComponent<AIManager>();
        duration = manager.sittingDuration;

        animator.SetBool("Stand Up", false);
        standingUp = false;
    }

    public override void OnStateFixedUpdate() {
        base.OnStateFixedUpdate();

        if (lookAtTransform) 
            Utility.LerpLookTowardsTarget(transform, new Vector3(lookAtTransform.position.x, transform.position.y, lookAtTransform.position.z));

        if (!standingUp) {
            duration -= Time.fixedDeltaTime;
            if (duration <= 0) {
                animator.SetBool("Stand Up", true);
                standingUp = true;
            }
        }
        
    }
}