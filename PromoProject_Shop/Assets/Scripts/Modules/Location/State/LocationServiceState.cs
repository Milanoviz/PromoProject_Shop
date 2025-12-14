using Modules.Core.Interfaces;

namespace Modules.Location.State
{
    public class LocationServiceState : IGameServiceState
    {
        public string LocationName { get; set; }

        public LocationServiceState(string locationName)
        {
            LocationName = locationName;
        }
    }
}