using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppActs.Client.WebSite.App_Base;
using AppActs.Client.View;
using AppActs.Client.Presenter;

namespace AppActs.Client.WebSite.Password_Change
{
    public partial class Default : MvpPage<IAccountUserForgotPasswordChangeView, AccountUserForgotPasswordChangePresenter>,
        IAccountUserForgotPasswordChangeView
    {
        #region //Events
        /// <summary>
        /// Occurs when [submit].
        /// </summary>
        public event EventHandler Submit;
        #endregion

        #region //Page Methods
        /// <summary>
        /// Called when [submit_ on click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            if (this.Submit != null)
            {
                this.Submit(sender, e);
            }
        }
        #endregion

        #region //Presenter Methods
        /// <summary>
        /// Gets the account user GUID.
        /// </summary>
        /// <returns></returns>
        public Guid GetForgotPasswordGuid()
        {
            return Guid.Parse(this.Request.QueryString[QueryStringKeys.TOKEN]);
        }

        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <returns></returns>
        public string GetPassword()
        {
            return this.txtPassword.Text;
        }

        /// <summary>
        /// Gets the password confirm.
        /// </summary>
        /// <returns></returns>
        public string GetPasswordConfirm()
        {
            return this.txtPasswordConfirm.Text;
        }

        public void ShowErrorInvalidGuid()
        {
            this.mvChangePassword.SetActiveView(this.vwInvalidGuid);
        }

        public void ShowErrorPasswordConfirmation()
        {
            this.lblErrorInvalidPassword.Visible = true;
        }

        /// <summary>
        /// Shows the success password changed.
        /// </summary>
        public void ShowSuccessPasswordChanged()
        {
            this.mvChangePassword.SetActiveView(this.vwChangePasswordSuccess);
        }
        #endregion
    }
}