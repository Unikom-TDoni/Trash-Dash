using UnityEngine;
using UnityEngine.AI;

public class AIMovingToSeatState : StateBehaviour {
    AIManager manager;
    NavMeshAgent agent;
    Transform transform;

    public override void Start(Transform transform) {
        manager = GameObject.FindObjectOfType<AIManager>();
        agent = transform.GetComponent<NavMeshAgent>();
        this.transform = transform;
    }

    public override void OnStateEnter() {
        
    }

    public override void OnStateFixedUpdate() {
        base.OnStateFixedUpdate();

        if (Vector3.SqrMagnitude(agent.destination - transform.position) <= Mathf.Epsilon) {
            transform.GetComponent<Animator>().CrossFade("Sitting", .25f);
        }
    }

    public override void OnStateExit() {
        base.OnStateExit();
    }
}