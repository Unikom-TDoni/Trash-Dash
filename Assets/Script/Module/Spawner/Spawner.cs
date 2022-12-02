using Random = UnityEngine.Random;
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Pool;

namespace Group8.TrashDash.Module.Spawner
{
    using Pool;

    public class Spawner : MonoBehaviour
    {
        public Action<SpawnObject> OnRelease;

        [SerializeField] protected PoolManager poolManager;
        [SerializeField] private SpawnPrefab spawnPrefab;

        protected GameObject[] obj;

        public int GetActiveSpawnObject() => poolManager.pools[spawnPrefab.prefab].CountActive;

        private void Awake()
        {
            if (!poolManager) poolManager = FindObjectOfType<PoolManager>();
        }

        protected virtual void Start()
        {
            OnRelease += Release;
            poolManager.Add(spawnPrefab.prefab, spawnPrefab.maxObjectInPool);
        }

        public virtual Coroutine InstantSpawn(Transform center, Vector3 offset = default, int amount = 1, Vector3 areaSize = default, bool randomizeRotation = false)
        {
            return StartCoroutine(InstantSpawnCoroutine(center, offset, amount, areaSize, randomizeRotation));
        }

        public virtual Coroutine RepeatSpawn(Transform center, float minInterval, float maxInterval, Vector3 offset = default, int amount = 1, Vector3 areaSize = default, bool randomizeRotation = false)
        {
            return StartCoroutine(RepeatSpawnCoroutine(center, minInterval, maxInterval, offset, amount, areaSize, randomizeRotation));
        }

        protected virtual IEnumerator InstantSpawnCoroutine(Transform center, Vector3 offset, int amount, Vector3 areaSize, bool randomizeRotation)
        {
            if (!SpawnCondition()) yield break;

            SpawnObjects(center, offset, amount, areaSize, randomizeRotation);

            yield return new WaitForFixedUpdate();

            AfterSpawn();
        }

        protected virtual IEnumerator RepeatSpawnCoroutine(Transform center, float minInterval, float maxInterval, Vector3 offset, int amount, Vector3 areaSize, bool randomizeRotation)
        {
            while (true)
            {
                if (!SpawnCondition()) yield break;

                yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));

                SpawnObjects(center, offset, amount, areaSize, randomizeRotation);

                yield return new WaitForFixedUpdate();

                AfterSpawn();
            }
        }

        protected void SpawnObjects(Transform center, Vector3 offset = default, int amount = 1, Vector3 areaSize = default, bool randomizeRotation = false)
        {
            if (center == null) return;

            obj = new GameObject[amount];
            for (int i = 0; i < amount; i++)
            {
                if (poolManager.pools[spawnPrefab.prefab].CountActive >= spawnPrefab.maxObjectInPool) break;

                Vector3 position = RandomSpawnPosition(center, offset, areaSize);
                Quaternion rotation = (randomizeRotation) ? Random.rotation : Quaternion.identity;

                obj[i] = poolManager.pools[spawnPrefab.prefab].Get();
                obj[i].transform.position = position;
                obj[i].transform.rotation = rotation;
                //obj[i].SetActive(false);
                //obj[i].transform.SetParent(transform);
                obj[i].GetComponent<SpawnObject>().spawner = this;
            }
        }

        protected virtual Vector3 RandomSpawnPosition(Transform center, Vector3 offset, Vector3 areaSize)
        {
            return center.position + offset + new Vector3(
                    Random.Range(-areaSize.x / 2, areaSize.x / 2),
                    Random.Range(-areaSize.y / 2, areaSize.y / 2),
                    Random.Range(-areaSize.z / 2, areaSize.z / 2));
        }

        protected virtual bool SpawnCondition()
        {
            return true;
        }

        protected virtual void AfterSpawn()
        {
            for (int i = 0; i < obj.Length; i++)
            {
                if (obj[i] == null) continue;

                obj[i].SetActive(true);
                obj[i] = null;
            }
        }

        private void Release(SpawnObject obj)
        {
            if (obj == null) return;
            poolManager.pools[spawnPrefab.prefab].Release(obj.gameObject);
        }

        private void OnDestroy()
        {
            OnRelease -= Release;
            poolManager.Remove(spawnPrefab.prefab);

            StopAllCoroutines();
        }
    }
}