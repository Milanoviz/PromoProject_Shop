using System;
using Modules.Core.Services.ResourceManagement;
using Modules.Shop.Enums;
using Modules.Shop.Helper;
using Modules.Shop.Services.Preview;
using Modules.Shop.Services.Shop;
using Modules.Shop.UI.PreviewWindow.Views;
using Modules.Shop.UI.ShopWindow.Models;
using Modules.Shop.UI.ShopWindow.Views;

namespace Modules.Shop.UI.PreviewWindow.Controllers
{
    public class ShopPreviewWindowController : IShopPreviewWindowController
    {
        private readonly IShopPreviewService _shopPreviewService;
        private readonly IShopService _shopService;
        private readonly IAssetProvider _assetProvider;
        
        private readonly IShopLotModel _lotModel;
        private readonly IShopPreviewWindowView _view;

        public ShopPreviewWindowController(IShopLotModel lotModel,
            IShopPreviewWindowView view,
            IShopService shopService,
            IShopPreviewService shopPreviewService,
            IAssetProvider assetProvider)
        {
            _lotModel = lotModel;
            _view = view;
            _shopService = shopService;
            _shopPreviewService = shopPreviewService;
            _assetProvider = assetProvider;
        }

        public void Show()
        {
            _shopService.DataChanged += ResourcesDataChangedHandler;
            _shopService.PurchaseProcessChanged += PurchaseProcessChangedHandler;
            _view.PurchaseButtonClicked += PurchaseButtonClickedHandler;
            _view.ExitButtonClicked += ExitButtonClickedHandler;
            
            var lotViewPrefab = GetShopLotPrefab();
            _view.Show(_lotModel, lotViewPrefab);
            UpdateView();
        }

        public void Hide()
        {   
            _shopService.DataChanged -= ResourcesDataChangedHandler;
            _shopService.PurchaseProcessChanged -= PurchaseProcessChangedHandler;
            _view.PurchaseButtonClicked -= PurchaseButtonClickedHandler;
            _view.ExitButtonClicked -= ExitButtonClickedHandler;
            
            _view.Hide();
        }
        
        private void UpdateView()
        {
            var state = GetLotState(_lotModel);
            _view.SetLotState(state);
        }
        
        private ShopLotView GetShopLotPrefab()
        {
            var lotViewPrefab = _assetProvider.LoadAsset<ShopLotView>(ShopStaticData.ShopLotAssetPath);
            return lotViewPrefab;
        }
        
        private ShopLotViewState GetLotState(IShopLotModel lotModel)
        {
            if (_shopService.PurchaseInProgress(lotModel))
            {
                return ShopLotViewState.PurchaseInProgress;
            }

            return _shopService.CanPurchase(lotModel) ? ShopLotViewState.Available : ShopLotViewState.Unavailable;
        }
        
        private void ResourcesDataChangedHandler(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void PurchaseProcessChangedHandler(object sender, string e)
        {
            UpdateView();
        }
        
        private void PurchaseButtonClickedHandler(object sender, EventArgs e)
        {
            _shopService.Purchase(_lotModel);
        }
        
        private void ExitButtonClickedHandler(object sender, EventArgs e)
        {
            _shopPreviewService.ClosePreview();
        }
    }
}