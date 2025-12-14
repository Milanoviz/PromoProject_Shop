using System;
using Modules.Shop.Enums;
using Modules.Shop.UI.ShopWindow.Models;
using Modules.Shop.UI.ShopWindow.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Shop.UI.PreviewWindow.Views
{
    public class ShopPreviewWindowView : MonoBehaviour, IShopPreviewWindowView
    {
        public event EventHandler PurchaseButtonClicked;
        public event EventHandler ExitButtonClicked;

        [SerializeField] private Button exitButton;
        [SerializeField] private Transform _root;

        private ShopLotView _lotView;
        
        public void Show(IShopLotModel shopLotModel, ShopLotView lotPrefab)
        {
            _lotView = CreateLotView(shopLotModel, lotPrefab);
            exitButton.onClick.AddListener(ExitButtonClickedHandler);
        }

        public void SetLotState(ShopLotViewState state)
        {
            _lotView.SetState(state);
        }

        public void Hide()
        {
            DisposeLotView();
            exitButton.onClick.RemoveListener(ExitButtonClickedHandler);
        }

        private ShopLotView CreateLotView(IShopLotModel shopLotModel, ShopLotView lotPrefab)
        {
            var instance = Instantiate(lotPrefab, _root);
            instance.Init(shopLotModel.Id, shopLotModel.Tittle);
            instance.SetMode(ShopLotViewMode.Single);
            instance.PurchaseButtonClicked += LotPurchaseButtonClickedHandler;
            return instance;
        }

        private void DisposeLotView()
        {
            if (_lotView == null)
                return;

            _lotView.Dispose();
            _lotView.PurchaseButtonClicked -= LotPurchaseButtonClickedHandler;
            Destroy(_lotView.gameObject);
            _lotView = null;
        }

        private void OnDestroy()
        {
            DisposeLotView();
        }

        private void LotPurchaseButtonClickedHandler(object sender, string e)
        {
            PurchaseButtonClicked?.Invoke(this, EventArgs.Empty);
        }
        
        private void ExitButtonClickedHandler()
        {
            ExitButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}