using System;
using System.Collections.Generic;
using Modules.Core.Services.ResourceManagement;
using Modules.Shop.Enums;
using Modules.Shop.Helper;
using Modules.Shop.Services.Preview;
using Modules.Shop.Services.Shop;
using Modules.Shop.UI.ShopWindow.Models;
using Modules.Shop.UI.ShopWindow.Views;
using UnityEngine;

namespace Modules.Shop.UI.ShopWindow.Controllers
{
    public class ShopWindowController : IShopWindowController
    {
        private readonly IShopService _shopService;
        private readonly IAssetProvider _assetProvider;
        private readonly IShopPreviewService _shopPreviewService;
        
        private readonly IShopModel _shopModel;
        private readonly IShopWindowView _view;

        private Dictionary<string, IShopLotModel> _lotModels;

        public ShopWindowController(IShopModel shopModel,
            IShopWindowView view,
            IShopService shopService,
            IAssetProvider assetProvider,
            IShopPreviewService shopPreviewService)
        {
            _shopModel = shopModel;
            _view = view;
            _shopService = shopService;
            _assetProvider = assetProvider;
            _shopPreviewService = shopPreviewService;

            InitLotModelsDictionary(shopModel);
        }

        public void Show()
        {
            _shopService.DataChanged += ShopServiceDataChangedHandler;
            _shopService.PurchaseProcessChanged += PurchaseProcessChangedHandler;
            _view.LotPurchaseButtonClicked += LotPurchaseButtonClickedHandler;
            _view.LotInfoButtonClicked += LotInfoButtonClickedHandler;
            
            var lotViewPrefab = GetShopLotPrefab();
            _view.Show(_shopModel, lotViewPrefab);
            UpdateLotViews();
        }

        public void Hide()
        {
            _shopService.DataChanged -= ShopServiceDataChangedHandler;
            _shopService.PurchaseProcessChanged -= PurchaseProcessChangedHandler;
            _view.LotPurchaseButtonClicked -= LotPurchaseButtonClickedHandler;
            _view.LotInfoButtonClicked -= LotPurchaseButtonClickedHandler;
            
            _view.Hide();
        }

        private void InitLotModelsDictionary(IShopModel shopModel)
        {
            _lotModels = new Dictionary<string, IShopLotModel>();
            
            foreach (var lotModel in shopModel.Lots)
            {
                if (_lotModels.ContainsKey(lotModel.Id))
                {
                    Debug.LogError($"Several lots have the same Id: {lotModel.Id}");
                    continue;
                }

                _lotModels[lotModel.Id] = lotModel;
            }
        }

        private ShopLotView GetShopLotPrefab()
        {
            var lotViewPrefab = _assetProvider.LoadAsset<ShopLotView>(ShopStaticData.ShopLotAssetPath);
            return lotViewPrefab;
        }

        private void UpdateLotViews()
        {
            foreach (var model in _lotModels.Values)
            {
                UpdateLotView(model);
            }
        }

        private void UpdateLotView(IShopLotModel model)
        {
            var state = GetLotState(model);
            _view.SetLotState(model.Id, state);
        }

        private ShopLotViewState GetLotState(IShopLotModel lotModel)
        {
            if (_shopService.PurchaseInProgress(lotModel))
            {
                return ShopLotViewState.PurchaseInProgress;
            }

            return _shopService.CanPurchase(lotModel) ? ShopLotViewState.Available : ShopLotViewState.Unavailable;
        }
        
        private void ShopServiceDataChangedHandler(object sender, EventArgs e)
        {
            UpdateLotViews();
        }
        
        private void PurchaseProcessChangedHandler(object sender, string lotId)
        {
            if (_lotModels.TryGetValue(lotId, out var lotModel))
            {
                UpdateLotView(lotModel);
            }
        }
        
        private void LotPurchaseButtonClickedHandler(object sender, string lotId)
        {
            if (_lotModels.TryGetValue(lotId, out var model))
            {
                _shopService.Purchase(model);
            }
        }
        
        private void LotInfoButtonClickedHandler(object sender, string lotId)
        {
            if (_lotModels.TryGetValue(lotId, out var lotModel))
            {
                _shopPreviewService.ShowPreview(this, lotModel);
            }
        }
    }
}