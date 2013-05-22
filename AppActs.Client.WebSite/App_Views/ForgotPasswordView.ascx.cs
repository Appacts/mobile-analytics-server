using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using AppActs.Client.WebSite.App_Base;
using AppActs.Client.View;
using AppActs.Client.Presenter;

namespace AppActs.Client.WebSite.App_Views
{
    public partial class ForgotPasswordView : MvpUserControl<IAccountUserForgotPasswordView, AccountUserForgotPasswordPresenter>,
        IAccountUserForgotPasswordView
    {
        #region //Event
        /// <summary>
        /// Occurs when [submit].
        /// </summary>
        public event EventHandler Submit;
        #endregion

        #region //Page methods
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
        /// Gets the email.
        /// </summary>
        /// <returns></returns>
        public string GetEmail()
        {
            return this.txtEmail.Text;
        }

        /// <summary>
        /// Determines whether this instance is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValid()
        {
            return this.reqEmail.IsValid;
        }

        /// <summary>
        /// Shows the error no such email.
        /// </summary>
        public void ShowErrorNoSuchEmail()
        {
            this.lblErrorNoEmailAddress.Visible = true;
        }

        /// <summary>
        /// Shows the error invalid email.
        /// </summary>
        public void ShowErrorInvalidEmail()
        {
            this.lblErrorInvalidEmailAddress.Visible = true;
        }

        /// <summary>
        /// Shows the success email sent.
        /// </summary>
        /// <param name="email">The email.</param>
        public void ShowSuccessEmailSent(string email)
        {
            this.lblSuccessForgotPasswordRequest.Text = String.Format(Resources.Messages.SuccessForgotPasswordRequest, email);
            this.mvForgotPassword.SetActiveView(vwSuccesfull);
        }
        #endregion
    }
}