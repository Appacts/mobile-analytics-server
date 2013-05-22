using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppActs.Mvp.Web;
using AppActs.Mvp.View;
using AppActs.Mvp.Presenter;

namespace AppActs.Client.WebSite.App_Base
{
    public abstract class MvpMasterPage<TView, TPresenter> : MvpMasterPageBase<TView, TPresenter>
        where TView : class, IViewBase
        where TPresenter : PresenterBase<TView>
    {
        public bool IsSecureConnection
        {
            get { return this.Request.IsSecureConnection; }
        }


        protected override void OnLoad(EventArgs e)
        {
            Presenter.Init();

            if (!this.IsPostBack)
            {
                Presenter.OnViewInitialized();
            }

            base.OnLoad(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            Presenter.OnViewLoaded();
        }

        public void RedirectToSecure()
        {
            this.Response.Redirect(this.Request.Url.ToString().Replace("http://", "https://"));
        }

        public void RedirectToLogin()
        {
            this.Response.Redirect("~/Login.aspx");
        }
    }
}