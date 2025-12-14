using System;

namespace Modules.Core.Services.Notifier
{
    public interface IGameEventNotifier : IService
    {
        event EventHandler<string> GameSceneChanged;
        event EventHandler GameInitialized;

        void NotifyGameSceneChanged(string activeSceneName);
        void NotifyGameInitialized();
    }
}