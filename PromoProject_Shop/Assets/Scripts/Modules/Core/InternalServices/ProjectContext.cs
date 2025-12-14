using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Modules.Core.Interfaces;
using Modules.Core.InternalServices.LoadingScreen;
using Modules.Core.Services;
using Modules.Core.Services.DebugServices;
using Modules.Core.Services.GameObjectFactory;
using Modules.Core.Services.Notifier;
using Modules.Core.Services.PlayerData;
using Modules.Core.Services.ResourceManagement;
using Modules.Core.Services.SceneLoader;
using Modules.Core.Services.UI;
using Modules.Core.Services.Wallet;

namespace Modules.Core.InternalServices
{
    internal class ProjectContext
    {
        private ServiceContainer _serviceContainer;
        private readonly List<IModuleContext> _moduleContexts = new();

        public ServiceContainer ServiceContainer => _serviceContainer;

        public void Install(ICoroutineRunner coroutineRunner)
        {
            InstallServices(coroutineRunner);
            InstallModules(_serviceContainer);
        }

        private void InstallServices(ICoroutineRunner coroutineRunner)
        {
            _serviceContainer = new ServiceContainer();

            _serviceContainer.Bind(coroutineRunner);
            
            var gameNotifier = new GameEventNotifier();
            _serviceContainer.Bind<IGameEventNotifier>(gameNotifier);
            
            _serviceContainer.Bind<ISceneLoader>(new SceneLoader(coroutineRunner, gameNotifier));
            
            var assetProvider = new AssetProvider();
            _serviceContainer.Bind<IAssetProvider>(assetProvider);

            var gameObjectFactory = new GameObjectFactory();
            _serviceContainer.Bind<IGameObjectFactory>(gameObjectFactory);

            var playerDataService = new PlayerDataService();
            _serviceContainer.Bind<IPlayerDataService>(playerDataService);
            
            _serviceContainer.Bind<ILoadingScreenService>(new LoadingScreenService(assetProvider, gameObjectFactory));
            
            _serviceContainer.Bind<IWalletService>(new WalletService());

            var uiContainerService = new UIContainerService(assetProvider, gameObjectFactory, gameNotifier);
            _serviceContainer.Bind<IUIContainerService>(uiContainerService);
            
            _serviceContainer.Bind<IDebugService>(new DebugService(uiContainerService));
        }
        
        private void InstallModules(ServiceContainer serviceContainer)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(ReferencesCore)
                .ToArray();
            
            var contextTypes = assemblies
                .SelectMany(GetTypesSafe)
                .Where(t => typeof(IModuleContext).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
                .ToArray();

            try
            {
                foreach (var t in contextTypes)
                {
                    var inst = (IModuleContext)Activator.CreateInstance(t)!;
                    inst.Install(serviceContainer);
                    _moduleContexts.Add(inst);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Install process failed", e);
            }
        }
        
        private bool ReferencesCore(Assembly a)
        {
            try
            {
                return a.GetReferencedAssemblies().Any(r => r.Name.StartsWith("Core")) || a.GetName().Name == "Assembly-CSharp";
            }
            catch
            {
                return false;
            }
        }

        private IEnumerable<Type> GetTypesSafe(Assembly asm)
        {
            try
            {
                return asm.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types.Where(t => t != null)!;
            }
            catch
            {
                return Enumerable.Empty<Type>();
            }
        }
    }
}