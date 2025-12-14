using Modules.Core.Interfaces;
using Modules.Core.Services.DebugServices;
using Modules.Core.Services.DebugServices.Base;
using Modules.Core.Services.PlayerData;
using Modules.Core.Services.Wallet;
using Modules.Gold.States;

namespace Modules.Gold.DebugService
{
    public class GoldDebugWidgetController : DebugWidgetControllerBase<GoldServiceState>
    {
        private const string TittleText = "Gold";
        
        public GoldDebugWidgetController(IDebugWidgetView view, IResourceModel resourceModel, IPlayerDataService playerDataService, IWalletService walletService) : base(view, resourceModel, playerDataService, walletService)
        {
        }

        protected override string GetValueText(GoldServiceState state)
        {
            return state.Amount.ToString();
        }

        protected override string GetTitleText()
        {
            return TittleText;
        }
    }
}