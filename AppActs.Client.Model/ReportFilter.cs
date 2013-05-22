using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Model;
using AppActs.Model.Enum;

namespace AppActs.Client.Model
{
    public class ReportFilter
    {
        public DatePicker DatePicker { get; set; }
        public Application Application { get; set; }
        public List<Application> CompareToApplications { get; set; }
        public List<string> CompareToVersions { get; set; }
        public List<PlatformType> CompareToPlatforms { get; set; }

        public ReportFilter()
        {
            this.DatePicker = new DatePicker(DateTime.Now.AddMonths(-1).Date, DateTime.Now.Date);
            this.CompareToApplications = new List<Application>();
            this.CompareToVersions = new List<string>();
            this.CompareToPlatforms = new List<PlatformType>();
        }
        

        public void Consume(ReportFilter reportFilter)
        {
            this.DatePicker = reportFilter.DatePicker;
            this.Application = reportFilter.Application;
            this.CompareToApplications = reportFilter.CompareToApplications;
            this.CompareToVersions = reportFilter.CompareToVersions;
            this.CompareToPlatforms = reportFilter.CompareToPlatforms;
        }

        public void Clear()
        {
            this.Application = null;
            this.CompareToApplications.Clear();
            this.CompareToVersions.Clear();
            this.CompareToPlatforms.Clear();
        }
    }
}
