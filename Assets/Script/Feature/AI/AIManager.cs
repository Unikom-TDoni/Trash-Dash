using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class AIManager : MonoBehaviour {
    [SerializeField] CustomerAI prefab;
    [SerializeField] Vector3 spawnPosition;
    [SerializeField] Vector3 spawnRotation;

    [field: SerializeField] public float speed {get; private set;} = 10f;
    [field: SerializeField] public Transform stall {get; private set;}
    [field: SerializeField] public Vector3 stallPosition {get; private set;}

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