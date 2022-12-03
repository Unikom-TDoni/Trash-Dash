using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class AIStateMachine {
    [SerializeField] TextMeshPro stateText;

    public StateBehaviour currentStateBehaviour {get; private set;}
    public AIState currentState {get; private set;}
    Dictionary<AIState, StateBehaviour> stateDictionary;

    public System.Action<AIState> OnStateChange;

    public AIStateMachine(Transform transform) {
        stateDictionary = new Dictionary<AIState, StateBehaviour>() {
            {AIState.InQueue, new AIQueueingState()},
            {AIState.OrderingFood, new AIOrderingState()},
            {AIState.MovingToPoint, new AIMovingToPointState()},
            {AIState.Sitting, new AISittingState()},
            {AIState.WalkingAway, new AIWalkingAwayState()},
            {AIState.StandingUp, new AIStandingUpState()},
        };
        foreach (KeyValuePair<AIState, StateBehaviour> state in stateDictionary) {
            state.Value.Start(transform);
        }
    }

    public void Update(Transform transform) {
        currentStateBehaviour?.OnStateUpdate(transform);
    }

    public void FixedUpdate(Transform transform) {
        currentStateBehaviour?.OnStateFixedUpdate(transform);
    }

    public void LateUpdate(Transform transform) {
        currentStateBehaviour?.OnStateLateUpdate(transform);
    }

    #if UNITY_EDITOR
    public void OnDrawGizmos(Transform transform) {
        currentStateBehaviour?.OnDrawGizmos(transform);
    }
    #endif

    public void ChangeState(Transform transform, AIState aiState) {
        currentStateBehaviour?.OnStateExit(transform, stateDictionary[aiState]);

        currentStateBehaviour = stateDictionary[aiState];
        currentState = aiState;

        currentStateBehaviour.OnStateEnter(transform);

        if (OnStateChange != null) {
            OnStateChange(aiState);
        }
    }

    public void OnGUI(Transform transform) {
        currentStateBehaviour?.OnGUI(transform);
    }
}