using Modules.Core.Services;

namespace Modules.Core.Interfaces
{
    public interface IModuleContext
    {
        public void Install(ServiceContainer serviceContainer);
    }
}