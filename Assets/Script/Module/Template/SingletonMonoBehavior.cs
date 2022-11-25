using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lncodes.Module.Unity.Template
{
    public abstract class SingletonMonoBehavior<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; protected set; } = default;

        protected SingletonMonoBehavior() { }

        protected virtual void Awake()
        {
            if (Instance is not null)
            {
                Destroy(this);
                return;
            }

            Instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }
}

