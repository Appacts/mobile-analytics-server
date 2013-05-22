using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.Client.View
{
    public interface IAccountUserForgotPasswordView : IView, IViewIsValid
    {
        event EventHandler Submit;

        string GetEmail();
        void ShowErrorNoSuchEmail();
        void ShowErrorInvalidEmail();
        void ShowSuccessEmailSent(string email);
    }
}
