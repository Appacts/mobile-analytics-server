using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppActs.Client.View;
using AppActs.Client.Presenter;
using AppActs.Client.WebSite.App_Base;
using AppActs.Model;
using MongoDB.Bson;
using AppActs.Client.View.EventArg;
using AppActs.Model.Enum;

namespace AppActs.Client.WebSite.App_Views
{
    public partial class SetupAppUpdateView : MvpUserControl<ISetupAppUpdateView, SetupAppUpdatePresenter>,
        ISetupAppUpdateView
    {
        public event EventHandler Update;
        public event EventHandler Remove;
        public event EventHandler<EventArgs<Guid>> Selected;

        protected void ddlApps_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Selected != null)
            {
                this.Selected(sender, new EventArgs<Guid>(Guid.Parse(this.ddlApps.SelectedItem.Value)));
            }
        }

        protected void btnRemove_OnClick(object sender, EventArgs e)
        {
            if (this.Remove != null)
            {
                this.Remove(sender, e);
            }
        }

        protected void btnUpdate_OnClick(object sender, EventArgs e)
        {
            if (this.Update != null)
            {
                this.Update(sender, e);
            }
        }

        public void ShowErrorPlatformNotSelected()
        {
            this.lblErrorPlatformNeedsToBeSelected.Visible = true;
        }

        public void ShowErrorNameTaken()
        {
            this.lblErrorNameTaken.Visible = true;
        }

        public IEnumerable<PlatformType> GetPlatforms()
        {
            return this.ddlPlatform.Items.OfType<ListItem>().Where(x => x.Selected)
                .Select(x => (PlatformType)int.Parse(x.Value));
        }

        public void Populate(IEnumerable<AppActs.Model.Platform> platforms)
        {
            this.ddlPlatform.DataSource = platforms;
            this.ddlPlatform.DataBind();
            
        }

        public void Set(IEnumerable<Platform> list)
        {
            IEnumerable<int> ids = list.Select(x => (int)x.Id);
            this.ddlPlatform.Items.OfType<ListItem>()
                .Where(x => ids.Contains(int.Parse(x.Value)))
                .ToList().ForEach((ListItem listItem) => { listItem.Selected = true; });
        }

        public void SetApplicationName(string name)
        {
            this.txtName.Text = name;
        }

        public void SetApplicationId(Guid id)
        {
            this.hdnApplicationId.Value = id.ToString();
        }

        public string GetApplicationName()
        {
            return this.txtName.Text.Trim();
        }

        public Guid GetApplicationId()
        {
            return Guid.Parse(this.hdnApplicationId.Value);
        }

        public void ShowSuccessRemove(string applicationName)
        {
            this.lblSuccessRemoved.Text = String.Format(Resources.Messages.SuccessAppRemoved, applicationName);
            this.lblSuccessRemoved.Visible = true;
        }

        public void Clear()
        {
            this.txtName.Text = null;
            this.ddlPlatform.ClearSelection();
            this.ddlApps.ClearSelection();
        }

        public void Populate(IEnumerable<AppActs.Model.Application> applications)
        {
            this.ddlApps.DataSource = applications;
            this.ddlApps.DataBind();
            this.ddlApps.Items.Insert(0, new ListItem(Resources.Messages.PleaseSelect, CommonValues.ZERO));
        }

        public bool IsValid()
        {
            return this.reqName.IsValid && this.reqPlatform.IsValid && this.reqApps.IsValid;
        }
    }
}