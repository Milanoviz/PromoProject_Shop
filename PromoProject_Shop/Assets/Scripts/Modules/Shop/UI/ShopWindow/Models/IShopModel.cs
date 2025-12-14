using System.Collections.Generic;

namespace Modules.Shop.UI.ShopWindow.Models
{
    public interface IShopModel
    {
        public IReadOnlyCollection<IShopLotModel> Lots { get; }
    }
}