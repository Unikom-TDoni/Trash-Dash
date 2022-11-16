using UnityEngine;
using UnityEngine.AI;

public class AIMovingToSeatState : StateBehaviour {
    AIManager manager;
    NavMeshAgent agent;
    GameObject seat;
    Animator animator;
    Transform transform;

    public override void Start(Transform transform) {
        manager = GameObject.FindObjectOfType<AIManager>();
        agent = transform.GetComponent<NavMeshAgent>();
        animator = transform.GetComponent<Animator>();
        this.transform = transform;
    }

    public override void OnStateEnter() {
        int seatIndex = Random.Range(0, manager.seatList.Count);
        seat = manager.seatList[seatIndex];
        manager.seatList.RemoveAt(seatIndex);

        agent.SetDestination(seat.transform.position);
    }

    public override void OnStateFixedUpdate() {
        base.OnStateFixedUpdate();

        if (Vector3.SqrMagnitude(agent.destination - transform.position) <= Mathf.Epsilon) {
            animator.CrossFade("Sitting", .25f);
        }
    }

    public override void OnStateExit() {
        base.OnStateExit();
    }
}