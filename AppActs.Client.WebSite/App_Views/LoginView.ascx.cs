using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using AppActs.Client.View;
using AppActs.Client.Presenter;
using AppActs.Client.WebSite.App_Base;

namespace AppActs.Client.WebSite.App_Views
{
    public partial class LoginView : MvpUserControl<IAccountUserLoginView, AccountUserLoginPresenter>,
        IAccountUserLoginView
    {
        public event EventHandler Submit;

        public bool IsDemo
        {
            get
            {
                if (Request.QueryString["demo"] != null)
                {
                    return bool.Parse(Request.QueryString["demo"]);
                }

                return false;
            }
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            if (this.Submit != null)
            {
                this.Submit(this, e);
            }
        }

        public string GetEmail()
        {
            return this.txtEmail.Text;
        }


        public string GetPassword()
        {
            return this.txtPassword.Text;
        }


        public string GetUrlAttempted()
        {
            return this.Request.QueryString["ref"];
        }

        public bool IsValid()
        {
            return this.reqEmail.IsValid && this.reqPassword.IsValid;
        }

        public void ShowErrorInvalidCredentials()
        {
            this.lblErrorInvalidCredentials.Visible = true;
        }

        public void ShowErrorInvalidLogin()
        {
            this.lblErrorInvalidLogin.Visible = true;
        }

        public void RedirectToFeed()
        {
            this.Response.Redirect("~/");
        }

        public void RedirectToAttempted()
        {
            this.Response.Redirect(this.GetUrlAttempted());
        }

        public void RedirectToApps()
        {
            this.Response.Redirect("~/Apps/");
        }
    }
}