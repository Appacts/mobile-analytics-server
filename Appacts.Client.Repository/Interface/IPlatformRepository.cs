using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Model;
using AppActs.Client.Model;
using MongoDB.Bson;
using AppActs.Model.Enum;

namespace AppActs.Client.Repository.Interface
{
    public interface IPlatformRepository
    {
        IEnumerable<Platform> FindAll();
        Platform Find(PlatformType id);
        void Save(IEnumerable<Platform> platforms);
    }
}
