using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Client.View.EventArg;
using AppActs.Client.Model;
using AppActs.Model;
using MongoDB.Bson;

namespace AppActs.Client.View
{
    public interface ISetupAppViewView : IViewLoggedIn
    {
        event EventHandler<EventArgs<Guid>> Selected;
        void Populate(IEnumerable<Application> applications);
        void Set(Guid applicationId, string displayApplicationId);
    }
}
