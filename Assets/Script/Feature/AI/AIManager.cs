using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class AIManager : MonoBehaviour {
    [SerializeField] CustomerAI prefab;

    [field: Header("Durations")]
    [field: SerializeField] public float sittingDuration {get; private set;}

    [Header("Positions")]
    [SerializeField] Transform stall;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform queuePoint;
    [SerializeField] Transform exitPoint;
    
    public Vector3 stallPosition => stall.position;
    public Vector3 queuePosition => queuePoint.position;
    public Vector3 exitPosition => exitPoint.position;

    Vector3 spawnPosition => spawnPoint.position;
    Vector3 spawnRotation => spawnPoint.rotation.eulerAngles;

    public List<CustomerAI> customerQueue;
    public List<GameObject> seatList;
    public List<GameObject> targetPointList;

    void Start() {
        seatList = GameObject.FindGameObjectsWithTag("Seat").ToList();
        targetPointList = GameObject.FindGameObjectsWithTag("AITargetPoint").ToList();
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