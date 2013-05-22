using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Model;
using AppActs.Client.Model;
using MongoDB.Bson;

namespace AppActs.Client.Service.Interface
{
    public interface IApplicationService
    {
        bool IsApplicationNameAvailable(string applicationName);
        void Save(Application application);
        void Update(Application application);
        Application Get(Guid applicationId);
        IEnumerable<string> GetVersions(Guid applicationId);
        IEnumerable<Platform> GetPlatforms();
        void Save(IEnumerable<Platform> platforms);
        IEnumerable<Application> GetAll();
    }
}
