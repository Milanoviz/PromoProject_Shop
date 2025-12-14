using System;
using System.Collections;
using System.Collections.Generic;
using Modules.Core.Services;
using Modules.Core.Services.Wallet;
using Modules.Shop.UI.ShopWindow.Models;
using UnityEngine;

namespace Modules.Shop.Services.Shop
{
    public class ShopService : IShopService
    {
        public event EventHandler DataChanged;
        public event EventHandler<string> PurchaseProcessChanged;

        private readonly IWalletService _walletService;
        private readonly ICoroutineRunner _coroutineRunner;

        private readonly HashSet<string> _purchaseProcesses = new();

        public ShopService(IWalletService walletService, ICoroutineRunner coroutineRunner)
        {
            _walletService = walletService;
            _coroutineRunner = coroutineRunner;
            _walletService.DataChanged += WalletDataChangedHandler;
        }

        public bool CanPurchase(IShopLotModel lotModel)
        {
            return _walletService.CanApply(lotModel.Price);
        }

        public bool PurchaseInProgress(IShopLotModel lotModel)
        {
            return _purchaseProcesses.Contains(lotModel.Id);
        }

        public void Purchase(IShopLotModel lotModel)
        {
            if (!CanPurchase(lotModel))
            {
                return;
            }

            _coroutineRunner.StartCoroutine(ProcessPurchaseCoroutine(lotModel));
        }

        private IEnumerator ProcessPurchaseCoroutine(IShopLotModel lot)
        {
            RegisterPurchaseProcess(lot.Id);
            yield return new WaitForSeconds(3f);

            var isPaySuccess = _walletService.Apply(lot.Price);
            if (isPaySuccess)
            {
                _walletService.Apply(lot.Reward);
            }
            else
            {
                Debug.LogError($"Purchase error. Could not purchase productId: {lot.Id}");
            }
            
            UnregisterPurchaseProcess(lot.Id);
        }

        private void RegisterPurchaseProcess(string productId)
        {
            if (!_purchaseProcesses.Contains(productId))
            {
                _purchaseProcesses.Add(productId);
                OnPurchaseProcessChanged(productId);
            }
        }

        private void UnregisterPurchaseProcess(string productId)
        {
            if (_purchaseProcesses.Contains(productId))
            {
                _purchaseProcesses.Remove(productId);
                OnPurchaseProcessChanged(productId);
            }
        }
        
        private void WalletDataChangedHandler(object sender, EventArgs e)
        {
            DataChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnPurchaseProcessChanged(string productId)
        {
            PurchaseProcessChanged?.Invoke(this, productId);
        }
    }
}