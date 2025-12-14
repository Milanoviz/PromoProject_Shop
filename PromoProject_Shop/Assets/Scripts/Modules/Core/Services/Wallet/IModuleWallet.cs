using System.Collections.Generic;
using Modules.Core.Interfaces;

namespace Modules.Core.Services.Wallet
{
    public interface IModuleWallet
    {
        bool CanApply(IEnumerable<IResourceModel> resources);
        void Apply(IEnumerable<IResourceModel> resources);
    }
}