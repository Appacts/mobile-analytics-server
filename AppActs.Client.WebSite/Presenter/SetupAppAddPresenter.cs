using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Client.View;
using AppActs.Model;
using log4net;
using AppActs.Client.Service.Interface;
using AppActs.Core.Exceptions;
using AppActs.Client.Model;
using Mosaic.Mvp.Pipeline.Interface;
using AppActs.Client.View.EventArg;
using AppActs.Client.Presenter.Enum;
using MongoDB.Bson;
using AppActs.Model.Enum;
using System.Web.Script.Serialization;

namespace AppActs.Client.Presenter
{
    public class SetupAppAddPresenter : LoggedInPresenter<ISetupAppAddView>
    {
        private readonly IApplicationService applicationService;
        private readonly IEmailService emailService;
        private readonly IPipeline pipeline;


        public SetupAppAddPresenter(ISetupAppAddView view, User accountUser, 
            ILog iLog, AppActs.Client.Model.Settings settings, IApplicationService applicationService,
            IEmailService emailService, IPipeline pipeline)
            : base(accountUser, iLog, settings)
        {
            this.View = view;
            this.applicationService = applicationService;
            this.emailService = emailService;
            this.pipeline = pipeline;
        }

        public override void Init()
        {
            this.View.Add += this.OnAdd;
            base.Init();
        }

        public override void OnViewInitialized()
        {
            try
            {
                IEnumerable<Platform> platforms = this.applicationService.GetPlatforms();
                if (platforms.Count() == 0)
                {
                    platforms = new JavaScriptSerializer()
                        .Deserialize<List<Platform>>(AppActs.Client.WebSite.Properties.Settings.Default.StartupSystemPlatform);

                    this.applicationService.Save(platforms);
                }

                this.View.Populate(platforms);
            }
            catch (Exception ex)
            {
                this.pipeline.Send<EventArgs<Exception>>(this, new EventArgs<Exception>(ex), (int)MessageType.Error);
                this.Logger.Error(ex);
            }

            base.OnViewInitialized();
        }

        public void OnAdd(object sender, EventArgs e)
        {
            try
            {
                bool isNameAvailable = this.applicationService
                    .IsApplicationNameAvailable(this.View.GetApplicationName());

                IEnumerable<PlatformType> platformIds = this.View.GetPlatforms();

                if (isNameAvailable && platformIds.Count() > 0)
                {
                    Application application = new Application(this.View.GetApplicationName());

                    application.Platforms =
                        this.applicationService.GetPlatforms().Where(x => platformIds.Contains(x.Type)).ToList();

                    this.applicationService.Save(application);

                    this.View.Clear();

                    IEnumerable<Application> applications = this.applicationService.GetAll();
                    this.pipeline.Send<EventArgs<IEnumerable<Application>>>(sender, new EventArgs<IEnumerable<Application>>(applications), (int)MessageType.AppsList);
                    this.pipeline.Send<EventArgs<Application>>(sender, new EventArgs<Application>(application), (int)MessageType.SetupAppView);
                }
                else
                {
                    if (!isNameAvailable)
                    {
                        this.View.ShowErrorNameTaken();
                    }

                    if (platformIds.Count() == 0)
                    {
                        this.View.ShowErrorPlatformNotSelected();
                    }
                }
            }
            catch (Exception ex)
            {
                this.pipeline.Send<EventArgs<Exception>>(this, new EventArgs<Exception>(ex), (int)MessageType.Error);
                this.Logger.Error(ex);
            }
        }
    }
}
