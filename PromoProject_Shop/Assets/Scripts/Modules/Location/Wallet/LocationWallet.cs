using System.Collections.Generic;
using Modules.Core.Interfaces;
using Modules.Core.Services.PlayerData;
using Modules.Core.Services.Wallet;
using Modules.Location.Resource;
using Modules.Location.State;
using UnityEngine;

namespace Modules.Location.Wallet
{
    public class LocationWallet : IModuleWallet
    {
        private readonly IPlayerDataService _playerDataService;

        public LocationWallet(IPlayerDataService playerDataService)
        {
            _playerDataService = playerDataService;
        }
        
        public bool CanApply(IEnumerable<IResourceModel> resources)
        {
            if (!_playerDataService.TryGetData(out LocationServiceState _))
                return false;
            
            ILocationResource locationResource = null;

            foreach (var resource in resources)
            {
                if (resource is not ILocationResource loc)
                    continue;

                if (locationResource != null)
                {
                    Debug.LogError("Error. More than one LocationResource");
                    return false;
                }

                locationResource = loc;
            }

            if (locationResource == null)
                return true;

            if (string.IsNullOrEmpty(locationResource.LocationName))
            {
                Debug.LogError("Error. LocationName is empty");
                return false;
            }
            
            return true;
        }

        public void Apply(IEnumerable<IResourceModel> resources)
        {
            if (!CanApply(resources))
                return;

            if (!_playerDataService.TryGetData(out LocationServiceState state))
                return;
            
            ILocationResource locationResource = null;

            foreach (var resource in resources)
            {
                if (resource is ILocationResource loc)
                {
                    locationResource = loc;
                    break;
                }
            }

            if (locationResource == null)
                return;

            state.LocationName = locationResource.LocationName;
            _playerDataService.SetData(state);
        }
    }
}