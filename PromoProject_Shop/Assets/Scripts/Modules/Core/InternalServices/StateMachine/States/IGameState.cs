namespace Modules.Core.InternalServices.StateMachine.States
{
    internal interface IGameState
    {
        void Enter();
        void Exit();
    }
}