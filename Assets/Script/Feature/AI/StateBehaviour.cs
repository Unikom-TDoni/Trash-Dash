using UnityEngine;

[System.Serializable]
public abstract class StateBehaviour {
    public virtual void Start(Transform transform) { }
    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }
    public virtual void OnStateExit(StateBehaviour newState) { }
    public virtual void OnStateUpdate() { }
    public virtual void OnStateFixedUpdate() { }
    public virtual void OnStateLateUpdate() { }
    public virtual void OnGUI() { }
    public virtual void OnDrawGizmos() { }
}