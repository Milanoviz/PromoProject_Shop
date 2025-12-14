using Modules.Core.InternalServices.StateMachine.States;

namespace Modules.Core.InternalServices.StateMachine
{
    internal interface IGameStateMachine
    {
        public void Enter<TState>() where TState : class, IGameState;
    }
}