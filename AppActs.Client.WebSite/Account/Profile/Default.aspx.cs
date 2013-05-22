using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppActs.Client.WebSite.App_Base;
using AppActs.Client.View;
using AppActs.Client.Presenter;
using AppActs.Client.Model;

namespace AppActs.Client.WebSite.Account.Profile
{
    public partial class Default : MvpPage<IAccountUserUpdateView, AccountUserUpdatePresenter>,
        IAccountUserUpdateView
    {
        public event EventHandler SubmitDetails;
        public event EventHandler SubmitPassword;

        protected void btnSubmitDetails_OnClick(object sender, EventArgs e)
        {
            if (this.SubmitDetails != null)
            {
                this.SubmitDetails(sender, e);
            }
        }

        protected void btnSubmitPassword_OnClick(object sender, EventArgs e)
        {
            if (this.SubmitPassword != null)
            {
                this.SubmitPassword(sender, e);
            }
        }

        public void Populate(User accountUser)
        {
            this.txtName.Text = accountUser.Name;
            this.txtEmail.Text = accountUser.Email;
        }

        public string GetName()
        {
            return this.txtName.Text;
        }

        public string GetEmail()
        {
            return this.txtEmail.Text;
        }

        public string GetPassword()
        {
            return this.txtPassword.Text;
        }

        public string GetPasswordConfirm()
        {
            return this.txtPasswordConfirm.Text;
        }

        public void ShowUpdatePasswordComplete()
        {
            this.lblSuccessPasswordChanged.Visible = true;
        }

        public void ShowUpdateEmailComplete()
        {
            this.lblSuccess.Visible = true;
        }


        public void ShowErrorValidation()
        {
            this.lblErrorValidation.Visible = true;
        }

        public void ShowErrorEmailTaken()
        {
            this.lblErrorEmailTaken.Visible = true;
        }

        public void ShowErrorPasswordValidation()
        {
            this.lblErrorPasswordValidation.Visible = true;
        }

        public new bool IsValid()
        {
            return this.reqEmail.IsValid && this.reqName.IsValid;
        }

        public void ShowErrorSystemGeneral()
        {
            this.lblError.Visible = true;
        }

        public string GetOldPassword()
        {
            return this.txtPasswordOld.Text;
        }

        public void ShowErrorOldPasswordNoMatch()
        {
            this.lblErrorPasswordOldDontMatch.Visible = true;
        }

        public void ShowUpdateNameComplete()
        {
            this.lblSuccess.Visible = true;
        }
    }
}