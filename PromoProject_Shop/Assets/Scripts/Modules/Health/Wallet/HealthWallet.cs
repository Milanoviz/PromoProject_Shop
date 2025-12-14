using System.Collections.Generic;
using Modules.Core.Interfaces;
using Modules.Core.Services.PlayerData;
using Modules.Core.Services.Wallet;
using Modules.Health.Resource;
using Modules.Health.State;

namespace Modules.Health.Wallet
{
    public class HealthWallet : IModuleWallet
    {
        private readonly IPlayerDataService _playerDataService;

        public HealthWallet(IPlayerDataService playerDataService)
        {
            _playerDataService = playerDataService;
        }
        
        public bool CanApply(IEnumerable<IResourceModel> resources)
        {
            if (!_playerDataService.TryGetData(out HealthServiceState state))
                return false;
            
            var delta = 0;
            
            foreach (var resource in resources)
            {
                if (resource is IHealthResource health)
                    delta += health.Amount;
            }
            
            var result = state.Amount + delta;
            return result >= 0;
        }

        public void Apply(IEnumerable<IResourceModel> resources)
        {
            if (!CanApply(resources))
                return;

            if (!_playerDataService.TryGetData(out HealthServiceState state))
                return;

            foreach (var resource in resources)
            {
                if (resource is IHealthResource health)
                    state.Amount += health.Amount;
            }

            _playerDataService.SetData(state);
        }
    }
}