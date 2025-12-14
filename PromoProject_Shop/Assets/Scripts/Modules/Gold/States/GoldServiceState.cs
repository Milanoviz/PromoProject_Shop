using Modules.Core.Interfaces;

namespace Modules.Gold.States
{
    public class GoldServiceState : IGameServiceState
    {
        public int Amount { get; set; }

        public GoldServiceState(int amount)
        {
            Amount = amount;
        }
    }
}