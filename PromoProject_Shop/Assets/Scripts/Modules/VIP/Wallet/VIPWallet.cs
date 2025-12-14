using System;
using System.Collections.Generic;
using Modules.Core.Interfaces;
using Modules.Core.Services.PlayerData;
using Modules.Core.Services.Wallet;
using Modules.VIP.Resource;
using Modules.VIP.State;

namespace Modules.VIP.Wallet
{
    public class VIPWallet : IModuleWallet
    {
        private readonly IPlayerDataService _playerDataService;

        public VIPWallet(IPlayerDataService playerDataService)
        {
            _playerDataService = playerDataService;
        }
        
        public bool CanApply(IEnumerable<IResourceModel> resources)
        {
            if (!_playerDataService.TryGetData(out VIPServiceState state))
                return false;

            var delta = TimeSpan.Zero;

            foreach (var resource in resources)
            {
                if (resource is IVIPResource vip)
                    delta += TimeSpan.FromSeconds(vip.DurationSeconds);
            }

            return state.DurationTimeSpan + delta >= TimeSpan.Zero;
        }

        public void Apply(IEnumerable<IResourceModel> resources)
        {
            if (!CanApply(resources))
                return;

            if (!_playerDataService.TryGetData(out VIPServiceState state))
                return;
            
            var delta = TimeSpan.Zero;

            foreach (var resource in resources)
            {
                if (resource is IVIPResource vip)
                    delta += TimeSpan.FromSeconds(vip.DurationSeconds);
            }
            
            state.DurationTimeSpan += delta;
            _playerDataService.SetData(state);
        }
    }
}