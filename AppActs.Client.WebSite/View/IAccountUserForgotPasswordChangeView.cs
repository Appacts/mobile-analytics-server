using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.Client.View
{
    public interface IAccountUserForgotPasswordChangeView : IView, IViewIsValid
    {
        event EventHandler Submit;
        
        Guid GetForgotPasswordGuid();

        string GetPassword();
        string GetPasswordConfirm();
        void ShowErrorInvalidGuid();
        void ShowErrorPasswordConfirmation();
        void ShowSuccessPasswordChanged();
    }
}
