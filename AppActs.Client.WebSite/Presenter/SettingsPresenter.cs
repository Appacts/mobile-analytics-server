using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Client.View;
using AppActs.Model;
using log4net;
using Mosaic.Mvp.Pipeline.Interface;
using AppActs.Client.View.EventArg;
using AppActs.Client.Presenter.Enum;
using AppActs.Client.Model.Enum;
using AppActs.Client.Model;

namespace AppActs.Client.Presenter
{
    public class SettingsPresenter :
        LoggedInPresenter<ISettingsView>,
        IReceiver<EventArgs<User>>,
        IReceiver<EventArgs<ScreenType>>
    {
        private readonly IPipeline pipeline;

        public SettingsPresenter(ISettingsView view, IPipeline pipeline, 
            User accountUser, ILog log, AppActs.Client.Model.Settings settings)
            : base(accountUser, log, settings)
        {
            this.View = view;
            this.pipeline = pipeline;
        }

        public override void Init()
        {
            this.pipeline.Register(this, (int)MessageType.AccountUser);
            this.pipeline.Register(this, (int)MessageType.Screen);

            base.Init();
        }

        public override void OnViewInitialized()
        {
            this.load(this.user);
            base.OnViewInitialized();
        }

        public void Receive(object sender, EventArgs<User> e, int messageId)
        {
            this.load(e.ValueOne);
        }

        public void Receive(object sender, EventArgs<ScreenType> e, int messageId)
        {
            this.View.Set(e.ValueOne);
        }

        public void Dispose()
        {
            this.pipeline.Remove(this, (int)MessageType.AccountUser);
            this.pipeline.Remove(this, (int)MessageType.Screen);
        }

        private void load(User accountUser)
        {
            this.View.Set(accountUser.Name);
        }
    }
}
