using UnityEngine;

public class AISittingState : StateBehaviour {
    Transform transform;
    Animator animator;

    float duration;
    Vector3 lookAtPos;

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

        duration = 0f;
    }

    public override void OnStateFixedUpdate() {
        base.OnStateFixedUpdate();

        Utility.LerpLookTowardsTarget(transform, lookAtPos);
        
        duration -= Time.fixedDeltaTime;
        if (duration <= 0) {
            animator.CrossFade("Sitting Stand Up", .25f);
        }
    }
}