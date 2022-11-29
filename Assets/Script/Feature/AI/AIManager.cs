using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Group8.TrashDash.Spawner;

public class AIManager : MonoBehaviour {
    [SerializeField] CustomerAI prefab;

    [field: Header("Durations")]
    [field: SerializeField] public float sittingDuration {get; private set;}

    [SerializeField] SpawnConfiguration[] spawnConfigurations;
    [SerializeField] Transform exitPoint;

    int spawnIndex;
    public SpawnConfiguration GetSpawnConfiguration() {
        spawnIndex = (spawnIndex + 1) % spawnConfigurations.Length;
        return spawnConfigurations[spawnIndex];
    }
    
    public Vector3 exitPosition => exitPoint.position;

    public List<GameObject> pointList;
    // public TrashSpawner trashSpawner {get; private set;}

    [field: SerializeField] public AIState[] trashSpawnState {get; private set;}

    void Awake() {
        foreach (var spawnConfig in spawnConfigurations) {
            spawnConfig.InitializeQueue();
        }

        // trashSpawner = FindObjectOfType<TrashSpawner>();
        // if (!trashSpawner) {
        //     Debug.LogError("No trash spawner found!");
        // }
    }

    void Start() {
        pointList = GameObject.FindGameObjectsWithTag("AIPoint").ToList();
    }

    public void Spawn() {
        CustomerAI spawnedAI = Instantiate(prefab);
        // spawnedAI.trashSpawner = trashSpawner;
        spawnedAI.aiManager = this;
    }

    void OnGUI() {
        GUILayout.BeginArea(new Rect(0, 0, 500, 500));
        if (GUILayout.Button("Spawn")) {
            Spawn();
        }
        GUILayout.EndArea();
    }
}

[System.Serializable]
public class SpawnConfiguration {
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform stallPoint;
    [SerializeField] Vector3 queueOffsetToStall;

    public Vector3 spawnPosition => spawnPoint.position;
    public Quaternion spawnRotation => spawnPoint.rotation;
    public Vector3 queuePosition => stallPoint.position + queueOffsetToStall;
    public Vector3 stallPosition => stallPoint.position;
    [field: SerializeField] public List<CustomerAI> customerQueue {get; private set;}

    public void InitializeQueue() {
        customerQueue = new List<CustomerAI>();
    }
}