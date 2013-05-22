using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using log4net;

using AppActs.Client.View;

namespace AppActs.Client.Presenter
{
    public class ErrorPresenter : Presenter<IErrorView>
    {
        public ErrorPresenter(IErrorView view, ILog log, AppActs.Client.Model.Settings settings) 
            :base(log, settings)
        {
            this.View = view;
        }

        public override void OnViewInitialized()
        {
            Exception ex = this.View.GetException();

            if (ex != null)
            {
                this.Logger.Error("Unhandled Exception", ex);
            }

            base.OnViewInitialized();
        }
    }
}
