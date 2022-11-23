using UnityEngine;
using UnityEngine.AI;

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

        Utility.LerpLookTowardsTarget(transform, new Vector3(manager.stallPosition.x, transform.position.y, manager.stallPosition.z));
        orderTime -= Time.fixedDeltaTime;

        if (orderTime <= 0) {
            if (manager.seatList.Count > 0) {
                transform.GetComponent<NavMeshAgent>().SetDestination(GetPoint(ref manager.seatList));
                animator.CrossFade("Moving To Seat", .25f);
            } else if (manager.targetPointList.Count > 0){
                transform.GetComponent<NavMeshAgent>().SetDestination(GetPoint(ref manager.targetPointList));
                animator.CrossFade("Moving To Point", .25f);
            }
        }
    }

    Vector3 GetPoint(ref System.Collections.Generic.List<GameObject> list) {
        int index = Random.Range(0, list.Count);
        Transform point = list[index].transform;
        list.RemoveAt(index);
        return point.position;
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