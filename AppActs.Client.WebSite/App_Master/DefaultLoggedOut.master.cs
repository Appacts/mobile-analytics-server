using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppActs.Client.WebSite.App_Master
{
    public partial class DefaultLoggedOut : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request.QueryString.Count != 0)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["login"]))
                {
                    this.hypLogin.CssClass = "select";
                }
            }
            else
            {
                this.hypLogin.CssClass = "select";
            }
        }
    }
}