using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppActs.Client.WebSite.App_Base;
using AppActs.Client.View;
using AppActs.Client.Presenter;
using AppActs.Client.View.EventArg;
using AppActs.Model;
using AppActs.Client.Model;

namespace AppActs.Client.WebSite.Account.Settings
{
    public partial class Default : MvpPage<IAccountUserManagementView, AccountUserManagementPresenter>,
        IAccountUserManagementView
    {
        public event EventHandler<EventArgs<Guid>> Remove;
        public event EventHandler Add;

        protected void btnMemberAdd_OnClick(object sender, EventArgs e)
        {
            if (this.Add != null)
            {
                this.Add(sender, e);
            }
        }


        protected void btnMemberRemove_OnClick(object sender, EventArgs e)
        {
            if (this.Remove != null)
            {
                Button btnMemberRemove = sender as Button;

                this.Remove(sender, new EventArgs<Guid>(Guid.Parse(btnMemberRemove.CommandArgument)));
            }
        }

        protected void grvMembers_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                User accountUser = (User)e.Row.DataItem;

                Button btnMemberRemove = (Button)e.Row.FindControl("btnMemberRemove");
                btnMemberRemove.CommandArgument = accountUser.Guid.ToString();
            }
        }

        public void Populate(IEnumerable<User> accountUsers)
        {
            this.grvMembers.DataSource = accountUsers;
            this.grvMembers.DataBind();
        }

        public string GetName()
        {
            return this.txtName.Text;
        }

        public string GetEmail()
        {
            return this.txtEmail.Text;
        }

        public new bool IsValid()
        {
            return this.reqEmail.IsValid && this.reqName.IsValid;
        }

        public void ShowErrorValidation()
        {
            this.lblErrorValidation.Visible = true;
        }

        public void ShowErrorEmailTaken()
        {
            this.lblErrorEmailTaken.Visible = true;
        }

        public void ShowSuccessfullyAdded()
        {
            this.lblSuccessAdded.Visible = true;
        }

        public void ShowSuccessfullyRemoved(string email)
        {
            this.lblSuccessRemoved.Visible = true;
        }

        public void ShowErrorSystemGeneral()
        {
            this.lblError.Visible = true;
        }

        public void ShowErrorEmailCantBeSent()
        {
            this.lblErrorEmailCantBeSent.Visible = true;
        }

        public void Clear()
        {
            this.txtEmail.Text = string.Empty;
            this.txtName.Text = string.Empty;
        }
    }
}