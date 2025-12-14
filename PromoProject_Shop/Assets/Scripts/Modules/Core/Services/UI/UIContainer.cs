using UnityEngine;

namespace Modules.Core.Services.UI
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private Transform _windowRoot;
        [SerializeField] private Transform _debugPanelRoot;

        public Transform WindowRoot => _windowRoot;
        public Transform DebugPanelRoot => _debugPanelRoot;
    }
}