using Modules.Core.Interfaces;

namespace Modules.Location.Resource
{
    public interface ILocationResource : IResourceModel
    {
        string LocationName { get; }
    }
}