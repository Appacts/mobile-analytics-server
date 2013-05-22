using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppActs.Mvp.Web;
using AppActs.Mvp.View;
using AppActs.Mvp.Presenter;

namespace AppActs.Client.WebSite.App_Base
{
    public class MvpUserControl<TView, TPresenter> : MvpUserControlBase<TView, TPresenter>
        where TView : class, IViewBase
        where TPresenter : PresenterBase<TView>
    {
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


        public bool IsSecureConnection
        {
            get { return this.Request.IsSecureConnection; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Presenter.Init();
        }


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

        public virtual void RedirectToLogin()
        {
            this.Response.Redirect("~/login.aspx");
        }

        public void RedirectToSecure()
        {
            this.Response.Redirect(this.Request.Url.ToString().Replace("http://", "https://"));
        }
    }
}