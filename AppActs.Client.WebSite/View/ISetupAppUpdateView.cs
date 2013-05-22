using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Client.View.EventArg;
using AppActs.Client.Model;
using AppActs.Model;
using MongoDB.Bson;
using AppActs.Model.Enum;

namespace AppActs.Client.View
{
    public interface ISetupAppUpdateView : IViewLoggedIn, IViewIsValid
    {
        event EventHandler Update;
        event EventHandler Remove;
        event EventHandler<EventArgs<Guid>> Selected;

        void ShowErrorPlatformNotSelected();
        void ShowErrorNameTaken();
        IEnumerable<PlatformType> GetPlatforms();
        void Populate(IEnumerable<AppActs.Model.Platform> platforms);
        void Set(IEnumerable<Platform> list);
        void SetApplicationName(string name);
        void SetApplicationId(Guid id);
        string GetApplicationName();
        Guid GetApplicationId();
        void ShowSuccessRemove(string p);
        void Clear();
        void Populate(IEnumerable<Application> applications);
    }
}
