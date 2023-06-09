using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Group8.TrashDash.Spawner;
using UnityEngine.AI;
using Group8.TrashDash.TimeManager;

public class AIManager : MonoBehaviour {
    [SerializeField] NavMeshData data;

    [Space]
    [SerializeField] CustomerAI prefab;

    [field: Header("Durations")]
    [field: SerializeField] public float sittingDuration {get; private set;}

    [SerializeField] SpawnConfiguration[] spawnConfigurations;
    [SerializeField] Transform[] exitPoints;

    [Header("Spawn")]
    private TimeManager timeManager;
    [SerializeField] private int wavePerHour = 1;
    [SerializeField] private int customerPerWave = 5;
    
    [Space]
    public float trashSpawnMinInterval = 2f;
    public float trashSpawnMaxInterval = 5f;

    public HashSet<AIState> trashSpawnStateSet {get; private set;}

    public SpawnConfiguration GetSpawnConfiguration() {
        spawnIndex = (spawnIndex + 1) % spawnConfigurations.Length;
        return spawnConfigurations[spawnIndex];
    }
    
    public Vector3 exitPosition {
        get {
            exitIndex = (exitIndex + 1) % exitPoints.Length;
            return exitPoints[exitIndex].position;
        }
    }

    public List<GameObject> pointList;
    public TrashSpawner trashSpawner {get; private set;}

    int spawnIndex, exitIndex;

    void Awake() {
        foreach (var spawnConfig in spawnConfigurations) {
            spawnConfig.InitializeQueue();
        }

        trashSpawner = FindObjectOfType<TrashSpawner>();
        if (!trashSpawner) {
            Debug.LogError("No trash spawner found!");
        }

        timeManager = FindObjectOfType<TimeManager>();
        if (timeManager)
        {
            timeManager.WavePerHour = wavePerHour;
            timeManager.OnWaveTick += OnWaveSpawn;
        }
    }

    void Start() {
        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(data);
        pointList = GameObject.FindGameObjectsWithTag("AIPoint").ToList();
    }

    private void OnWaveSpawn()
    {
        for(int i = 0; i < customerPerWave; i++) Spawn();
    }

    public void Spawn() {
        CustomerAI spawnedAI = Instantiate(prefab);
        // spawnedAI.trashSpawner = trashSpawner;
        spawnedAI.aiManager = this;
        spawnedAI.spawnConfiguration = GetSpawnConfiguration();
        spawnedAI.transform.position = spawnedAI.spawnConfiguration.spawnPosition;
        spawnedAI.transform.rotation = spawnedAI.spawnConfiguration.spawnRotation;
        spawnedAI.GetComponent<NavMeshAgent>().enabled = true;
    }

    //#if UNITY_EDITOR
    //void OnGUI() {
    //  GUILayout.BeginArea(new Rect(0, 0, 500, 500));
    //  if (GUILayout.Button("Spawn")) {
    //      Spawn();
    //  }
    //  GUILayout.EndArea();
    //}
    //#endif
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
    public List<CustomerAI> customerQueue {get; private set;}

    public void InitializeQueue() {
        customerQueue = new List<CustomerAI>();
    }
}