using Random = UnityEngine.Random;
using System;
using UnityEngine;
using System.Collections;

namespace Group8.TrashDash.Module.Spawner
{
    public abstract class Spawner : MonoBehaviour
    {
        public Action<SpawnObject> OnRelease;

        [SerializeField] private GameObject prefab;
        [SerializeField] private Vector3 size;
        [SerializeField] private Vector3 offset;
        [SerializeField] private int amount = 3;
        [SerializeField] private float interval = 5f;
        [SerializeField] private int maxSpawnedObject = 10;

        protected GameObject[] obj;
        private void Start()
        {
            obj = new GameObject[amount];
            OnRelease += Release;
            PoolManager.Instance.Add(prefab);

            StartCoroutine(Spawn());
        }

        protected virtual IEnumerator Spawn()
        {
            for (int i = 0; i < amount; i++)
            {
                if (PoolManager.Instance.pools[prefab].Count >= maxSpawnedObject) break;
                
                Vector3 position = transform.position + new Vector3(
                    Random.Range(-size.x / 2, size.x / 2),
                    Random.Range(-size.y / 2, size.y / 2),
                    Random.Range(-size.z / 2, size.z / 2));
                Quaternion rotation = Quaternion.identity;

                obj[i] = PoolManager.Instance.pools[prefab].Spawn(position, rotation);
                //obj[i].transform.SetParent(transform);
                obj[i].GetComponent<SpawnObject>().spawner = this;
            }

            yield return new WaitForSeconds(interval);

            StartCoroutine(Spawn());
        }

        private void Release(SpawnObject obj)
        {
            PoolManager.Instance.pools[prefab].Release(obj.gameObject);
        }

        private void OnDestroy()
        {
            OnRelease -= Release;
            PoolManager.Instance.Remove(prefab);

            StopAllCoroutines();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + offset, size);
        }
    }
}