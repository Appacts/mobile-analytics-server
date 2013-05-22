using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Client.View;
using AppActs.Client.Service.Interface;
using AppActs.Model;
using log4net;
using AppActs.Core.Exceptions;
using AppActs.Client.Model;
using Mosaic.Mvp.Pipeline.Interface;
using AppActs.Client.Model.Enum;
using AppActs.Client.View.EventArg;
using AppActs.Client.Presenter.Enum;

namespace AppActs.Client.Presenter
{
    public class MainPresenter : LoggedInPresenter<IMainView>
    {
        private readonly IApplicationService applicationService;
        private readonly IPipeline pipeline;
        private readonly IReportService reportService;

        public MainPresenter(IMainView view, IPipeline pipeline, IApplicationService applicationService,
            IReportService reportService, User user, ILog log, 
            AppActs.Client.Model.Settings settings) 
            : base(user, log, settings)
        {
            this.View = view;
            this.applicationService = applicationService;
            this.pipeline = pipeline;
            this.reportService = reportService;
        }

        public override void OnViewLoaded()
        {
            this.pipeline.Send<EventArgs<ScreenType>>(this, new EventArgs<ScreenType>(ScreenType.Main), (int)MessageType.Screen);
            base.OnViewLoaded();
        }

        public override void OnViewInitialized()
        {
            try
            {
                IEnumerable<Application> applications = this.applicationService.GetAll();

                if (applications.Count() > 0)
                {
                    this.View.Set(applications);

                    Application application = applications.First();
                    this.View.SetSelected(application);

                    DatePicker datePicker = new DatePicker(DateTime.Today.AddDays(-7), DateTime.Today);
                    DatePicker datePickerCompare = new DatePicker(DateTime.Today.AddDays(-14), DateTime.Today.AddDays(-7));

                    this.View.Set(datePicker, datePickerCompare);

                    this.View.Populate
                        (
                            this.reportService.GetReportDefinitions()
                                .Where(x => x.Active)
                        );

                    this.View.Populate
                        (
                            this.reportService.GetReportDefinitions()
                                .Where(x => x.Active && x.ReportNormal.Detail.Guid != Guid.Empty)
                                .Select(x => x.ReportNormal.Detail)
                        );
                }
                else
                {
                    this.View.RedirectToApps();
                }
            }
            catch (Exception ex)
            {
                this.pipeline.Send<EventArgs<Exception>>(this, new EventArgs<Exception>(ex), (int)MessageType.Error);
                this.Logger.Error(ex);
            }

            base.OnViewInitialized();
        }
    }
}
