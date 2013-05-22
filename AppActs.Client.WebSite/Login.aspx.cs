using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppActs.Client.WebSite
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request.QueryString["forgot-password"] != null)
            {
                this.mvMain.SetActiveView(vwForgotPassword);
            }
            else
            {
                this.mvMain.SetActiveView(vwLogin);
            }
        }
    }
}