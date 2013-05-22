using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppActs.Mvp.Web;
using AppActs.Mvp.View;
using AppActs.Mvp.Presenter;

namespace AppActs.Client.WebSite.Base
{
    /// <summary>
    /// Summary description for MvpUserControl
    /// </summary>
    public class MvpUserControl<TView, TPresenterFrom, TPresenterTo>
        : MvpUserControlBase<TView, TPresenterFrom, TPresenterTo>
        where TView : class, IViewBase
        where TPresenterTo : PresenterBase<TView>, TPresenterFrom
    {
        #region //Public Properties
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MvpUserControl&lt;TView, TPresenterFrom, TPresenterTo&gt;"/> is initialized.
        /// </summary>
        /// <value><c>true</c> if initialized; otherwise, <c>false</c>.</value>
        private bool ViewInitialized
        {
            get
            {
                if (ViewState["Initialized"] != null)
                {
                    return (bool)ViewState["Initialized"];
                }

                return false;
            }
            set { ViewState["Initialized"] = value; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is secure connection.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is secure connection; otherwise, <c>false</c>.
        /// </value>
        public bool IsSecureConnection
        {
            get { return this.Request.IsSecureConnection; }
        }
        #endregion

        #region //Methods
        /// <summary>
        /// Raises the <see cref="E:Load"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //All controls need to be init
            Presenter.Init();
        }

        /// <summary>
        /// Saves any user control view-state changes that have occurred since the last page postback.
        /// </summary>
        /// <returns>
        /// Returns the user control's current view state. If there is no view state associated with the control, it returns null.
        /// </returns>
        protected override object SaveViewState()
        {
            if (this.Visible == true)
            {
                if (!this.ViewInitialized)
                {
                    Presenter.OnViewInitialized();
                    this.ViewInitialized = true;
                }

                Presenter.OnViewLoaded();
            }

            return base.SaveViewState();
        }

        /// <summary>
        /// Redirects to login.
        /// </summary>
        public virtual void RedirectToLogin()
        {
            this.Response.Redirect("/?login=true");
        }

        /// <summary>
        /// Redirects to secure.
        /// </summary>
        public void RedirectToSecure()
        {
            this.Response.Redirect(this.Request.Url.ToString().Replace("http://", "https://"));
        }
        #endregion
    }
}