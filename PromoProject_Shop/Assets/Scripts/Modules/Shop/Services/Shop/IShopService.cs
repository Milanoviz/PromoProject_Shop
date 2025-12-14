using System;
using Modules.Shop.UI.ShopWindow.Models;

namespace Modules.Shop.Services.Shop
{
    public interface IShopService
    {
        event EventHandler DataChanged;
        event EventHandler<string> PurchaseProcessChanged;

        bool CanPurchase(IShopLotModel lotModel);
        bool PurchaseInProgress(IShopLotModel lotModel);
        void Purchase(IShopLotModel lotModel);
    }
}