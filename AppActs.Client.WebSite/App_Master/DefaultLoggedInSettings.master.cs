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
    public partial class DefaultLoggedInSettings : MvpMasterPage<ISettingsView, SettingsPresenter>,
        ISettingsView

    {
        public void Set(string fullName)
        {
            this.lblName.Text = fullName;
        }

        public void Set(ScreenType screenType)
        {
            switch (screenType)
            {
                case ScreenType.Main:
                    break;
                case ScreenType.Applications:
                    this.hypApps.CssClass = "selected";
                    break;
                case ScreenType.Account:
                    this.hypAccount.CssClass = "selected";
                    break;
                case ScreenType.Settings:
                    this.hypUsers.CssClass = "selected";
                    break;
            }
        }
    }
}