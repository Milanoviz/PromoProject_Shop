using Modules.Core.Services.GameObjectFactory;
using Modules.Core.Services.ResourceManagement;
using Modules.Core.Services.SceneLoader;
using Modules.Core.Services.UI;
using Modules.Shop.Helper;
using Modules.Shop.Services.Shop;
using Modules.Shop.UI.PreviewWindow.Controllers;
using Modules.Shop.UI.PreviewWindow.Views;
using Modules.Shop.UI.ShopWindow.Controllers;
using Modules.Shop.UI.ShopWindow.Models;
using UnityEngine.SceneManagement;

namespace Modules.Shop.Services.Preview
{
    public class ShopPreviewService : IShopPreviewService
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IShopService _shopService;
        private readonly IAssetProvider _assetProvider;
        private readonly IGameObjectFactory _gameObjectFactory;
        private readonly IUIContainerService _uiContainerService;

        private IShopPreviewWindowController _previewWindowController;

        private string _previousSceneName = string.Empty;

        public ShopPreviewService(ISceneLoader sceneLoader,
            IShopService shopService,
            IAssetProvider assetProvider,
            IGameObjectFactory gameObjectFactory,
            IUIContainerService uiContainerService)
        {
            _sceneLoader = sceneLoader;
            _shopService = shopService;
            _assetProvider = assetProvider;
            _gameObjectFactory = gameObjectFactory;
            _uiContainerService = uiContainerService;
        }

        public void ShowPreview(IShopWindowController shopWindowController, IShopLotModel lotModel)
        {
            var activeScene = SceneManager.GetActiveScene();
            _previousSceneName = activeScene.name;
            
            shopWindowController.Hide();
            _sceneLoader.Load(ShopStaticData.PreviewSceneName, () => ActivatePreviewWindow(lotModel));
        }

        public void ClosePreview()
        {
            if (_previewWindowController != null)
            {
                _previewWindowController.Hide();
                _previewWindowController = null;
            }
            
            _sceneLoader.Load(_previousSceneName);
        }

        private void ActivatePreviewWindow(IShopLotModel shopLotModel)
        {
            _uiContainerService.CreateUIContainer();

            var view = CreatPreviewWindowView();
           _previewWindowController = new ShopPreviewWindowController(shopLotModel, view, _shopService, this, _assetProvider);
           _previewWindowController.Show();
        }

        private IShopPreviewWindowView CreatPreviewWindowView()
        {
            var prefab = _assetProvider.LoadAsset<ShopPreviewWindowView>(ShopStaticData.ShopPreviewWindowAssetPath);
            var instance = _gameObjectFactory.Create(prefab, _uiContainerService.WindowContainer);
            return instance;
        }
    }
}