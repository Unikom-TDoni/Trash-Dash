using UnityEngine;

public class AISittingState : StateBehaviour {
    Vector3 lookAtPos;
    Transform transform;

    public override void Start(Transform transform) {
        this.transform = transform;
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
    }

    public override void OnStateFixedUpdate() {
        base.OnStateFixedUpdate();

        Utility.LerpLookTowardsTarget(transform, lookAtPos);
    }
}