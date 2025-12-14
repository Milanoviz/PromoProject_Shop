using System;
using System.Collections.Generic;
using Modules.Core.Interfaces;

namespace Modules.Core.Services.PlayerData
{
    public class PlayerDataService : IPlayerDataService
    {
        private readonly Dictionary<Type, IGameServiceState> _states = new();

        public bool TryGetData<TState>(out TState state) where TState : class, IGameServiceState
        {
            if (_states.TryGetValue(typeof(TState), out var boxedState))
            {
                state = boxedState as TState;
                return state != null;
            }
            
            state = null;
            return false;
        }

        public void SetData<TState>(TState state) where TState : class, IGameServiceState
        {
            _states[typeof(TState)] = state;
        }
    }
}