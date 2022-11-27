using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Group8.TrashDash.Module.Pool
{
    public class PoolManager : MonoBehaviour
    {
        public Dictionary<GameObject, ObjectPool<GameObject>> pools = new Dictionary<GameObject, ObjectPool<GameObject>>();

        public void Add(GameObject prefab, int maxObject)
        {
            if (pools.ContainsKey(prefab)) return;

            pools.Add(prefab, new ObjectPool<GameObject>(
                createFunc: () => Instantiate(prefab),
                actionOnGet: (obj) => obj.SetActive(true),
                actionOnRelease: (obj) => obj.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj),
                collectionCheck: false,
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
