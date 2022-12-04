using UnityEngine;

public class AISpawn : MonoBehaviour {
    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, .5f);
    }
}