using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Client.View;
using AppActs.Client.Service.Interface;
using AppActs.Model;
using log4net;
using AppActs.Core.Exceptions;
using AppActs.Client.Model.Enum;
using AppActs.Client.Model;
using Mosaic.Mvp.Pipeline.Interface;
using AppActs.Client.Presenter.Enum;
using AppActs.Client.View.EventArg;

namespace AppActs.Client.Presenter
{
    public class AccountUserManagementPresenter : LoggedInPresenter<IAccountUserManagementView>
    {
        private readonly IUserService accountUserService;
        private readonly IEmailService emailService;
        private readonly IPipeline pipeline;

        public AccountUserManagementPresenter(IPipeline iPipeline, IAccountUserManagementView view, 
            IUserService accountUserService, IEmailService emailService,
            User user, ILog log, AppActs.Client.Model.Settings settings)
            : base(user, log, settings)
        {
            this.View = view;
            this.accountUserService = accountUserService;
            this.emailService = emailService;
            this.pipeline = iPipeline;
        }

        public override void Init()
        {
            this.View.Remove += this.OnRemove;
            this.View.Add += this.OnAdd;

            base.Init();
        }

        public override void OnViewInitialized()
        {
            try
            {
                this.View.Populate(this.accountUserService.GetAll());
            }
            catch (Exception ex)
            {
                this.pipeline.Send<EventArgs<Exception>>(this, new EventArgs<Exception>(ex), (int)MessageType.Error);
                this.Logger.Error(ex);
            }

            base.OnViewInitialized();
        }


        public override void OnViewLoaded()
        {
            this.pipeline.Send<EventArgs<ScreenType>>(this, new EventArgs<ScreenType>(ScreenType.Settings), 
                (int)MessageType.Screen);

            base.OnViewLoaded();
        }


        public void OnRemove(object sender, EventArgs<Guid> eventArgsWithIdentifier)
        {
            try
            {
                //TODO: add logic, can't remove default user
                Guid guid = eventArgsWithIdentifier.ValueOne;

                User accountUser = accountUserService.GetUser(guid);
                accountUser.Active = false;

                this.accountUserService.Update(accountUser);

                if (accountUser.Id != this.user.Id)
                {
                    this.View.Populate(accountUserService.GetAll());

                    this.View.ShowSuccessfullyRemoved(accountUser.Email);
                }
                else
                {
                    this.user.Clear();
                    this.View.RedirectToLogin();
                }
            }
            catch (Exception ex)
            {
                this.pipeline.Send<EventArgs<Exception>>(this, new EventArgs<Exception>(ex), (int)MessageType.Error);
                this.Logger.Error(ex);
            }
        }

        public void OnAdd(object sender, EventArgs e)
        {
            try
            {
                if (this.View.IsValid())
                {
                    if (this.accountUserService.IsEmailAvailable(this.View.GetEmail()))
                    {
                        User accountUser = new User(this.View.GetName(), this.View.GetEmail(),
                            this.accountUserService.GeneratePassword());

                        try
                        {
                            this.emailService.SendUserAdded(this.user.Name, accountUser.Name,
                                accountUser.Email, accountUser.Password);

                            //if email sent just fine then save the user to db and update the view
                            this.accountUserService.Save(accountUser);

                            this.View.Populate(accountUserService.GetAll());

                            this.View.Clear();
                        }
                        catch (Exception ex)
                        {
                            this.pipeline.Send<EventArgs<Exception>>(this, new EventArgs<Exception>(ex), (int)MessageType.Error);
                            this.Logger.Error(ex);
                        }

                        this.View.ShowSuccessfullyAdded();
                    }
                    else
                    {
                        this.View.ShowErrorEmailTaken();
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

    }
}
