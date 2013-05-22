using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Mvp.Presenter;
using AppActs.Client.View;
using log4net;
using AppActs.Client.Model;

namespace AppActs.Client.Presenter
{
    public abstract class LoggedInPresenter<TInterface> : Presenter<TInterface>,
        IPresenterBase where TInterface : class, IViewLoggedIn
    {
        protected readonly User user;

        public LoggedInPresenter(User user, ILog log, AppActs.Client.Model.Settings settings)
            : base(log, settings)
        {
            this.user = user;
        }

        public override void Init()
        {
            base.Init();

            if (user.IsEmpty())
            {
                this.View.RedirectToLogin();
            }
        }
    }
}
