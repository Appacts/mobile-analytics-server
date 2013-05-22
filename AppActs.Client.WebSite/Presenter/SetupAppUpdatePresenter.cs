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
using AppActs.Client.View.EventArg;
using AppActs.Client.Presenter.Enum;
using Mosaic.Mvp.Pipeline.Interface;
using MongoDB.Bson;
using AppActs.Model.Enum;

namespace AppActs.Client.Presenter
{
    public class SetupAppUpdatePresenter : LoggedInPresenter<ISetupAppUpdateView>, 
        IReceiver<EventArgs<IEnumerable<Application>>>
    {
        private readonly IApplicationService applicationService;
        private readonly IEmailService emailService;
        private readonly IPipeline pipeline;

        public SetupAppUpdatePresenter(ISetupAppUpdateView view, User user, 
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
            this.View.Update += this.OnUpdate;
            this.View.Remove += this.OnRemove;
            this.View.Selected += this.OnSelected;

            this.pipeline.Register(this, (int)MessageType.AppsList);

            base.Init();
        }

        public void OnSelected(object sender, EventArgs<Guid> e)
        {
            try
            {
                Application application = 
                    this.applicationService.Get(e.ValueOne);

                this.View.Populate(this.applicationService.GetPlatforms());
                this.View.Set(application.Platforms);
                this.View.SetApplicationName(application.Name);
                this.View.SetApplicationId(application.Guid);
            }
            catch (Exception ex)
            {
                this.pipeline.Send<EventArgs<Exception>>(this, new EventArgs<Exception>(ex), (int)MessageType.Error);
                this.Logger.Error(ex);
            }
        }

        public void OnUpdate(object sender, EventArgs e)
        {
            try
            {
                bool error = false;

                Application application = this.applicationService.Get(this.View.GetApplicationId());
                IEnumerable<Platform> platformsAvailable = this.applicationService.GetPlatforms();

                if(application.Name != this.View.GetApplicationName())
                {
                    bool isNameAvailable = 
                        this.applicationService.IsApplicationNameAvailable(this.View.GetApplicationName());

                    if(!isNameAvailable)
                    {
                        error = true;
                        this.View.ShowErrorNameTaken();
                    }
                }

                IEnumerable<PlatformType> platformIds = this.View.GetPlatforms();
                List<Platform> platformsSelected = new List<Platform>();

                if (!error)
                {
                    if (platformIds != null && platformIds.Count() > 0)
                    {
                        platformsSelected = platformsAvailable.Where(x => platformIds.Contains(x.Type)).ToList();
                    }
                    else
                    {
                        error = true;
                        this.View.ShowErrorPlatformNotSelected();
                    }
                }

                if (!error)
                {
                    application.Name = this.View.GetApplicationName();
                    application.Platforms = platformsSelected;
                    this.applicationService.Update(application);

                    this.View.Clear();
                    this.View.Populate(new List<Platform>());

                    IEnumerable<Application> applications = this.applicationService.GetAll();
                    this.pipeline.Send<EventArgs<IEnumerable<Application>>>(sender, new EventArgs<IEnumerable<Application>>(applications), (int)MessageType.AppsList);
                    this.pipeline.Send<EventArgs<Application>>(sender, new EventArgs<Application>(application), (int)MessageType.SetupAppView);
                }
            }
            catch (Exception ex)
            {
                this.pipeline.Send<EventArgs<Exception>>(this, new EventArgs<Exception>(ex), (int)MessageType.Error);
                this.Logger.Error(ex);
            }
        }

        public void OnRemove(object sender, EventArgs e)
        {
            try
            {
                Application application = this.applicationService.Get(this.View.GetApplicationId());
                application.Active = false;
                this.applicationService.Update(application);
                this.View.ShowSuccessRemove(application.Name);
                this.View.Clear();

                IEnumerable<Application> applications = this.applicationService.GetAll();
                this.pipeline.Send<EventArgs<IEnumerable<Application>>>(sender, new EventArgs<IEnumerable<Application>>(applications), (int)MessageType.AppsList);
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

        public void Dispose()
        {
            this.pipeline.Remove(this, (int)MessageType.AppsList);
        }
    }
}
