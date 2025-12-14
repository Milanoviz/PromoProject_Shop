using System;
using Modules.Shop.Enums;
using Modules.Shop.UI.ShopWindow.Models;
using Modules.Shop.UI.ShopWindow.Views;

namespace Modules.Shop.UI.PreviewWindow.Views
{
    public interface IShopPreviewWindowView
    {
        event EventHandler PurchaseButtonClicked;
        event EventHandler ExitButtonClicked;
        
        void Show(IShopLotModel shopLotModel, ShopLotView lotPrefab);
        void SetLotState(ShopLotViewState state);
        void Hide();
    }
}