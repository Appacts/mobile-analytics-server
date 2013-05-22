using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppActs.Client.WebSite.App_Base;
using AppActs.Client.View;
using AppActs.Client.Presenter;
using AppActs.Client.Model;
using AppActs.Model;
using System.Web.Script.Serialization;

namespace AppActs.Client.WebSite
{
    public partial class Default : MvpPage<IMainView, MainPresenter>,
        IMainView
    {
        protected Application application;
        protected string jsonDatePicker;
        protected string jsonDatePickerCompare;

        public void Set(DatePicker datePicker, DatePicker datePickerCompare)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new JavaScriptConverter[] { new DatePickerConverter() });
            this.jsonDatePicker = serializer.Serialize(datePicker);
            this.jsonDatePickerCompare = serializer.Serialize(datePickerCompare);
        }

        public void Set(IEnumerable<Application> applications)
        {
            this.ddlApplications.DataSource = applications;
            this.ddlApplications.DataBind();
        }


        public void SetSelected(Application application)
        {
            this.ddlApplications.SelectedValue = application.Guid.ToString();
            this.application = application;
        }

        public void Populate(IEnumerable<ReportDefinition> reportDefinitions)
        {
            this.rptTiles.DataSource = reportDefinitions;
            this.rptTiles.DataBind();
        }

        public void Populate(IEnumerable<ReportDefinitionReportDetail> reportDefinitionDetails)
        {
            this.rptDetails.DataSource = reportDefinitionDetails;
            this.rptDetails.DataBind();
        }

        public void RedirectToApps()
        {
            this.Response.Redirect("~/Apps/", true);
        }
    }
}