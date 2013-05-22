using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppActs.Client.WebSite.App_Base;
using AppActs.Client.View;
using AppActs.Client.Presenter;
using AppActs.Client.Model.Enum;

namespace AppActs.Client.WebSite.App_Master
{
    public partial class DefaultLoggedIn : MvpMasterPage<IAccountUserLoggedInView, AccountUserLoggedInPresenter>,
        IAccountUserLoggedInView
    {
        public event EventHandler Logout;

        protected void btnLogout_OnClick(object sender, EventArgs e)
        {
            if (this.Logout != null)
            {
                this.Logout(sender, e);
            }
        }

        public void Set(ScreenType screenType)
        {
            switch (screenType)
            {
                case ScreenType.Main:
                    this.hypHome.CssClass = "select";
                    break;
                case ScreenType.Applications:
                case ScreenType.Account:
                case ScreenType.Settings:
                case ScreenType.SDKs:
                    this.hypSettings.CssClass = String.Concat(this.hypSettings.CssClass, " select");
                    break;
            }
        }
    }
}