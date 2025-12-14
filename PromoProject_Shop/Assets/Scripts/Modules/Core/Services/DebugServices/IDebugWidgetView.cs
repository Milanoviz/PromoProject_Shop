using System;

namespace Modules.Core.Services.DebugServices
{
    public interface IDebugWidgetView
    {
        event EventHandler AddResourceButtonClicked;
        void SetTitleText(string value);
        void SetValue(string value);
    }
}