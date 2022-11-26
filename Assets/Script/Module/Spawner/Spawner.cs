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

        [SerializeField] private PoolManager poolManager;
        [SerializeField] private SpawnPrefab spawnPrefab;

        protected GameObject[] obj;

        protected virtual void Start()
        {
            OnRelease += Release;
            poolManager.Add(spawnPrefab.prefab, spawnPrefab.maxObjectInPool);
        }

        public void InstantSpawn(Transform center, Vector3 offset = default, int amount = 1, Vector3 areaSize = default, bool randomizeRotation = false)
        {
            StartCoroutine(InstantSpawnCoroutine(center, offset, amount, areaSize, randomizeRotation));
        }

        public void RepeatSpawn(Transform center, float interval, Vector3 offset = default, int amount = 1, Vector3 areaSize = default, bool randomizeRotation = false)
        {
            StartCoroutine(RepeatSpawnCoroutine(center, interval, offset, amount, areaSize, randomizeRotation));
        }

        protected virtual IEnumerator InstantSpawnCoroutine(Transform center, Vector3 offset, int amount, Vector3 areaSize, bool randomizeRotation)
        {
            SpawnObjects(center, offset, amount, areaSize, randomizeRotation);

            yield return new WaitForFixedUpdate();

            AfterSpawn();
        }

        protected virtual IEnumerator RepeatSpawnCoroutine(Transform center, float interval, Vector3 offset, int amount, Vector3 areaSize, bool randomizeRotation)
        {
            yield return new WaitForSeconds(interval);

            SpawnObjects(center, offset, amount, areaSize, randomizeRotation);

            yield return new WaitForFixedUpdate();

            AfterSpawn();

            StartCoroutine(RepeatSpawnCoroutine(center, interval, offset, amount, areaSize, randomizeRotation));
        }

        protected void SpawnObjects(Transform center, Vector3 offset = default, int amount = 1, Vector3 areaSize = default, bool randomizeRotation = false)
        {
            if (center == null) return;

            obj = new GameObject[amount];
            for (int i = 0; i < amount; i++)
            {
                if (poolManager.pools[spawnPrefab.prefab].CountActive >= spawnPrefab.maxObjectInPool) break;

                Vector3 position = center.position + offset + new Vector3(
                    Random.Range(-areaSize.x / 2, areaSize.x / 2),
                    Random.Range(-areaSize.y / 2, areaSize.y / 2),
                    Random.Range(-areaSize.z / 2, areaSize.z / 2));
                Quaternion rotation = (randomizeRotation) ? Random.rotation : Quaternion.identity;

                obj[i] = poolManager.pools[spawnPrefab.prefab].Get();
                obj[i].transform.position = position;
                obj[i].transform.rotation = rotation;
                //obj[i].SetActive(false);
                //obj[i].transform.SetParent(transform);
                obj[i].GetComponent<SpawnObject>().spawner = this;
            }
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