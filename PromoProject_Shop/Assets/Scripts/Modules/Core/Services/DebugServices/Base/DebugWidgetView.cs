using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Core.Services.DebugServices.Base
{
    public class DebugWidgetView : MonoBehaviour, IDebugWidgetView
    {
        public event EventHandler AddResourceButtonClicked;

        [SerializeField] private TMP_Text _tittleText;
        [SerializeField] private TMP_Text _valueText;
        [SerializeField] private Button _addResourceButton;

        private void OnEnable()
        {
            _addResourceButton.onClick.AddListener(AddResourceButtonClickedHandler);
        }

        private void OnDisable()
        {
            _addResourceButton.onClick.RemoveListener(AddResourceButtonClickedHandler);
        }

        public void SetTitleText(string value)
        {
            _tittleText.SetText(value);
        }

        public void SetValue(string value)
        {
            _valueText.SetText(value);
        }
        
        private void AddResourceButtonClickedHandler()
        {
            AddResourceButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}