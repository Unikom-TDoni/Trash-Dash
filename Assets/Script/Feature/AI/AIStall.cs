using System.Collections.Generic;
using UnityEngine;

public class AIStall : MonoBehaviour {
    [SerializeField] Vector3 queueOffset;
    public List<CustomerAI> customerQueue {get; private set;}

    public Vector3 queuePosition => transform.position + queueOffset;
    public Vector3 stallPosition => transform.position;

    void Awake() {
        customerQueue = new List<CustomerAI>();    
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(transform.position, .5f);
    }
}