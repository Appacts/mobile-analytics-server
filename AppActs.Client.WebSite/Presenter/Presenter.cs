using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Mvp.Presenter;
using AppActs.Client.View;
using log4net;
using System.Configuration;
using AppActs.Client.Model;

namespace AppActs.Client.Presenter
{
    public class Presenter<TInterface> : PresenterBase<TInterface>,
        IPresenterBase where TInterface : class, IView
    {
        protected readonly ILog Logger;
        protected readonly Settings Settings;

        public Presenter(ILog Logger, Settings settings)
        {
            this.Logger = Logger;
            this.Settings = settings;
        }

        public override void OnViewInitialized()
        {
            if (!this.View.IsSecureConnection)
            {
                if (this.Settings.SecureConnectionSupported)
                {
                    this.View.RedirectToSecure();
                }
            }

            base.OnViewInitialized();
        }
    }
}
