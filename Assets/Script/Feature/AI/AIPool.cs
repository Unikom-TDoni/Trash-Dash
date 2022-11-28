using UnityEngine;
using UnityEngine.Pool;

[System.Serializable]
public class AIPool {
    [SerializeField] CustomerAI aiPrefab;
    
    ObjectPool<CustomerAI> aiPool;

    public void Initialize() {
        aiPool = new ObjectPool<CustomerAI>(CreateAI, OnTakeAIFromPool, OnReturnAIToPool, OnDestroyAIFromPool);
    }

    CustomerAI CreateAI() {
        CustomerAI ai = GameObject.Instantiate(aiPrefab);
        return ai;
    }

    void OnDestroyAIFromPool(CustomerAI ai) {
        GameObject.Destroy(ai.gameObject);
    }

    void OnTakeAIFromPool(CustomerAI ai) {
        ai.gameObject.SetActive(true);
    }

    void OnReturnAIToPool(CustomerAI ai) { 
        ai.gameObject.SetActive(false);
    }

    public CustomerAI GetFromPool() {
        return aiPool.Get();
    }

    public void ReleaseToPool(CustomerAI ai) {
        aiPool.Release(ai);
    }

    public void Dispose() {
        CustomerAI[] ais = GameObject.FindObjectsOfType<CustomerAI>();
        for (int i = ais.Length - 1; i >= 0; i--) {
            GameObject.Destroy(ais[i].gameObject);
        }

        if (aiPool != null)
            aiPool.Dispose();

        aiPool = null;
    }
}