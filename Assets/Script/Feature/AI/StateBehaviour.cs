using UnityEngine;

[System.Serializable]
public abstract class StateBehaviour {
    public virtual void Start(Transform transform) { }
    public virtual void OnStateEnter(Transform transform) { }
    public virtual void OnStateExit(Transform transform, StateBehaviour newState) { }
    public virtual void OnStateUpdate(Transform transform) { }
    public virtual void OnStateFixedUpdate(Transform transform) { }
    public virtual void OnStateLateUpdate(Transform transform) { }
    public virtual void OnGUI(Transform transform) { }
    public virtual void OnDrawGizmos(Transform transform) { }
}