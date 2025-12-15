using Modules.Core.Interfaces;
using Modules.Core.Services.DebugServices;
using Modules.Core.Services.DebugServices.Base;
using Modules.Core.Services.PlayerData;
using Modules.Core.Services.Wallet;
using Modules.VIP.Helpers;
using Modules.VIP.State;

namespace Modules.VIP.DebugService
{
    public class VIPDebugWidgetController : DebugWidgetControllerBase<VIPServiceState>
    {
        private const string TittleText = "VIP";
        
        public VIPDebugWidgetController(IDebugWidgetView view, IResourceModel resourceModel, IPlayerDataService playerDataService, IWalletService walletService) : base(view, resourceModel, playerDataService, walletService)
        {
        }

        protected override string GetValueText(VIPServiceState state)
        {
            return state.DurationTimeSpan.ToPrettyTimeWithLetters();
        }

        protected override string GetTitleText()
        {
            return TittleText;
        }
    }
}