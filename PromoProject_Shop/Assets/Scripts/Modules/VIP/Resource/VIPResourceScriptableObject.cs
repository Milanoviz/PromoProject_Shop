using Modules.Core.Interfaces;
using UnityEngine;

namespace Modules.VIP.Resource
{
    [CreateAssetMenu(menuName = "Configs/Resources/VIP")]
    public class VIPResourceScriptableObject : ResourceScriptableObjectModelBase, IVIPResource
    {
        [SerializeField] private int _durationSeconds;
        public int DurationSeconds => _durationSeconds;
    }
}