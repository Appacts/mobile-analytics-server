using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Client.View;
using AppActs.Client.Service.Interface;
using log4net;
using AppActs.Core.Exceptions;
using AppActs.Model;
using AppActs.Client.Model;
using Mosaic.Mvp.Pipeline.Interface;
using AppActs.Client.Presenter.Enum;
using AppActs.Client.View.EventArg;

namespace AppActs.Client.Presenter
{
    public class AccountUserForgotPasswordChangePresenter : Presenter<IAccountUserForgotPasswordChangeView>
    {
        private readonly IUserService accountUserService;
        private readonly IPipeline pipeline; 


        public AccountUserForgotPasswordChangePresenter(IAccountUserForgotPasswordChangeView view, 
            IUserService accountUserService, ILog log, AppActs.Client.Model.Settings settings, IPipeline pipeline)
            : base(log, settings)
        {
            this.View = view;
            this.accountUserService = accountUserService;
            this.pipeline = pipeline;
        }

        public override void Init()
        {
            this.View.Submit += this.OnSubmit;    
            base.Init();
        }

        public override void OnViewInitialized()
        {
            try
            {
                User accountUser = 
                    this.accountUserService.GetUserByForgotPassword(this.View.GetForgotPasswordGuid());

                if (accountUser == null)
                {
                    this.View.ShowErrorInvalidGuid();
                }
                else
                {
                    if (!accountUser.Active)
                    {
                        this.View.ShowErrorInvalidGuid();
                    }
                }
            }
            catch (Exception ex)
            {
                this.pipeline.Send<EventArgs<Exception>>(this, new EventArgs<Exception>(ex), (int)MessageType.Error);
                this.Logger.Error(ex);
            }

            base.OnViewInitialized();
        }

        public void OnSubmit(object sender, EventArgs e)
        {
            try
            {
                if(this.View.IsValid() && (this.View.GetPassword().Equals(this.View.GetPasswordConfirm())))
                {
                    User accountUser = 
                        this.accountUserService.GetUserByForgotPassword(this.View.GetForgotPasswordGuid());

                    if (accountUser != null && accountUser.Active)
                    {
                        //update password
                        this.accountUserService
                            .UpdatePassword(accountUser.Id, this.View.GetPassword());

                        //de-activate token
                        accountUser.ForgotPasswordGuid = Guid.NewGuid();
                        accountUser.DateModified = DateTime.Now;
                        this.accountUserService.Update(accountUser);

                        this.View.ShowSuccessPasswordChanged();
                    }
                    else
                    {
                        this.View.ShowErrorInvalidGuid();
                    }
                }
                else
                {
                    this.View.ShowErrorPasswordConfirmation();
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
