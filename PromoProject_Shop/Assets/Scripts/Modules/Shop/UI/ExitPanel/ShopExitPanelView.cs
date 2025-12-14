using System;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Shop.UI.ExitPanel
{
    public class ShopExitPanelView : MonoBehaviour
    {
        public event EventHandler ExitButtonClicked;
        
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(ButtonClickedHandler);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ButtonClickedHandler);
        }

        private void ButtonClickedHandler()
        {
            ExitButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}