using System;
using Modules.Shop.Enums;
using Modules.Shop.UI.PurchaseButton;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Shop.UI.ShopWindow.Views
{
    public class ShopLotView : MonoBehaviour
    {
        public event EventHandler<string> InfoButtonClicked; 
        public event EventHandler<string> PurchaseButtonClicked; 

        [SerializeField] private TMP_Text _tittle;
        [SerializeField] private Button _infoButton;
        [SerializeField] private PurchaseButtonView _purchaseButtonView;

        private string _id;
        
        public void Init(string id, string tittleText)
        {
            _id = id;
            _tittle.text = tittleText;
            Subscribes();
            _purchaseButtonView.Init();
        }

        public void SetState(ShopLotViewState state)
        {
            SetPurchaseButtonState(state);
        }
        
        public void SetMode(ShopLotViewMode mode)
        {
            _infoButton.gameObject.SetActive(mode == ShopLotViewMode.Shop);
        }

        private void SetPurchaseButtonState(ShopLotViewState state)
        {
            var isActive = state is ShopLotViewState.Available;
            var purchaseInProgress = state is ShopLotViewState.PurchaseInProgress;
            _purchaseButtonView.SetState(isActive, purchaseInProgress);
        }
        
        public void Dispose()
        {
            Unsubscribes();
            _purchaseButtonView.Dispose();
        }

        private void Subscribes()
        {
            _infoButton.onClick.AddListener(OnInfoButtonClicked);
            _purchaseButtonView.ButtonClicked += OnPurchaseButtonClicked;
        }

        private void Unsubscribes()
        {
            _infoButton.onClick.RemoveListener(OnInfoButtonClicked);
            _purchaseButtonView.ButtonClicked -= OnPurchaseButtonClicked;
        }

        private void OnInfoButtonClicked()
        {
            InfoButtonClicked?.Invoke(this, _id);
        }
        
        private void OnPurchaseButtonClicked(object sender, EventArgs e)
        {
            PurchaseButtonClicked?.Invoke(this, _id);
        }
    }
}