using Modules.Core.Interfaces;

namespace Modules.Health.State
{
    public class HealthServiceState : IGameServiceState
    {
        public int Amount { get; set; }

        public HealthServiceState(int amount)
        {
            Amount = amount;
        }
    }
}