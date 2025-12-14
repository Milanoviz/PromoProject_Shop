using System;

namespace Modules.Core.Services.SceneLoader
{
    public interface ISceneLoader : IService
    {
        void Load(string name, Action onLoaded = null);
    }
}