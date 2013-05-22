using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Client.View;
using AppActs.Model;
using AppActs.Client.Service.Interface;
using log4net;
using AppActs.Client.View.EventArg;
using AppActs.Core.Exceptions;
using Mosaic.Mvp.Pipeline.Interface;
using AppActs.Client.Presenter.Enum;
using AppActs.Client.Model;
using MongoDB.Bson;

namespace AppActs.Client.Presenter
{
    public class SetupAppViewPresenter : LoggedInPresenter<ISetupAppViewView>,
        IReceiver<EventArgs<Application>>,
        IReceiver<EventArgs<IEnumerable<Application>>>
    {
        private readonly IApplicationService applicationService;
        private readonly IEmailService emailService;
        private readonly IPipeline pipeline;

        public SetupAppViewPresenter(ISetupAppViewView view, User user, 
            ILog log, AppActs.Client.Model.Settings settings, IApplicationService applicationService,
            IEmailService emailService, IPipeline pipeline)
            : base(user, log, settings)
        {
            this.View = view;
            this.applicationService = applicationService;
            this.emailService = emailService;
            this.pipeline = pipeline;
        }

        public override void Init()
        {
            this.View.Selected += this.OnSelected;

            this.pipeline.Register(this, (int)MessageType.SetupAppView);
            this.pipeline.Register(this, (int)MessageType.AppsList);

            base.Init();
        }

        public void OnSelected(object sender, EventArgs<Guid> e)
        {
            try
            {
                Application application = this.applicationService.Get(e.ValueOne);
                this.loadApplicationInfo(application);
            }
            catch (Exception ex)
            {
                this.pipeline.Send<EventArgs<Exception>>(this, new EventArgs<Exception>(ex), (int)MessageType.Error);
                this.Logger.Error(ex);
            }
        }

        public void Receive(object sender, EventArgs<IEnumerable<Application>> e, int messageId)
        {
            this.View.Populate(e.ValueOne);
        }

        public void Receive(object sender, EventArgs<Application> e, int messageId)
        {
            this.loadApplicationInfo(e.ValueOne);
        }

        public void Dispose()
        {
            this.pipeline.Remove(this, (int)MessageType.SetupAppView);
            this.pipeline.Remove(this, (int)MessageType.AppsList);
        }

        private void loadApplicationInfo(Application application)
        {
            try
            {
                this.View.Set(application.Guid, application.Guid.ToString());
            }
            catch (Exception ex)
            {
                this.pipeline.Send<EventArgs<Exception>>(this, new EventArgs<Exception>(ex), (int)MessageType.Error);
                this.Logger.Error(ex);
            }
        }
    }
}
