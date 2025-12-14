using System;
using System.Collections.Generic;
using Modules.Core.Interfaces;

namespace Modules.Core.Services.Wallet
{
    public interface IWalletService : IService
    {
        event EventHandler DataChanged;
        
        bool CanApply(IEnumerable<IResourceModel> resources);
        bool Apply(IEnumerable<IResourceModel> resources);

        void Register(IModuleWallet moduleWallet);
        void Unregister(IModuleWallet moduleWallet);
    }
}