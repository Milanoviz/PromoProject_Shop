using Modules.Shop.UI.ShopWindow.Controllers;
using Modules.Shop.UI.ShopWindow.Models;

namespace Modules.Shop.Services.Preview
{
    public interface IShopPreviewService
    {
        void ShowPreview(IShopWindowController shopWindowController, IShopLotModel lotModel);
        void ClosePreview();
    }
}