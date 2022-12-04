using UnityEngine;
using Group8.TrashDash.Spawner;
using Group8.TrashDash.TrashBin;

public class CustomerAI : MonoBehaviour {
    public AIStateMachine stateMachine {get; private set;}
    [HideInInspector] public Transform targetPoint;
    [HideInInspector] public AIManager aiManager;
    [HideInInspector] public SpawnConfiguration spawnConfiguration;

    Coroutine spawnCoroutine;

    void Start() {
        stateMachine = new AIStateMachine(transform);
        stateMachine.OnStateChange += OnStateChange;
    }

    void OnStateChange(AIState newState) {
        SpawnTrashes(AIConfiguration.trashSpawnStateSet.Contains(newState));
    }

    void SpawnTrashes(bool spawnTrashes) {
        if (spawnTrashes && spawnCoroutine == null) {
            spawnCoroutine = aiManager.trashSpawner.RepeatSpawn(transform, aiManager.trashSpawnMinInterval, aiManager.trashSpawnMaxInterval, areaSize: new Vector3(2f, 1f, 2f), offset: Vector3.up, randomizeRotation: true);
        } else if (spawnCoroutine != null){
            aiManager.trashSpawner.StopCoroutine(spawnCoroutine);
        }
    }

    void Update() {
        stateMachine.Update(transform);
    }

    void FixedUpdate() {
        stateMachine.FixedUpdate(transform);
    }

    void LateUpdate() {
        stateMachine.LateUpdate(transform);
    }

    #if UNITY_EDITOR
    void OnDrawGizmos() {
        stateMachine.OnDrawGizmos(transform);
    }
    #endif

    //void OnGUI() {
    //    stateMachine.OnGUI(transform);
    //}
}