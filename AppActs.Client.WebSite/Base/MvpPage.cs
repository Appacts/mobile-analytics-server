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
    /// Summary description for MvpPage
    /// </summary>
    public class MvpPage<TView, TPresenterFrom, TPresenterTo>
        : MvpPageBase<TView, TPresenterFrom, TPresenterTo>
        where TView : class, IViewBase
        where TPresenterTo : PresenterBase<TView>, TPresenterFrom
    {
        #region //Public Properties
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

            Presenter.Init();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Page.LoadComplete"/> event at the end of the page load stage.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoadComplete(EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Presenter.OnViewInitialized();
            }

            base.OnLoadComplete(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Page.SaveStateComplete"/> event after the page state has been saved to the persistence medium.
        /// </summary>
        /// <param name="e">A <see cref="T:System.EventArgs"/> object containing the event data.</param>
        protected override void OnSaveStateComplete(EventArgs e)
        {
            base.OnSaveStateComplete(e);
            Presenter.OnViewLoaded();
        }

        /// <summary>
        /// Redirects to login.
        /// </summary>
        public virtual void RedirectToLogin()
        {
            this.Response.Redirect("/?login=true");
        }

        /// <summary>
        /// Determines whether this instance is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsValid()
        {
            return base.IsValid;
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