using System;
using System.Collections.Generic;
using Modules.Core.Services.UI;

namespace Modules.Core.Services.DebugServices
{
    public class DebugService : IDebugService
    {
        private readonly List<IModuleDebugService> _moduleServices = new();

        public DebugService(IUIContainerService uiContainerService)
        {
            uiContainerService.UIContainerCreated += UiContainerCratedHandler;
        }

        public void Register(IModuleDebugService moduleDebugService)
        {
            _moduleServices.Add(moduleDebugService);
        }

        public void Unregister(IModuleDebugService moduleDebugService)
        {
            _moduleServices.Remove(moduleDebugService);
        }

        public void Activate()
        {
            foreach (var moduleService in _moduleServices)
            {
                moduleService.Activate();
            }
        }

        public void Deactivate()
        {
            foreach (var moduleService in _moduleServices)
            {
                moduleService.Deactivate();
            }
        }
        
        private void UiContainerCratedHandler(object sender, EventArgs e)
        {
            Activate();
        }
    }
}