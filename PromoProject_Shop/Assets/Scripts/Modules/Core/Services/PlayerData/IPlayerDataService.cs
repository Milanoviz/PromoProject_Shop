using Modules.Core.Interfaces;

namespace Modules.Core.Services.PlayerData
{
    public interface IPlayerDataService : IService
    {
        bool TryGetData<TState>(out TState state) where TState : class, IGameServiceState;
        void SetData<TState>(TState state) where TState : class, IGameServiceState;
    }
}