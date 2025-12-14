using Modules.Core.Interfaces;

namespace Modules.Health.Resource
{
    public interface IHealthResource : IResourceModel
    {
        int Amount { get; }
    }
}