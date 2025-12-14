using System;
using System.Collections.Generic;
using Modules.Shop.Enums;
using Modules.Shop.UI.ShopWindow.Models;
using UnityEngine;

namespace Modules.Shop.UI.ShopWindow.Views
{
    public class ShopWindowView : MonoBehaviour, IShopWindowView
    {
        public event EventHandler<string> LotInfoButtonClicked;
        public event EventHandler<string> LotPurchaseButtonClicked;
        
        [SerializeField] private Transform _root;

        private readonly Dictionary<string, ShopLotView> _lotViews = new();

        public void Show(IShopModel model, ShopLotView lotViewPrefab)
        {
            foreach (var lotModel in model.Lots)
            {
                var view = CreateLotView(lotModel, lotViewPrefab);
                _lotViews[lotModel.Id] = view;
            }
        }

        public void SetLotState(string id, ShopLotViewState state)
        {
            if (_lotViews.TryGetValue(id, out var view))
            {
                view.SetState(state);
            }
        }

        public void Hide()
        {
            Dispose();
        }

        private ShopLotView CreateLotView(IShopLotModel model, ShopLotView lotViewPrefab)
        {
            var instance = Instantiate(lotViewPrefab, _root);
            instance.Init(model.Id, model.Tittle);
            instance.SetMode(ShopLotViewMode.Shop);
            instance.transform.position = Vector3.zero;
            instance.InfoButtonClicked += LotInfoButtonClickedHandler;
            instance.PurchaseButtonClicked += LotPurchaseButtonClickedHandler;
            return instance;
        }

        private void Dispose()
        {
            foreach (var lotView in _lotViews.Values)
            {
                lotView.Dispose();
                lotView.InfoButtonClicked -= LotInfoButtonClickedHandler;
                lotView.PurchaseButtonClicked -= LotInfoButtonClickedHandler;
                Destroy(lotView.gameObject);
            }
            
            _lotViews.Clear();
        }
        
        private void OnDestroy()
        {
            Dispose();
        }
        
        private void LotInfoButtonClickedHandler(object sender, string lotId)
        {
            LotInfoButtonClicked?.Invoke(this, lotId);
        }
        
        private void LotPurchaseButtonClickedHandler(object sender, string lotId)
        {
            LotPurchaseButtonClicked?.Invoke(this, lotId);
        }
    }
}