using UnityEngine;

public class AIStandingUpState : StateBehaviour {
    public Transform chair;
    Vector3 targetPos;

    public override void OnStateEnter(Transform transform) {
        targetPos = chair.position + chair.forward;
        targetPos.y = transform.position.y;
    }

    public override void OnStateFixedUpdate(Transform transform) {
        Utility.LerpLookTowardsDirection(transform, chair.forward);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.fixedDeltaTime * .5f);
    }
}