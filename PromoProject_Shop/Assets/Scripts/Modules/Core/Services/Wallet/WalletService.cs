using System;
using System.Collections.Generic;
using Modules.Core.Interfaces;

namespace Modules.Core.Services.Wallet
{
    public class WalletService : IWalletService
    { 
        public event EventHandler DataChanged;

        private readonly List<IModuleWallet> _moduleWallets = new();
        
        public bool CanApply(IEnumerable<IResourceModel> resources)
        {
            foreach (var moduleWallet in _moduleWallets)
            {
                if (!moduleWallet.CanApply(resources))
                    return false;
            }

            return true;
        }

        public bool Apply(IEnumerable<IResourceModel> resources)
        {
            if (!CanApply(resources))
                return false;
            
            foreach (var wallet in _moduleWallets)
            {
                wallet.Apply(resources);
            }
            
            OnDataChanged();
            return true;
        }

        public void Register(IModuleWallet moduleWallet)
        {
            _moduleWallets.Add(moduleWallet);
        }

        public void Unregister(IModuleWallet moduleWallet)
        {
            _moduleWallets.Remove(moduleWallet);
        }

        private void OnDataChanged()
        {
            DataChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}