using Modules.Core.Interfaces;

namespace Modules.VIP.Resource
{
    public interface IVIPResource : IResourceModel
    {
        int DurationSeconds { get; }
    }
}