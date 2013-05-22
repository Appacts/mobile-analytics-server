using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Model;
using MongoDB.Bson;

namespace AppActs.Repository.Interface
{
    public interface IApplicationRepository
    {
        Application Find(Guid id);
        IEnumerable<string> GetVersionsByApplication(Guid id);
    }
}
