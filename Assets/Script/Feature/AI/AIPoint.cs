using UnityEngine;

public class AIPoint : MonoBehaviour {
    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, .5f);
    }
}