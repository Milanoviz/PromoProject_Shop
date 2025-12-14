using Modules.Core.InternalServices.LoadingScreen;
using Modules.Core.Services;
using Modules.Core.Services.SceneLoader;

namespace Modules.Core.InternalServices.StateMachine.States
{
    internal class BootstrapState : IGameState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ProjectContext _projectContext;
        private ISceneLoader _sceneLoader;

        public BootstrapState(IGameStateMachine stateMachine, ICoroutineRunner coroutineRunner, ProjectContext projectContext)
        {
            _stateMachine = stateMachine;
            _coroutineRunner = coroutineRunner;
            _projectContext = projectContext;
        }

        public void Enter()
        {
            _projectContext.Install(_coroutineRunner);

            var loadingScreenService = _projectContext.ServiceContainer.Resolve<ILoadingScreenService>();
            loadingScreenService.Show();
            
            _sceneLoader = _projectContext.ServiceContainer.Resolve<ISceneLoader>();
            _sceneLoader.Load(SceneStaticData.GameSceneName, onLoaded: LoadGameScene);
        }

        public void Exit()
        {
            
        }
        
        private void LoadGameScene()
        {
            _stateMachine.Enter<GameLoopState>();
        }
    }
}