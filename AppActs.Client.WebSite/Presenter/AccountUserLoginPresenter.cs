using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Client.View;
using log4net;
using AppActs.Client.Service.Interface;
using AppActs.Core.Exceptions;
using AppActs.Model;
using AppActs.Client.Model;
using AppActs.Client.Model.Enum;
using Mosaic.Mvp.Pipeline.Interface;
using AppActs.Client.View.EventArg;
using AppActs.Client.Presenter.Enum;
using System.Web.Script.Serialization;

namespace AppActs.Client.Presenter
{
    public class AccountUserLoginPresenter : Presenter<IAccountUserLoginView>
    {
        private readonly IUserService userService;
        private readonly User accountUser;
        private readonly IApplicationService applicationService;
        private readonly IPipeline pipeline; 


        public AccountUserLoginPresenter(IAccountUserLoginView view, 
            IUserService userService,
            IApplicationService applicationService, ILog log, User user, 
            AppActs.Client.Model.Settings settings, IPipeline pipeline) 
            : base(log, settings)
        {
            this.View = view;
            this.userService = userService;
            this.applicationService = applicationService;
            this.pipeline = pipeline;

            this.accountUser = user;
        }

        public override void Init()
        {
            this.View.Submit += this.OnSubmit;

            base.Init();
        }


        public override void OnViewInitialized()
        {
            if (!this.accountUser.IsEmpty())
            {
                this.View.RedirectToFeed();
            }

            base.OnViewInitialized();
        }


        public void OnSubmit(object sender, EventArgs e)
        {
            try
            {
                if (this.View.IsValid())
                {
                    if (this.userService.GetAll().Count() == 0)
                    {
                        User defaultUser = new JavaScriptSerializer().Deserialize<User>(AppActs.Client.WebSite.Properties.Settings.Default.StartupSystemUser);
                        defaultUser.DateCreated = DateTime.Now;
                        defaultUser.DateModified = DateTime.Now;
                        this.userService.Save(defaultUser);
                    }

                    User accountUser = this.userService
                        .GetUser(this.View.GetEmail(), this.View.GetPassword());

                    if (accountUser != null)
                    {
                        this.accountUser.Consume(accountUser);


                        if (this.applicationService.GetAll().Count() > 0)
                        {
                            this.View.RedirectToFeed();
                        }
                        else
                        {
                            this.View.RedirectToApps();
                        }
                    }
                    else
                    {
                        this.View.ShowErrorInvalidLogin();
                    }
                }
                else
                {
                    this.View.ShowErrorInvalidCredentials();
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
