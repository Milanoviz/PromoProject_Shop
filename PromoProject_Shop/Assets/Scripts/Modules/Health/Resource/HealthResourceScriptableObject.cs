using Modules.Core.Interfaces;
using UnityEngine;

namespace Modules.Health.Resource
{
    [CreateAssetMenu(menuName = "Configs/Resources/Health")]
    public class HealthResourceScriptableObject : ResourceScriptableObjectModelBase, IHealthResource
    {
        [SerializeField] private int _amount;
        public int Amount => _amount;
        
    }
}