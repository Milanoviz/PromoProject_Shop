using Modules.Core.InternalServices.LoadingScreen;
using Modules.Core.Services.Notifier;

namespace Modules.Core.InternalServices.StateMachine.States
{
    internal class GameLoopState : IGameState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ProjectContext _projectContext;
        
        public GameLoopState(IGameStateMachine stateMachine, ProjectContext projectContext)
        {
            _stateMachine = stateMachine;
            _projectContext = projectContext;
        }

        public void Enter()
        {
            var gameNotifier = _projectContext.ServiceContainer.Resolve<IGameEventNotifier>();
            gameNotifier.NotifyGameInitialized();
            
            var loadingScreenService = _projectContext.ServiceContainer.Resolve<ILoadingScreenService>();
            loadingScreenService.Hide();
        }

        public void Exit()
        {
            
        }
    }
}