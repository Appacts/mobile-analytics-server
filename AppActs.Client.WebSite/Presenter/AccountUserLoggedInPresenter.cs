using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Client.View;
using log4net;
using AppActs.Model;
using AppActs.Client.Service.Interface;
using AppActs.Client.Model;
using AppActs.Client.Model.Enum;
using AppActs.Core.Exceptions;
using System.Reflection;
using Mosaic.Mvp.Pipeline.Interface;
using AppActs.Client.Presenter.Enum;
using AppActs.Client.View.EventArg;

namespace AppActs.Client.Presenter
{
    public class AccountUserLoggedInPresenter : 
        LoggedInPresenter<IAccountUserLoggedInView>, 
        IReceiver<EventArgs<ScreenType>>
    {
        private readonly IUserService accountUserService;
        private readonly IEmailService emailService;
        private readonly IPipeline pipeline;
        private readonly ReportFilter reportFilter;

        public AccountUserLoggedInPresenter(IPipeline pipeline, IAccountUserLoggedInView view,
            IUserService accountUserService, IEmailService emailService, User user, ReportFilter reportFilter, ILog log, 
            AppActs.Client.Model.Settings settings)
            : base(user, log, settings)
        {
            this.View = view;
            this.accountUserService = accountUserService;
            this.pipeline = pipeline;
            this.reportFilter = reportFilter;
            this.emailService = emailService;
        }

        public override void Init()
        {
            this.View.Logout += this.OnLogout;

            this.pipeline.Register(this, (int)MessageType.Screen);

            base.Init();
        }

        public void OnLogout(object sender, EventArgs e)
        {
            this.user.Clear();
            this.reportFilter.Clear();
            this.View.RedirectToLogin();
        }

        public void Receive(object sender, EventArgs<ScreenType> e, int messageId)
        {
            this.View.Set(e.ValueOne);
        }

        public void Dispose()
        {
            this.pipeline.Remove(this, (int)MessageType.Screen);
        }
    }
}
