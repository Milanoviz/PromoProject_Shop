using System;
using UnityEngine;

namespace Modules.Core.Services.UI
{
    public interface IUIContainerService : IService
    {
        event EventHandler UIContainerCreated;
        
        Transform WindowContainer { get; }
        Transform DebugWidgetContainer { get; }

        void CreateUIContainer();
    }
}