using System;
using Modules.Core.Interfaces;

namespace Modules.VIP.State
{
    public class VIPServiceState : IGameServiceState
    {
        public TimeSpan DurationTimeSpan { get; set; }

        public VIPServiceState(int durationSeconds)
        {
            DurationTimeSpan = TimeSpan.FromSeconds(durationSeconds);
        }
    }
}