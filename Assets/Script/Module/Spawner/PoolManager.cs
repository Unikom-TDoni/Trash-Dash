using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Group8.TrashDash.Module.Spawner
{
    public class PoolManager : MonoBehaviour
    {
        #region Singleton
        private static PoolManager instance;
        public static PoolManager Instance { get => instance; }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                return;
            }

            Debug.Log("There is more than one PoolManager detected.");
            Destroy(this);
        }
        #endregion

        public Dictionary<GameObject, ObjectPool> pools = new Dictionary<GameObject, ObjectPool>();

        public void Add(GameObject prefab)
        {
            if (pools.ContainsKey(prefab)) return;

            pools.Add(prefab, new ObjectPool(prefab));
        }

        public void Remove(GameObject prefab)
        {
            if (!pools.ContainsKey(prefab)) return;

            pools.Remove(prefab);
        }

        private void OnEnable()
        {
            foreach(ObjectPool pool in pools.Values)
            {
                pool.enabled = true;
            }
        }

        private void OnDisable()
        {
            foreach (ObjectPool pool in pools.Values)
            {
                pool.enabled = false;
            }
        }
    }
}
