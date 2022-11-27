using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class AIStateMachine {
    [SerializeField] TextMeshPro stateText;

    public StateBehaviour currentStateBehaviour {get; private set;}
    AIState currentState;
    Dictionary<AIState, StateBehaviour> stateDictionary;

    public System.Action<AIState> OnStateChange;

    public AIStateMachine(Transform transform) {
        stateDictionary = new Dictionary<AIState, StateBehaviour>() {
            {AIState.InQueue, new AIQueueingState()},
            {AIState.OrderingFood, new AIOrderingState()},
            {AIState.MovingToPoint, new AIMovingToPointState()},
            {AIState.Sitting, new AISittingState()},
            {AIState.WalkingAway, new AIWalkingAwayState()},
        };
        foreach (KeyValuePair<AIState, StateBehaviour> state in stateDictionary) {
            state.Value.Start(transform);
        }
    }

    public void Update() {
        currentStateBehaviour?.OnStateUpdate();
    }

    public void FixedUpdate() {
        currentStateBehaviour?.OnStateFixedUpdate();
    }

    public void LateUpdate() {
        currentStateBehaviour?.OnStateLateUpdate();
    }

    #if UNITY_EDITOR
    public void OnDrawGizmos() {
        currentStateBehaviour?.OnDrawGizmos();
    }
    #endif

    public void ChangeState(AIState aiState) {
        currentStateBehaviour?.OnStateExit();
        currentStateBehaviour?.OnStateExit(stateDictionary[aiState]);

        currentStateBehaviour = stateDictionary[aiState];
        currentState = aiState;

        currentStateBehaviour.OnStateEnter();

        if (OnStateChange != null) {
            OnStateChange(aiState);
        }
    }

    public void OnGUI() {
        currentStateBehaviour?.OnGUI();
    }
}