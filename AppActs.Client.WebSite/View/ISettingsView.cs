using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Client.Model.Enum;

namespace AppActs.Client.View
{
    public interface ISettingsView : IViewLoggedIn
    {
        void Set(string fullName);
        void Set(ScreenType screenType);
    }
}
