using UnityEngine;

public class CustomerAI : MonoBehaviour {
    public AIStateMachine stateMachine {get; private set;}
    public Transform targetPoint;

    void Start() {
       stateMachine = new AIStateMachine(transform);
    }

    void Update() {
        stateMachine.Update();
    }

    void FixedUpdate() {
        stateMachine.FixedUpdate();
    }

    void LateUpdate() {
        stateMachine.LateUpdate();
    }

    #if UNITY_EDITOR
    void OnDrawGizmos() {
        stateMachine.OnDrawGizmos();
    }
    #endif

    void OnGUI() {
        stateMachine.OnGUI();
    }
}