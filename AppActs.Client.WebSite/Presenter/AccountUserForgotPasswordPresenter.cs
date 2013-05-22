using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Client.View;
using log4net;
using AppActs.Client.Service.Interface;
using AppActs.Core.Exceptions;
using AppActs.Model;
using Mosaic.Mvp.Pipeline.Interface;
using AppActs.Client.View.EventArg;
using AppActs.Client.Presenter.Enum;
using AppActs.Client.Model;

namespace AppActs.Client.Presenter
{
    public class AccountUserForgotPasswordPresenter : Presenter<IAccountUserForgotPasswordView>
    {
        private readonly IUserService accountUserService;
        private readonly IEmailService emailService;
        private readonly IPipeline pipeline; 

        public AccountUserForgotPasswordPresenter(IAccountUserForgotPasswordView view,
            IUserService accountUserService, 
            IEmailService emailService, ILog log, 
            AppActs.Client.Model.Settings settings, IPipeline pipeline)
            : base(log, settings)
        {
            this.View = view;
            this.accountUserService = accountUserService;
            this.emailService = emailService;
            this.pipeline = pipeline;
        }

        public override void Init()
        {
            this.View.Submit += this.OnSubmit;    
            base.Init();
        }

        public void OnSubmit(object sender, EventArgs e)
        {
            try
            {
                if (this.View.IsValid())
                {
                    User accountUser =
                        this.accountUserService.GetUser(this.View.GetEmail());

                    if (accountUser != null)
                    {
                        accountUser.ForgotPasswordGuid = Guid.NewGuid();
                        accountUser.DateModified = DateTime.Now;
                        this.accountUserService.Update(accountUser);

                        this.emailService.SendUserForgotPassword(accountUser, accountUser.ForgotPasswordGuid);
                        this.View.ShowSuccessEmailSent(this.View.GetEmail());
                    }
                    else
                    {
                        this.View.ShowErrorNoSuchEmail();
                    }
                }
                else
                {
                    this.View.ShowErrorInvalidEmail();
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
