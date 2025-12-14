using System.Collections;
using UnityEngine;

namespace Modules.Core.InternalServices.LoadingScreen
{
    internal class LoadingScreenView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        private readonly float _hideAnimSpeed = 0.03f;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 1;
        }

        public void Hide()
        {
            StartCoroutine(HideAnimCoroutine());
        }
        
        private IEnumerator HideAnimCoroutine()
        {
            while (_canvasGroup.alpha > 0)
            {
                _canvasGroup.alpha -= _hideAnimSpeed;
                yield return new WaitForSeconds(_hideAnimSpeed);
            }
      
            gameObject.SetActive(false);
        }
    }
}