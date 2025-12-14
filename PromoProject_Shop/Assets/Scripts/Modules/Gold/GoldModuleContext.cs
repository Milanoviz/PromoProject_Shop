using Modules.Core.Interfaces;
using Modules.Core.Services;
using Modules.Core.Services.DebugServices;
using Modules.Core.Services.GameObjectFactory;
using Modules.Core.Services.PlayerData;
using Modules.Core.Services.ResourceManagement;
using Modules.Core.Services.UI;
using Modules.Core.Services.Wallet;
using Modules.Gold.DebugService;
using Modules.Gold.States;
using Modules.Gold.Wallet;

namespace Modules.Gold
{
    public class GoldModuleContext : IModuleContext
    {
        public void Install(ServiceContainer serviceContainer)
        {
            var walletService =  serviceContainer.Resolve<IWalletService>();
            var assetProvider =  serviceContainer.Resolve<IAssetProvider>();
            var gameObjectFactory =  serviceContainer.Resolve<IGameObjectFactory>();
            var uiContainerService =  serviceContainer.Resolve<IUIContainerService>();
            
            var playerDataService = serviceContainer.Resolve<IPlayerDataService>();
            playerDataService.SetData(new GoldServiceState(20));
            
            var moduleWallet = new GoldWallet(playerDataService);
            walletService.Register(moduleWallet);
            
            var moduleDebugService = new GoldDebugService(playerDataService, walletService, assetProvider, gameObjectFactory, uiContainerService);

            var mainDebugService = serviceContainer.Resolve<IDebugService>();
            mainDebugService.Register(moduleDebugService);
        }
    }
}