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

        [SerializeField] private SpawnPrefab spawnPrefab;
        [SerializeField] private Vector3 size;
        [SerializeField] private Vector3 offset;
        [SerializeField] private int amount = 3;
        [SerializeField] private float interval = 5f;
        [SerializeField] private int maxSpawnedObject = 1;
        [SerializeField] private bool randomizeRotation;

        private int countObject = 0;
        protected GameObject[] obj;

        private void Start()
        {
            obj = new GameObject[amount];
            OnRelease += Release;
            PoolManager.Instance.Add(spawnPrefab.prefab, spawnPrefab.maxObjectInPool);

            StartCoroutine(Spawn());
        }

        protected virtual IEnumerator Spawn()
        {
            for (int i = 0; i < amount; i++)
            {
                if (countObject >= maxSpawnedObject) break;

                Vector3 position = transform.position + offset + new Vector3(
                    Random.Range(-size.x / 2, size.x / 2),
                    Random.Range(-size.y / 2, size.y / 2),
                    Random.Range(-size.z / 2, size.z / 2));
                Quaternion rotation = (randomizeRotation) ? Random.rotation : Quaternion.identity;

                obj[i] = PoolManager.Instance.pools[spawnPrefab.prefab].Get();
                obj[i].transform.position = position;
                obj[i].transform.rotation = rotation;
                //obj[i].SetActive(false);
                //obj[i].transform.SetParent(transform);
                obj[i].GetComponent<SpawnObject>().spawner = this;

                countObject++;
            }

            // Fix Trash Collider Late Position Change Bug
            yield return new WaitForFixedUpdate();

            AfterSpawn();

            yield return new WaitForSeconds(interval);

            RestartSpawn();
        }

        protected virtual void AfterSpawn() { }

        protected virtual void RestartSpawn()
        {
            for (int i = 0; i < obj.Length; i++)
            {
                if (obj[i] == null) continue;

                obj[i].SetActive(true);
                obj[i] = null;
            }

            StartCoroutine(Spawn());
        }

        private void Release(SpawnObject obj)
        {
            if (obj == null) return;
            PoolManager.Instance.pools[spawnPrefab.prefab].Release(obj.gameObject);

            countObject--;
        }

        private void OnDestroy()
        {
            OnRelease -= Release;
            PoolManager.Instance.Remove(spawnPrefab.prefab);

            StopAllCoroutines();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + offset, size);
        }
    }
}