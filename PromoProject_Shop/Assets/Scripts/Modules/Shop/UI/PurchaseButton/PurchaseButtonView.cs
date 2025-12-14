using System;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Shop.UI.PurchaseButton
{
    public class PurchaseButtonView : MonoBehaviour
    {
        public event EventHandler ButtonClicked;
        
        [SerializeField] private GameObject _activeState;
        [SerializeField] private GameObject _inactiveState;
        [SerializeField] private GameObject _inProgressState;
        [SerializeField] private Button _button;

        public void Init()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        public void SetState(bool isActive, bool inProgress)
        {
            _activeState.SetActive(!inProgress && isActive);
            _inactiveState.SetActive(!inProgress && !isActive);
            _inProgressState.SetActive(inProgress);
        }

        public void Dispose()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }
        
        private void OnButtonClicked()
        {
            ButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}