using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Model;
using AppActs.Client.View.EventArg;
using AppActs.Client.Model;

namespace AppActs.Client.View
{
    public interface IAccountUserManagementView : IViewLoggedIn, IViewIsValid
    {
        event EventHandler<EventArgs<Guid>> Remove;
        event EventHandler Add;

        void Populate(IEnumerable<User> accountUsers);
        string GetName();
        string GetEmail();
        void ShowErrorValidation();
        void ShowErrorEmailTaken();
        void ShowSuccessfullyAdded();
        void ShowSuccessfullyRemoved(string email);
        void ShowErrorEmailCantBeSent();
        void Clear();
    }
}
