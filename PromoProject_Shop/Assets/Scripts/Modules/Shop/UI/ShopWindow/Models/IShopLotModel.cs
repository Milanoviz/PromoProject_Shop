using System.Collections.Generic;
using Modules.Core.Interfaces;

namespace Modules.Shop.UI.ShopWindow.Models
{
    public interface IShopLotModel
    {
        public string Id { get; }
        public string Tittle { get; }
        public IReadOnlyCollection<IResourceModel> Price { get; }
        public IReadOnlyCollection<IResourceModel> Reward { get; }
    }
}