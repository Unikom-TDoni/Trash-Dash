using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Group8.TrashDash.Module.Pool
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

        public Dictionary<GameObject, ObjectPool<GameObject>> pools = new Dictionary<GameObject, ObjectPool<GameObject>>();

        public void Add(GameObject prefab, int maxObject)
        {
            if (pools.ContainsKey(prefab)) return;

            pools.Add(prefab, new ObjectPool<GameObject>(
                createFunc: () => Instantiate(prefab),
                actionOnGet: (obj) => obj.SetActive(false),
                actionOnRelease: (obj) => obj.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj),
                collectionCheck: true,
                maxSize: maxObject));
        }

        public void Remove(GameObject prefab)
        {
            if (!pools.ContainsKey(prefab)) return;

            pools[prefab].Clear();
            pools.Remove(prefab);
        }
    }
}
