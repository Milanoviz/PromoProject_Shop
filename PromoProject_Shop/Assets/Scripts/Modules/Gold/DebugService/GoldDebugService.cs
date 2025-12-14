using Modules.Core.Interfaces;
using Modules.Core.Services.DebugServices;
using Modules.Core.Services.DebugServices.Base;
using Modules.Core.Services.GameObjectFactory;
using Modules.Core.Services.PlayerData;
using Modules.Core.Services.ResourceManagement;
using Modules.Core.Services.UI;
using Modules.Core.Services.Wallet;
using Modules.Gold.Resource;
using Modules.Gold.States;

namespace Modules.Gold.DebugService
{
    public class GoldDebugService : DebugModuleServiceBase<GoldServiceState>
    {
        private const string ResourceAssetPath = "Debug/Resource/GoldResource_debug";
        
        public GoldDebugService(IPlayerDataService playerDataService, IWalletService walletService, IAssetProvider assetProvider, IGameObjectFactory gameObjectFactory, IUIContainerService uiContainerService) : base(playerDataService, walletService, assetProvider, gameObjectFactory, uiContainerService)
        {
        }

        protected override IResourceModel CreateDebugResourceModel()
        {
            var resourceModel = _assetProvider.LoadAsset<GoldResourceScriptableObject>(ResourceAssetPath);
            return resourceModel;
        }

        protected override IDebugWidgetController CreateDebugWidgetController(DebugWidgetView widgetView, IResourceModel resourceModel)
        {
            return new GoldDebugWidgetController(widgetView, resourceModel, _playerDataService, _walletService);
        }
    }
}