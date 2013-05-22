using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using AppActs.Client.WebSite.App_Base;
using AppActs.Client.Interface;
using AppActs.Client.Presenter.Interface;
using AppActs.Client.Presenter;

namespace AppActs.Client.WebSite.Setup
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Default : MvpPage<ISetupView, ISetupPresenter, SetupPresenter>,
        ISetupView
    {
        #region //Events

        #endregion

        #region //Page Methods

        #endregion

        #region //Methods
        /// <summary>
        /// Shows the general error.
        /// </summary>
        public void ShowErrorSystemGeneral()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}