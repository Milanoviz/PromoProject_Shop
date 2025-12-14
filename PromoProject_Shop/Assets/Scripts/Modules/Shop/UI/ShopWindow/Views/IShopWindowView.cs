using System;
using Modules.Shop.Enums;
using Modules.Shop.UI.ShopWindow.Models;

namespace Modules.Shop.UI.ShopWindow.Views
{
    public interface IShopWindowView
    {
        event EventHandler<string> LotInfoButtonClicked;
        event EventHandler<string> LotPurchaseButtonClicked;
        
        void Show(IShopModel model, ShopLotView lotViewPrefab);
        void SetLotState(string id, ShopLotViewState state);
        void Hide();
    }
}