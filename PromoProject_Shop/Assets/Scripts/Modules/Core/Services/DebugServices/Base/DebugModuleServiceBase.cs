using Modules.Core.Interfaces;
using Modules.Core.Services.GameObjectFactory;
using Modules.Core.Services.PlayerData;
using Modules.Core.Services.ResourceManagement;
using Modules.Core.Services.UI;
using Modules.Core.Services.Wallet;

namespace Modules.Core.Services.DebugServices.Base
{
    public abstract class DebugModuleServiceBase<TServiceState> : IModuleDebugService where TServiceState : class, IGameServiceState
    {
        private const string DebugWidgetAssetPath = "Debug/DebugWidgetView";
        
        protected readonly IPlayerDataService _playerDataService;
        protected readonly IWalletService _walletService;
        protected readonly IAssetProvider _assetProvider;
        private readonly IGameObjectFactory _gameObjectFactory;
        private readonly IUIContainerService _uiContainerService;

        private IDebugWidgetController _widgetController;

        protected DebugModuleServiceBase(
            IPlayerDataService playerDataService,
            IWalletService walletService,
            IAssetProvider assetProvider,
            IGameObjectFactory gameObjectFactory,
            IUIContainerService uiContainerService)
        {
            _playerDataService = playerDataService;
            _walletService = walletService;
            _assetProvider = assetProvider;
            _gameObjectFactory = gameObjectFactory;
            _uiContainerService = uiContainerService;
        }

        public void Activate()
        {
            CreateDebugWidget();
        }

        public void Deactivate()
        {
            _widgetController.Deactivate();
        }

        protected abstract IResourceModel CreateDebugResourceModel();
        protected abstract IDebugWidgetController CreateDebugWidgetController(DebugWidgetView widgetView, IResourceModel resourceModel);
        
        private void CreateDebugWidget()
        {
            var view = CreateWidgetView();
            var resourceModel = CreateDebugResourceModel();
            var widgetController = CreateDebugWidgetController(view, resourceModel);
            widgetController.Activate();
        }
        
        private DebugWidgetView CreateWidgetView()
        {
            var prefab = _assetProvider.LoadAsset<DebugWidgetView>(DebugWidgetAssetPath);
            var instance = _gameObjectFactory.Create(prefab, _uiContainerService.DebugWidgetContainer);
            return instance;
        }
    }
}