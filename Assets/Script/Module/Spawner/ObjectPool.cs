using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Group8.TrashDash.Module.Spawner
{
    public class ObjectPool
    {
        public bool enabled = true;

        private GameObject prototype;
        private Stack<GameObject> stack;
        private int count;
        public int Count { get { return count; } }

        public ObjectPool(GameObject _prototype)
        {
            prototype = _prototype;
            stack = new Stack<GameObject>();
            count = 0;
        }

        private GameObject Create(Vector3 position = default, Quaternion rotation = default)
        {
            count++;
            return Object.Instantiate(prototype, position, rotation);
        }

        public GameObject Spawn(Vector3 position = default, Quaternion rotation = default)
        {
            if (stack.Count == 0 || enabled == false) return Create(position, rotation);

            GameObject obj = stack.Pop();
            obj.transform.position = position;
            obj.transform.rotation = rotation;

            obj.SetActive(true);
            count++;

            return obj;
        }

        public void Release(GameObject obj)
        {
            if (enabled == false)
            {
                Object.Destroy(obj);
                return;
            }
            obj.SetActive(false);
            stack.Push(obj);
            count--;
        }

        public void Clear(bool destroyItems)
        {
            if (destroyItems)
            {
                foreach (GameObject item in stack)
                {
                    Object.Destroy(item);
                }
            }
            stack.Clear();
            count = 0;
        }
    }
}