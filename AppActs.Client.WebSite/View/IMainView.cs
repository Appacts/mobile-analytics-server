using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Client.Model;
using AppActs.Model;

namespace AppActs.Client.View
{
    public interface IMainView : IViewLoggedIn
    {
        void Set(DatePicker datePicker, DatePicker datePickerCompare);
        void Set(IEnumerable<Application> applications);

        void SetSelected(Application application);

        void RedirectToApps();

        void Populate(IEnumerable<ReportDefinition> reportDefinitions);

        void Populate(IEnumerable<ReportDefinitionReportDetail> reportDefinitionDetails);
    }
}
