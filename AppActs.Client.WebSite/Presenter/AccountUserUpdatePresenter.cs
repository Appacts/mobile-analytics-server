using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Client.View;
using AppActs.Client.Service.Interface;
using log4net;
using AppActs.Core.Exceptions;
using AppActs.Model;
using AppActs.Client.Model.Enum;
using Mosaic.Mvp.Pipeline.Interface;
using AppActs.Client.Presenter.Enum;
using AppActs.Client.View.EventArg;
using AppActs.Client.Model;

namespace AppActs.Client.Presenter
{
    public class AccountUserUpdatePresenter : LoggedInPresenter<IAccountUserUpdateView>
    {
        private readonly IUserService accountUserService;
        private readonly IEmailService emailService;
        private readonly IPipeline pipeline;

        public AccountUserUpdatePresenter(IPipeline iPipeline, IAccountUserUpdateView view, 
            IUserService accountUserService, IEmailService emailService, User user, 
            ILog log, AppActs.Client.Model.Settings settings)
            : base(user, log, settings)
        {
            this.View = view;
            this.accountUserService = accountUserService;
            this.emailService = emailService;
            this.pipeline = iPipeline;
        }


        public override void Init()
        {
            this.View.SubmitDetails += this.OnSubmitDetails;
            this.View.SubmitPassword += this.OnSubmitPassword;

            base.Init();
        }


        public override void OnViewInitialized()
        {
            this.View.Populate(this.user);

            base.OnViewInitialized();
        }

        public override void OnViewLoaded()
        {
            this.pipeline.Send<EventArgs<ScreenType>>(this, new EventArgs<ScreenType>(ScreenType.Account), (int)MessageType.Screen);

            base.OnViewLoaded();
        }

        public void OnSubmitDetails(object sender, EventArgs e)
        {
            try
            {
                if (this.View.IsValid())
                {
                    if (this.View.GetName() != this.user.Name)
                    {
                        User accountUserUpdate = new User();

                        accountUserUpdate.Consume(this.user);

                        accountUserUpdate.Name = this.View.GetName();

                        this.accountUserService.Update(accountUserUpdate);

                        this.user.Consume(accountUserUpdate);

                        this.pipeline.Send<EventArgs<User>>(sender, new EventArgs<User>(accountUserUpdate), 
                            (int)MessageType.AccountUser);

                        this.View.ShowUpdateNameComplete();

                        this.View.Populate(accountUserUpdate);
                    }

                    //need to think about this in the future, if they change their email address
                    //dont they need to verify it again? If so, this changes few things, for now it's fine, we will trust our 
                    //users
                    if (this.View.GetEmail() != this.user.Email)
                    {
                        if (this.accountUserService.IsEmailAvailable(this.View.GetEmail()))
                        {
                            User accountUserUpdate = new User();

                            accountUserUpdate.Consume(this.user);

                            accountUserUpdate.Email = this.View.GetEmail();

                            this.accountUserService.Update(accountUserUpdate);

                            this.user.Consume(accountUserUpdate);

                            this.View.ShowUpdateEmailComplete();
                        }
                        else
                        {
                            this.View.ShowErrorEmailTaken();
                        }
                    }
                }
                else
                {
                    this.View.ShowErrorValidation();
                }
            }
            catch (Exception ex)
            {
                this.pipeline.Send<EventArgs<Exception>>(this, new EventArgs<Exception>(ex), (int)MessageType.Error);
                this.Logger.Error(ex);
            }
        }


        public void OnSubmitPassword(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(this.View.GetOldPassword()))
                {
                    if (!String.IsNullOrEmpty(this.View.GetPassword()))
                    {
                        User accountUser =
                            this.accountUserService.GetUser(this.user.Email, this.View.GetOldPassword());

                        if (accountUser != null)
                        {
                            if (this.View.GetPassword() == this.View.GetPasswordConfirm())
                            {
                                this.accountUserService.UpdatePassword(this.user.Id, this.View.GetPassword());

                                this.View.ShowUpdatePasswordComplete();
                            }
                            else
                            {
                                this.View.ShowErrorPasswordValidation();
                            }
                        }
                        else
                        {
                            this.View.ShowErrorOldPasswordNoMatch();
                        }
                    }
                    else
                    {
                        this.View.ShowErrorPasswordValidation();
                    }
                }
                else
                {
                    this.View.ShowErrorOldPasswordNoMatch();
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
