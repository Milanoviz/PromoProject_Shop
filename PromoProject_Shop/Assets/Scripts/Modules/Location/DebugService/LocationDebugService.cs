using Modules.Core.Interfaces;
using Modules.Core.Services.DebugServices;
using Modules.Core.Services.DebugServices.Base;
using Modules.Core.Services.GameObjectFactory;
using Modules.Core.Services.PlayerData;
using Modules.Core.Services.ResourceManagement;
using Modules.Core.Services.UI;
using Modules.Core.Services.Wallet;
using Modules.Location.Resource;
using Modules.Location.State;

namespace Modules.Location.DebugService
{
    public class LocationDebugService : DebugModuleServiceBase<LocationServiceState>
    {
        private const string ResourceAssetPath = "Debug/Resource/LocationResource_debug";
        
        public LocationDebugService(IPlayerDataService playerDataService, IWalletService walletService, IAssetProvider assetProvider, IGameObjectFactory gameObjectFactory, IUIContainerService uiContainerService) : base(playerDataService, walletService, assetProvider, gameObjectFactory, uiContainerService)
        {
        }

        protected override IResourceModel CreateDebugResourceModel()
        {
            var resourceModel = _assetProvider.LoadAsset<LocationResourceScriptableObject>(ResourceAssetPath);
            return resourceModel;
        }

        protected override IDebugWidgetController CreateDebugWidgetController(DebugWidgetView widgetView, IResourceModel resourceModel)
        {
            return new LocationDebugWidgetController(widgetView, resourceModel, _playerDataService, _walletService);
        }
    }
}