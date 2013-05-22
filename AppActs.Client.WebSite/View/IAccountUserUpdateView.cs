using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Client.Model;

namespace AppActs.Client.View
{
    public interface IAccountUserUpdateView : IViewLoggedIn, IViewIsValid
    {
        event EventHandler SubmitDetails;
        event EventHandler SubmitPassword;

        void Populate(User accountUser);
        string GetName();
        string GetEmail();
        string GetPassword();
        string GetPasswordConfirm();
        void ShowUpdatePasswordComplete();
        void ShowUpdateEmailComplete();
        void ShowErrorValidation();
        void ShowErrorEmailTaken();
        void ShowErrorPasswordValidation();
        string GetOldPassword();
        void ShowErrorOldPasswordNoMatch();
        void ShowUpdateNameComplete();
    }
}
