using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AppActs.Client.View;
using AppActs.Model;
using log4net;
using Mosaic.Mvp.Pipeline.Interface;
using AppActs.Client.Presenter.Enum;
using AppActs.Client.View.EventArg;
using AppActs.Client.Service.Interface;
using AppActs.Core.Exceptions;
using AppActs.Client.Model.Enum;
using AppActs.Client.Model;

namespace AppActs.Client.Presenter
{
    public class SetupPresenter : LoggedInPresenter<ISetupView>
    {
        private readonly IPipeline pipeline;
        private readonly IApplicationService applicationService;

        public SetupPresenter(IPipeline iPipeline, ISetupView view,
            User user, ILog log, AppActs.Client.Model.Settings settings, IApplicationService applicationService)
            : base(user, log, settings)
        {
            this.pipeline = iPipeline;
            this.applicationService = applicationService;
        }

        public override void OnViewLoaded()
        {
            this.pipeline.Send<EventArgs<ScreenType>>
                (this, new EventArgs<ScreenType>(ScreenType.Applications), 
                (int)MessageType.Screen);

            base.OnViewLoaded();
        }


        public override void OnViewInitialized()
        {
            try
            {
                IEnumerable<Application> applications = this.applicationService.GetAll();

                this.pipeline.Send<EventArgs<IEnumerable<Application>>>(this, new EventArgs<IEnumerable<Application>>(applications), (int)MessageType.AppsList);
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
