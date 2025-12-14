using System;
using Modules.Core.Services.GameObjectFactory;
using Modules.Core.Services.Notifier;
using Modules.Core.Services.ResourceManagement;
using UnityEngine;

namespace Modules.Core.Services.UI
{
    public class UIContainerService : IUIContainerService
    {
        private const string UIContainerAssetPath = "UI/UIContainer";
        public event EventHandler UIContainerCreated;

        private readonly IAssetProvider _assetProvider;
        private readonly IGameObjectFactory _gameObjectFactory;

        private UIRoot _instance;
        
        public Transform WindowContainer => _instance.WindowRoot;
        public Transform DebugWidgetContainer => _instance.DebugPanelRoot;

        public UIContainerService(IAssetProvider assetProvider, IGameObjectFactory gameObjectFactory, IGameEventNotifier gameEventNotifier)
        {
            _assetProvider = assetProvider;
            _gameObjectFactory = gameObjectFactory;

            gameEventNotifier.GameSceneChanged += GameSceneChangedHandler;
        }

        public void CreateUIContainer()
        {
            if (_instance == null)
            {
                var prefab = _assetProvider.LoadAsset<UIRoot>(UIContainerAssetPath);
                _instance = _gameObjectFactory.Create(prefab);
                UIContainerCreated?.Invoke(this, EventArgs.Empty);
            }
        }

        private void GameSceneChangedHandler(object sender, string activeSceneName)
        {
            CreateUIContainer();
        }
    }
}