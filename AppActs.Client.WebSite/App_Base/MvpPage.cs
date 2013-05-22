using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppActs.Mvp.Web;
using AppActs.Mvp.View;
using AppActs.Mvp.Presenter;

namespace AppActs.Client.WebSite.App_Base
{
    public class MvpPage<TView, TPresenter> : MvpPageBase<TView, TPresenter>
        where TView : class, IViewBase
        where TPresenter : PresenterBase<TView>
    {
        public bool IsSecureConnection 
        { 
            get { return this.Request.IsSecureConnection; } 
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Presenter.Init();
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Presenter.OnViewInitialized();
            }

            base.OnLoadComplete(e);
        }

        protected override void OnSaveStateComplete(EventArgs e)
        {
            base.OnSaveStateComplete(e);
            Presenter.OnViewLoaded();
        }

        public virtual void RedirectToLogin()
        {
            this.Response.Redirect("~/login.aspx");
        }

        public virtual bool IsValid()
        {
            return base.IsValid;
        }

        public void RedirectToSecure()
        {
            this.Response.Redirect(this.Request.Url.ToString().Replace("http://", "https://"));
        }
    }
}