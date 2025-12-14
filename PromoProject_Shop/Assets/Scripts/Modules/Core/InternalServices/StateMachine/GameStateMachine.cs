using System;
using System.Collections.Generic;
using Modules.Core.InternalServices.StateMachine.States;
using Modules.Core.Services;

namespace Modules.Core.InternalServices.StateMachine
{
    internal class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IGameState> _states;
        private IGameState _activeState;
        
        public GameStateMachine(ICoroutineRunner coroutineRunner, ProjectContext projectContext)
        {
            _states = new Dictionary<Type, IGameState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, coroutineRunner, projectContext),
                [typeof(GameLoopState)] = new GameLoopState(this, projectContext),
            };
        }
        
        public void Enter<TState>() where TState : class, IGameState
        {
            _activeState?.Exit();
            var state = GetState<TState>();
            _activeState = state;
            
            state.Enter();
        }
        
        private TState GetState<TState>() where TState : class, IGameState
        { 
            return _states[typeof(TState)] as TState;
        }
    }
}