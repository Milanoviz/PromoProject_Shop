using UnityEngine;

namespace Modules.Core.Services.GameObjectFactory
{
    public interface IGameObjectFactory : IService
    {
        GameObject Create(GameObject prefab);
        GameObject Create(GameObject prefab, Transform parent);
        T Create<T>(T prefab) where T : MonoBehaviour;
        T Create<T>(T prefab, Transform parent) where T : MonoBehaviour;
    }
}