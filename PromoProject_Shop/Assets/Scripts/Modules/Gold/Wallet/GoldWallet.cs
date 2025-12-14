using System.Collections.Generic;
using Modules.Core.Interfaces;
using Modules.Core.Services.PlayerData;
using Modules.Core.Services.Wallet;
using Modules.Gold.Resource;
using Modules.Gold.States;

namespace Modules.Gold.Wallet
{
    public class GoldWallet : IModuleWallet
    {
        private readonly IPlayerDataService _playerDataService;

        public GoldWallet(IPlayerDataService playerDataService)
        {
            _playerDataService = playerDataService;
        }
        
        public bool CanApply(IEnumerable<IResourceModel> resources)
        {
            if (!_playerDataService.TryGetData(out GoldServiceState state))
                return false;
            
            var delta = 0;
            
            foreach (var resource in resources)
            {
                if (resource is IGoldResource gold)
                    delta += gold.Amount;
            }
            
            var result = state.Amount + delta;
            return result >= 0;
        }

        public void Apply(IEnumerable<IResourceModel> resources)
        {
            if (!CanApply(resources))
                return;

            if (!_playerDataService.TryGetData(out GoldServiceState state))
                return;

            foreach (var resource in resources)
            {
                if (resource is IGoldResource gold)
                    state.Amount += gold.Amount;
            }

            _playerDataService.SetData(state);
        }
    }
}