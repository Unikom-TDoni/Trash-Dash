using UnityEngine;

public class AIOrderingState : StateBehaviour {
    Transform transform;
    AIManager manager;
    Animator animator;

    float orderTime;

    public override void Start(Transform transform) {
        base.Start(transform);
        this.transform = transform;
        manager = GameObject.FindObjectOfType<AIManager>();
        animator = transform.GetComponent<Animator>();
    }

    public override void OnStateEnter() {
        orderTime = 3f;
    }

    public override void OnStateFixedUpdate() {
        base.OnStateFixedUpdate();

        Utility.LerpLookTowardsTarget(transform, new Vector3(manager.stall.position.x, transform.position.y, manager.stall.position.z));
        orderTime -= Time.fixedDeltaTime;

        if (orderTime <= 0) {
            animator.CrossFade("Moving To Seat", .25f);
        }
    }

    public override void OnStateExit() {
        base.OnStateExit();

        manager.customerQueue.RemoveAt(0);
    }

    public override void OnGUI() {
        base.OnGUI();

        GUILayout.BeginArea(new Rect(0f, 200f, 1000, 1000));
        GUILayout.Label($"<color='white'><size=25>{"AI Ordering Time: " + orderTime}</size></color>");
        GUILayout.EndArea();
    }
}