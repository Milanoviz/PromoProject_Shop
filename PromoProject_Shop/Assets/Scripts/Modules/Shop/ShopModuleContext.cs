using Modules.Core.Interfaces;
using Modules.Core.Services;
using Modules.Core.Services.GameObjectFactory;
using Modules.Core.Services.ResourceManagement;
using Modules.Core.Services.SceneLoader;
using Modules.Core.Services.UI;
using Modules.Core.Services.Wallet;
using Modules.Shop.Services.Activator;
using Modules.Shop.Services.Preview;
using Modules.Shop.Services.Shop;

namespace Modules.Shop
{
    public class ShopModuleContext : IModuleContext
    {
        private IShopService _shopService;
        private IShopPreviewService _shopPreviewService;
        private ShopWindowActivator _shopWindowActivator;

        public void Install(ServiceContainer serviceContainer)
        {
            var walletService = serviceContainer.Resolve<IWalletService>();
            var coroutineRunner = serviceContainer.Resolve<ICoroutineRunner>();
            var assetProvider = serviceContainer.Resolve<IAssetProvider>();
            var gameObjectFactory = serviceContainer.Resolve<IGameObjectFactory>();
            var sceneLoader = serviceContainer.Resolve<ISceneLoader>();
            var uiContainerService = serviceContainer.Resolve<IUIContainerService>();

            _shopService = new ShopService(walletService, coroutineRunner);
            _shopPreviewService = new ShopPreviewService(sceneLoader, _shopService, assetProvider, gameObjectFactory, uiContainerService);
            _shopWindowActivator = new ShopWindowActivator(_shopService, _shopPreviewService, assetProvider, gameObjectFactory, uiContainerService);
        }
    }
}