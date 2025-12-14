using Modules.Core.Interfaces;
using Modules.Core.Services.DebugServices;
using Modules.Core.Services.DebugServices.Base;
using Modules.Core.Services.GameObjectFactory;
using Modules.Core.Services.PlayerData;
using Modules.Core.Services.ResourceManagement;
using Modules.Core.Services.UI;
using Modules.Core.Services.Wallet;
using Modules.Health.Resource;
using Modules.Health.State;

namespace Modules.Health.DebugService
{
    public class HealthDebugService : DebugModuleServiceBase<HealthServiceState>
    {
        private const string ResourceAssetPath = "Debug/Resource/HealthResource_debug";
        
        public HealthDebugService(IPlayerDataService playerDataService, IWalletService walletService, IAssetProvider assetProvider, IGameObjectFactory gameObjectFactory, IUIContainerService uiContainerService) : base(playerDataService, walletService, assetProvider, gameObjectFactory, uiContainerService)
        {
        }

        protected override IResourceModel CreateDebugResourceModel()
        {
            var resourceModel = _assetProvider.LoadAsset<HealthResourceScriptableObject>(ResourceAssetPath);
            return resourceModel;
        }

        protected override IDebugWidgetController CreateDebugWidgetController(DebugWidgetView widgetView, IResourceModel resourceModel)
        {
            return new HealthDebugWidgetController(widgetView, resourceModel, _playerDataService, _walletService);
        }
    }
}