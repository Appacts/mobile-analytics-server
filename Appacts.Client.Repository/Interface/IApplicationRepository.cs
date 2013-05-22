using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Model;
using MongoDB.Bson;

namespace AppActs.Client.Repository.Interface
{
    public interface IApplicationRepository : AppActs.Repository.Interface.IApplicationRepository
    {
        Application Find(string applicationName);
        void Save(Application application);
        void Update(Application application);
        IEnumerable<Application> FindAll();
    }
}
