using UnityEngine;

namespace Modules.Core.Services.ResourceManagement
{
    public interface IAssetProvider : IService
    {
        T LoadAsset<T>(string assetPath) where T : Object;
    }
}