using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Mvp.Presenter;
using AppActs.Client.View;
using Mosaic.Mvp.Pipeline.Interface;
using AppActs.Client.View.EventArg;
using log4net;
using AppActs.Client.Model;
using AppActs.Client.Presenter.Enum;

namespace AppActs.Client.Presenter
{
    public class DefaultPresenter : Presenter<IDefaultView>, IReceiver<EventArgs<Exception>>
    {
        private readonly IPipeline pipeline;

        public DefaultPresenter(IDefaultView view, IPipeline pipeline, ILog log, Settings settings)
            : base(log, settings)
        {
            this.View = view;
            this.pipeline = pipeline;
        }

        public override void Init()
        {
            this.pipeline.Register(this, (int)MessageType.Error);
            base.Init();
        }

        public void Receive(object sender, EventArgs<Exception> e, int messageId)
        {
            this.View.ShowErrorMessage();
        }

        public void Dispose()
        {
            this.pipeline.Remove(this, (int)MessageType.Error);
        }
    }
}
