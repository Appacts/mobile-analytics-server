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
using MongoDB.Bson;
using AppActs.Model.Enum;

namespace AppActs.Client.WebSite.App_Views
{
    public partial class SetupAppAddView : MvpUserControl<ISetupAppAddView, SetupAppAddPresenter>,
        ISetupAppAddView
    {
        public event EventHandler Add;

        public string GetApplicationName()
        {
            return this.txtName.Text;
        }

        public IEnumerable<PlatformType> GetPlatforms()
        {
            return this.ddlPlatform.Items.OfType<ListItem>()
                .Where(x => x.Selected).Select(x => (PlatformType)int.Parse(x.Value));
        }

        public void Populate(IEnumerable<Platform> platforms)
        {
            this.ddlPlatform.DataSource = platforms;
            this.ddlPlatform.DataBind();
        }

        public void ShowErrorPlatformNotSelected()
        {
            this.lblErrorPlatformNeedsToBeSelected.Visible = true;
        }

        public void Clear()
        {
            this.txtName.Text = null;
            this.ddlPlatform.ClearSelection();
        }

        public void ShowErrorNameTaken()
        {
            this.lblErrorNameTaken.Visible = true;
        }

        public bool IsValid()
        {
            return this.reqName.IsValid && this.reqPlatform.IsValid;
        }

        /// <summary>
        /// Handles the OnClick event of the btnAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnAdd_OnClick(object sender, EventArgs e)
        {
            if (this.Add != null)
            {
                this.Add(sender, e);
            }
        }
    }
}