using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using AppActs.Client.WebSite.App_Base;
using AppActs.Client.View;
using AppActs.Client.Presenter;

namespace AppActs.Client.WebSite.Apps
{
    public partial class Default : MvpPage<ISetupView, SetupPresenter>,
        ISetupView
    {

    }
}