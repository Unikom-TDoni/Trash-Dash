using UnityEngine;
using Group8.TrashDash.Spawner;
using Group8.TrashDash.TrashBin;

public class CustomerAI : MonoBehaviour {
    public AIStateMachine stateMachine {get; private set;}
    public SpawnConfiguration spawnConfiguration;
    public Transform targetPoint;
    public TrashSpawner trashSpawner;
    public AIManager aiManager;

    Coroutine spawnCoroutine;

    void Start() {
        stateMachine = new AIStateMachine(transform);
        stateMachine.OnStateChange += OnStateChange;
        StartTrashCoroutine();
    }

    void OnStateChange(AIState newState) {
        // if (aiManager.trashSpawnState.)
    }

    public void StartTrashCoroutine() {
        if (spawnCoroutine != null)
            return;
        
        spawnCoroutine = trashSpawner.RepeatSpawn(new TrashBinTypes[] { TrashBinTypes.Organic }, transform, .1f, 1f, areaSize: new Vector3(5, 1, 5));
    }

    public void StopTrashCoroutine() {
        StopCoroutine(spawnCoroutine);
    }

    void Update() {
        stateMachine.Update();
    }

    void FixedUpdate() {
        stateMachine.FixedUpdate();
    }

    void LateUpdate() {
        stateMachine.LateUpdate();
    }

    #if UNITY_EDITOR
    void OnDrawGizmos() {
        stateMachine.OnDrawGizmos();
    }
    #endif

    void OnGUI() {
        stateMachine.OnGUI();
    }
}