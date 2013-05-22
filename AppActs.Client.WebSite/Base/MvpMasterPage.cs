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
    /// Summary description for MvpMasterPage
    /// </summary>
    public class MvpMasterPage<TView, TPresenterFrom, TPresenterTo>
        : MvpMasterPageBase<TView, TPresenterFrom, TPresenterTo>
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
        /// Raises the <see cref="E:System.Web.UI.Control.Load"/> event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            Presenter.Init();

            if (!this.IsPostBack)
            {
                Presenter.OnViewInitialized();
            }

            base.OnLoad(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            Presenter.OnViewLoaded();
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