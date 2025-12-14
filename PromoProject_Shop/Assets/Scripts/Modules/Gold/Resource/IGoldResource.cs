using Modules.Core.Interfaces;

namespace Modules.Gold.Resource
{
    public interface IGoldResource : IResourceModel
    {
        int Amount { get; }
    }
}