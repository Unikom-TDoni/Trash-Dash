using UnityEngine;
using UnityEngine.AI;

public class AIOrderingState : StateBehaviour {
    CustomerAI customerAI;
    AIManager manager;

    float orderTime;

    public override void Start(Transform transform) {
        base.Start(transform);
        manager = GameObject.FindObjectOfType<AIManager>();
        customerAI = transform.GetComponent<CustomerAI>();
    }

    public override void OnStateEnter(Transform transform) {
        orderTime = 3f;
    }

    public override void OnStateFixedUpdate(Transform transform) {
        if (orderTime <= 0) {
            return;
        }

        Utility.LerpLookTowardsTarget(transform, new Vector3(customerAI.spawnConfiguration.stallPosition.x, transform.position.y, customerAI.spawnConfiguration.stallPosition.z));
        orderTime -= Time.fixedDeltaTime;

        if (orderTime <= 0) {
            if (manager.pointList.Count == 0) {
                Debug.LogWarning("No more points!");
                return;
            }
            transform.GetComponent<NavMeshAgent>().SetDestination(GetPoint(transform.GetComponent<CustomerAI>(), ref manager.pointList));
            transform.GetComponent<Animator>().CrossFade("Moving To Point", .25f);
        }
    }

    Vector3 GetPoint(CustomerAI customerAI, ref System.Collections.Generic.List<GameObject> list) {
        int index = Random.Range(0, list.Count);
        Transform point = list[index].transform;
        list.RemoveAt(index);
        customerAI.targetPoint = point;
        return point.position;
    }

    public override void OnStateExit(Transform transform, StateBehaviour newState) {
        customerAI.spawnConfiguration.customerQueue.RemoveAt(0);
    }

    public override void OnGUI(Transform transform) {
        GUILayout.BeginArea(new Rect(0f, 200f, 1000, 1000));
        GUILayout.Label($"<color='white'><size=25>{"AI Ordering Time: " + orderTime}</size></color>");
        GUILayout.EndArea();
    }
}