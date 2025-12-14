using Modules.Core.Interfaces;
using UnityEngine;

namespace Modules.Location.Resource
{
    [CreateAssetMenu(menuName = "Configs/Resources/Location")]
    public class LocationResourceScriptableObject : ResourceScriptableObjectModelBase, ILocationResource
    {
        [SerializeField] private string _locationName;
        public string LocationName => _locationName;
    }
}