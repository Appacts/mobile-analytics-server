using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppActs.Client.WebSite.App_Base;
using AppActs.Client.View;
using AppActs.Client.Presenter;
using AppActs.Model;
using AppActs.Client.Model;
using MongoDB.Bson;
using AppActs.Client.View.EventArg;

namespace AppActs.Client.WebSite.App_Views
{
    public partial class SetupAppViewView : MvpUserControl<ISetupAppViewView, SetupAppViewPresenter>,
        ISetupAppViewView
    {
        public event EventHandler<EventArgs<Guid>> Selected;

        protected void ddlApps_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Selected != null)
            {
                this.Selected(sender, new EventArgs<Guid>(Guid.Parse(this.ddlApps.SelectedItem.Value)));
            }
        }

        public void Populate(IEnumerable<Application> applications)
        {
            this.ddlApps.DataSource = applications;
            this.ddlApps.DataBind();
            this.ddlApps.Items.Insert(0, new ListItem(Resources.Messages.PleaseSelect, CommonValues.ZERO));
        }

        public void Set(Guid applicationId, string displayApplicationId)
        {
            this.ddlApps.Items.FindByValue(applicationId.ToString()).Selected = true;
            this.lblApplicationId.Text = displayApplicationId;
            this.divApplication.Visible = true;

            this.Page.ClientScript.RegisterStartupScript(this.GetType(), this.GetType().FullName, "window.scrollTo(0, document.body.scrollHeight);", true);
        }
    }
}