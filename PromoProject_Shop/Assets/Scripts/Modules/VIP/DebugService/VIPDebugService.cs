using Modules.Core.Interfaces;
using Modules.Core.Services.DebugServices;
using Modules.Core.Services.DebugServices.Base;
using Modules.Core.Services.GameObjectFactory;
using Modules.Core.Services.PlayerData;
using Modules.Core.Services.ResourceManagement;
using Modules.Core.Services.UI;
using Modules.Core.Services.Wallet;
using Modules.VIP.Resource;
using Modules.VIP.State;

namespace Modules.VIP.DebugService
{
    public class VIPDebugService : DebugModuleServiceBase<VIPServiceState>
    {
        private const string ResourceAssetPath = "Debug/Resource/VIPResource_debug";
        
        public VIPDebugService(IPlayerDataService playerDataService, IWalletService walletService, IAssetProvider assetProvider, IGameObjectFactory gameObjectFactory, IUIContainerService uiContainerService) : base(playerDataService, walletService, assetProvider, gameObjectFactory, uiContainerService)
        {
        }

        protected override IResourceModel CreateDebugResourceModel()
        {
            var resourceModel = _assetProvider.LoadAsset<VIPResourceScriptableObject>(ResourceAssetPath);
            return resourceModel;
        }

        protected override IDebugWidgetController CreateDebugWidgetController(DebugWidgetView widgetView, IResourceModel resourceModel)
        {
            return new VIPDebugWidgetController(widgetView, resourceModel, _playerDataService, _walletService);
        }
    }
}