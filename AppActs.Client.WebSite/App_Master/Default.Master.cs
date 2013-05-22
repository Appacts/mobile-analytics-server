using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using AppActs.Client.WebSite.App_Base;
using AppActs.Client.View;
using AppActs.Client.Presenter;

namespace AppActs.Client.WebSite.App_Master
{
    public partial class Default : MvpMasterPage<IDefaultView, DefaultPresenter>,
        IDefaultView
    {
        public void ShowErrorMessage()
        {
            this.divError.Visible = true;
        }
    }
}