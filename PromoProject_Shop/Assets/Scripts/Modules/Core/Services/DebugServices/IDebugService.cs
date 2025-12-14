namespace Modules.Core.Services.DebugServices
{
    public interface IDebugService : IService
    {
        void Register(IModuleDebugService moduleDebugService);
        void Unregister(IModuleDebugService moduleDebugService);

        void Activate();
        void Deactivate();
    }
}