using Modules.Core.Interfaces;
using Modules.Core.Services.DebugServices;
using Modules.Core.Services.DebugServices.Base;
using Modules.Core.Services.PlayerData;
using Modules.Core.Services.Wallet;
using Modules.Location.State;

namespace Modules.Location.DebugService
{
    public class LocationDebugWidgetController : DebugWidgetControllerBase<LocationServiceState>
    {
        private const string TittleText = "Location";
        
        public LocationDebugWidgetController(IDebugWidgetView view, IResourceModel resourceModel, IPlayerDataService playerDataService, IWalletService walletService) : base(view, resourceModel, playerDataService, walletService)
        {
        }

        protected override string GetValueText(LocationServiceState state)
        {
            return state.LocationName;
        }

        protected override string GetTitleText()
        {
            return TittleText;
        }
    }
}