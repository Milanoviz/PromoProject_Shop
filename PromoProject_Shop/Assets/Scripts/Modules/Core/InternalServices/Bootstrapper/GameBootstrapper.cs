using Modules.Core.InternalServices.StateMachine;
using Modules.Core.InternalServices.StateMachine.States;
using Modules.Core.Services;
using UnityEngine;

namespace Modules.Core.InternalServices.Bootstrapper
{
    internal class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private ProjectContext _projectContext;
        private IGameStateMachine _gameStateMachine;

        private void Awake()
        {
            _projectContext = new ProjectContext();
            _gameStateMachine = new GameStateMachine(this, _projectContext);
            _gameStateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}