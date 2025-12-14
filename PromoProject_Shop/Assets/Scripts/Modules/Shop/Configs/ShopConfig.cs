using System.Collections.Generic;
using Modules.Shop.UI.ShopWindow.Models;
using UnityEngine;

namespace Modules.Shop.Configs
{
    [CreateAssetMenu(menuName = "Configs/Shop/ShopConfig")]
    public class ShopConfig : ScriptableObject, IShopModel
    {
        [SerializeField] private List<ShopLotConfig> _lotConfigs;
        
        public IReadOnlyCollection<IShopLotModel> Lots => _lotConfigs;
    }
}