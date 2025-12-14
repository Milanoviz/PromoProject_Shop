using Modules.Core.Interfaces;
using UnityEngine;

namespace Modules.Gold.Resource
{
    [CreateAssetMenu(menuName = "Configs/Resources/Gold")]
    public class GoldResourceScriptableObject : ResourceScriptableObjectModelBase, IGoldResource
    {
        [SerializeField] private int _amount;
        public int Amount => _amount;
    }
}