using Modules.Core.Services.GameObjectFactory;
using Modules.Core.Services.ResourceManagement;

namespace Modules.Core.InternalServices.LoadingScreen
{
    internal class LoadingScreenService : ILoadingScreenService
    {
        private const string LoadScreeViewAssetPath = "UI/Windows/LodingScreenView";
        
        private readonly IAssetProvider _assetProvider;
        private readonly IGameObjectFactory _gameObjectFactory;
        
        private LoadingScreenView _view;

        public LoadingScreenService(IAssetProvider assetProvider, IGameObjectFactory gameObjectFactory)
        {
            _assetProvider = assetProvider;
            _gameObjectFactory = gameObjectFactory;
            
            InitializeView();
        }

        public void Show()
        {
            _view.Show();
        }

        public void Hide()
        {
            _view.Hide();
        }

        private void InitializeView()
        {
            var prefab = _assetProvider.LoadAsset<LoadingScreenView>(LoadScreeViewAssetPath);
           _view = _gameObjectFactory.Create(prefab);
        }
    }
}