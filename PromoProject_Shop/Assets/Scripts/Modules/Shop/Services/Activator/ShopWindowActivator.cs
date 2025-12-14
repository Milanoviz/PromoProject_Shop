using System;
using Modules.Core.Services.GameObjectFactory;
using Modules.Core.Services.ResourceManagement;
using Modules.Core.Services.UI;
using Modules.Shop.Configs;
using Modules.Shop.Helper;
using Modules.Shop.Services.Preview;
using Modules.Shop.Services.Shop;
using Modules.Shop.UI.ShopWindow.Controllers;
using Modules.Shop.UI.ShopWindow.Models;
using Modules.Shop.UI.ShopWindow.Views;
using UnityEngine.SceneManagement;

namespace Modules.Shop.Services.Activator
{
    public class ShopWindowActivator : IShopWindowActivator
    {
        private readonly IShopService _shopService;
        private readonly IShopPreviewService _shopPreviewService;
        private readonly IAssetProvider _assetProvider;
        private readonly IGameObjectFactory _gameObjectFactory;
        private readonly IUIContainerService _uiContainerService;
        
        private IShopWindowController _shopWindowController;

        public ShopWindowActivator(IShopService shopService,
            IShopPreviewService shopPreviewService,
            IAssetProvider assetProvider,
            IGameObjectFactory gameObjectFactory,
            IUIContainerService uiUIContainerService)
        {
            _shopService = shopService;
            _shopPreviewService = shopPreviewService;
            _assetProvider = assetProvider;
            _gameObjectFactory = gameObjectFactory;
            _uiContainerService = uiUIContainerService;

            _uiContainerService.UIContainerCreated += UIContainerChanged;
        }

        public void Activate()
        {
            var shopModel = CreateShopModel();
            var shopView = CreateShopWindowView();
            _shopWindowController = new ShopWindowController(shopModel, shopView, _shopService, _assetProvider, _shopPreviewService);
            _shopWindowController.Show();
        }
        
        private IShopModel CreateShopModel()
        {
            var shopConfig = _assetProvider.LoadAsset<ShopConfig>(ShopStaticData.ShopConfigAssetPath);
            return shopConfig;
        }

        private ShopWindowView CreateShopWindowView()
        {
            var prefab = _assetProvider.LoadAsset<ShopWindowView>(ShopStaticData.ShopWindowAssetPath);
            var instance = _gameObjectFactory.Create(prefab, _uiContainerService.WindowContainer);
            return instance;
        }
        
        private void UIContainerChanged(object sender, EventArgs e)
        {
            var canActivate = SceneManager.GetActiveScene().name != ShopStaticData.PreviewSceneName;
            if (canActivate)
            {
                Activate();
            }
        }
    }
}