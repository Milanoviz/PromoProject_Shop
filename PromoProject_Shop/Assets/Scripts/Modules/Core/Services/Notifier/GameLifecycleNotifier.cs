using System;

namespace Modules.Core.Services.Notifier
{
    public class GameEventNotifier : IGameEventNotifier
    {
        public event EventHandler<string> GameSceneChanged;
        public event EventHandler GameInitialized;
        
        public void NotifyGameInitialized()
        {
            GameInitialized?.Invoke(this, EventArgs.Empty);
        }

        public void NotifyGameSceneChanged(string activeSceneName)
        {
            GameSceneChanged?.Invoke(this, activeSceneName);
        }
    }
}