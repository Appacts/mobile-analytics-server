using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.Client.View
{
    public interface IAccountUserLoginView : IView, IViewIsValid
    {
        event EventHandler Submit;

        string GetEmail();

        string GetPassword();

        string GetUrlAttempted();

        bool IsValid();

        void ShowErrorInvalidCredentials();

        void ShowErrorInvalidLogin();

        void RedirectToFeed();

        void RedirectToAttempted();

        void RedirectToApps();

        bool IsDemo { get; }
    }
}
