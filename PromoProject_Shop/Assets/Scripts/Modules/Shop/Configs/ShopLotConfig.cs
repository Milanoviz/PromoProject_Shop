using System.Collections.Generic;
using Modules.Core.Interfaces;
using Modules.Shop.UI.ShopWindow.Models;
using UnityEngine;

namespace Modules.Shop.Configs
{
    [CreateAssetMenu(menuName = "Configs/Shop/ShopLotConfig")]
    public class ShopLotConfig : ScriptableObject, IShopLotModel
    {
        [SerializeField] private string _id;
        [SerializeField] private string _tittle;
        [SerializeField] private List<ResourceScriptableObjectModelBase> _price;
        [SerializeField] private List<ResourceScriptableObjectModelBase> _reward;

        public string Id => _id;
        public string Tittle => _tittle;
        public IReadOnlyCollection<IResourceModel> Price => _price;
        public IReadOnlyCollection<IResourceModel> Reward => _reward;
    }
}