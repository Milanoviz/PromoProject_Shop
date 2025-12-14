using Modules.Core.Interfaces;
using Modules.Core.Services.DebugServices;
using Modules.Core.Services.DebugServices.Base;
using Modules.Core.Services.PlayerData;
using Modules.Core.Services.Wallet;
using Modules.Health.State;

namespace Modules.Health.DebugService
{
    public class HealthDebugWidgetController : DebugWidgetControllerBase<HealthServiceState>
    {
        private const string TittleText = "Health";
        
        public HealthDebugWidgetController(IDebugWidgetView view, IResourceModel resourceModel, IPlayerDataService playerDataService, IWalletService walletService) : base(view, resourceModel, playerDataService, walletService)
        {
        }

        protected override string GetValueText(HealthServiceState state)
        {
            return state.Amount.ToString();
        }

        protected override string GetTitleText()
        {
            return TittleText;
        }
    }
}