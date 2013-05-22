using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Model;
using AppActs.Client.Model;

namespace AppActs.Client.Service.Interface
{
    public interface IEmailService
    {
        void SendUserForgotPassword(User accountUser, Guid guidForgotPassword);

        void SendUserAdded(string userNameAdded, string userNameNew, string userEmailNew, string userPasswordNew);
    }
}
