using UnityEngine;

namespace Modules.Core.Services.ResourceManagement
{
    public class AssetProvider : IAssetProvider
    {
        public T LoadAsset<T>(string assetPath) where T : Object
        {
            var prefab = Resources.Load<T>(assetPath);
            return prefab;
        }
    }
}