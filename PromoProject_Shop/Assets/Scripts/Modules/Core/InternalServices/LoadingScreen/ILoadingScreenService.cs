using Modules.Core.Services;

namespace Modules.Core.InternalServices.LoadingScreen
{
    internal interface ILoadingScreenService : IService
    {
        void Show();
        void Hide();
    }
}