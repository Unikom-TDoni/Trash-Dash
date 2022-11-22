using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class AIManager : MonoBehaviour {
    [SerializeField] CustomerAI prefab;
    [SerializeField] Transform spawnPoint;
    Vector3 spawnPosition => spawnPoint.position;
    Vector3 spawnRotation => spawnPoint.rotation.eulerAngles;

    [field: SerializeField] public float speed {get; private set;} = 10f;
    [field: SerializeField] public Transform stall {get; private set;}
    public Vector3 queuePosition => stall.position + queueOffset;

    [SerializeField] private Vector3 queueOffset;

    public List<CustomerAI> customerQueue {get; private set;} = new List<CustomerAI>();
    public List<GameObject> seatList {get; private set;}

    void Start() {
        seatList = GameObject.FindGameObjectsWithTag("Seat").ToList();
    }

    public void Spawn() {
        CustomerAI spawnedAI = Instantiate(prefab, spawnPosition, Quaternion.Euler(spawnRotation));
    }

    void OnGUI() {
        GUILayout.BeginArea(new Rect(0, 0, 500, 500));
        if (GUILayout.Button("Spawn")) {
            Spawn();
        }
        GUILayout.EndArea();
    }
}