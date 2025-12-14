using UnityEngine;

namespace Modules.Core.Services.GameObjectFactory
{
    public class GameObjectFactory : IGameObjectFactory
    {
        public GameObject Create(GameObject prefab)
        {
            if (prefab == null)
            {
                Debug.LogError($"Create gameObject failed. Prefab is null.");
                return null;
            }

            return Object.Instantiate(prefab);
        }

        public GameObject Create(GameObject prefab, Transform parent)
        {
            if (prefab == null)
            {
                Debug.LogError($"Create gameObject failed. Prefab is null.");
                return null;
            }
            
            if (parent == null)
            {
                Debug.LogWarning("Create gameObject process. Parent transform is null.");
            }

            return Object.Instantiate(prefab);
        }

        public T Create<T>(T prefab) where T : MonoBehaviour
        {
            if (prefab == null)
            {
                Debug.LogError($"Create gameObject {typeof(T)} failed. Prefab is null.");
                return null;
            }
            
            var instance = Object.Instantiate(prefab);
            return instance.GetComponent<T>();
        }

        public T Create<T>(T prefab, Transform parent) where T : MonoBehaviour
        {
            if (prefab == null)
            {
                Debug.LogError($"Create gameObject failed {typeof(T)}. Prefab is null.");
                return null;
            }
            
            if (parent == null)
            {
                Debug.LogWarning("Create gameObject process. Parent transform is null.");
            }

            var instance = Object.Instantiate(prefab, parent);
            return instance.GetComponent<T>();
        }
    }
}