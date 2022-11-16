using UnityEngine;

public class AIStateChanger : StateMachineBehaviour {
    [SerializeField] AIState targetState;

    AIStateMachine enemyStateMachine;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (enemyStateMachine == null) {
            enemyStateMachine = animator.GetComponent<CustomerAI>().stateMachine;
        }
        enemyStateMachine.ChangeState(targetState);
    }
}