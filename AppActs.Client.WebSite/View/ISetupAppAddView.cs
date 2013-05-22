using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Model;
using AppActs.Client.Model;
using MongoDB.Bson;
using AppActs.Model.Enum;

namespace AppActs.Client.View
{
    public interface ISetupAppAddView : IViewLoggedIn, IViewIsValid
    {
        event EventHandler Add;

        string GetApplicationName();
        IEnumerable<PlatformType> GetPlatforms();
        void Populate(IEnumerable<Platform> platforms);
        void ShowErrorPlatformNotSelected();
        void Clear();
        void ShowErrorNameTaken();
    }
}
