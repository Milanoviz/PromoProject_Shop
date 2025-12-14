using System;
using System.Collections.Generic;
using Modules.Core.Interfaces;
using Modules.Core.Services.PlayerData;
using Modules.Core.Services.Wallet;

namespace Modules.Core.Services.DebugServices.Base
{
    public abstract class DebugWidgetControllerBase<TServiceState> : IDebugWidgetController where TServiceState : class, IGameServiceState
    {
        private readonly IDebugWidgetView _view;
        private readonly IResourceModel _resourceModel;
        private readonly IPlayerDataService _playerDataService;
        private readonly IWalletService _walletService;
        
        protected DebugWidgetControllerBase(
            IDebugWidgetView view,
            IResourceModel resourceModel,
            IPlayerDataService playerDataService,
            IWalletService walletService)
        {
            _view = view;
            _resourceModel = resourceModel;
            _playerDataService = playerDataService;
            _walletService = walletService;
        }
        
        public void Activate()
        {
            _walletService.DataChanged += WalletDataChangedHandler;
            _view.AddResourceButtonClicked += AddResourceButtonClickedHandler;
            _view.SetTitleText(GetTitleText());
            UpdateView();
        }
        

        public void Deactivate()
        {
            _walletService.DataChanged -= WalletDataChangedHandler;
            _view.AddResourceButtonClicked -= AddResourceButtonClickedHandler;
        }

        protected abstract string GetValueText(TServiceState state);
        protected abstract string GetTitleText();

        private void UpdateView()
        {
            if (_playerDataService.TryGetData<TServiceState>(out var state))
            {
                _view.SetValue(GetValueText(state));
            }
        }
        
        private void WalletDataChangedHandler(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void AddResourceButtonClickedHandler(object sender, EventArgs e)
        {
            var resources = new List<IResourceModel>()
            {
                _resourceModel
            };
            
            _walletService.Apply(resources);
        }
    }
}