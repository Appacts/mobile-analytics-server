using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Model;
using AppActs.Client.Model.Enum;

namespace AppActs.Client.View
{
    public interface IAccountUserLoggedInView : IViewLoggedIn
    {
        event EventHandler Logout;

        void RedirectToLogin();
        void Set(ScreenType screenType);
    }
}
