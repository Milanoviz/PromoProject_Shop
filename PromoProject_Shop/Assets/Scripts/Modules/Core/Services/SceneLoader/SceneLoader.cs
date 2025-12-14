using System;
using System.Collections;
using Modules.Core.Services.Notifier;
using UnityEngine.SceneManagement;

namespace Modules.Core.Services.SceneLoader
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IGameEventNotifier _gameEventNotifier;

        public SceneLoader(ICoroutineRunner coroutineRunner, IGameEventNotifier gameEventNotifier)
        {
            _coroutineRunner = coroutineRunner;
            _gameEventNotifier = gameEventNotifier;
        }
        
        public void Load(string name, Action onLoaded = null)
        {
            _coroutineRunner.StartCoroutine(LoadSceneCoroutine(name, onLoaded));
        }
        
        private IEnumerator LoadSceneCoroutine(string nextScene, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                yield break;
            }
      
            var sceneAsyncOperation = SceneManager.LoadSceneAsync(nextScene);

            while (!sceneAsyncOperation.isDone)
                yield return null;
      
            _gameEventNotifier.NotifyGameSceneChanged(nextScene);
            onLoaded?.Invoke();
        }
    }
}