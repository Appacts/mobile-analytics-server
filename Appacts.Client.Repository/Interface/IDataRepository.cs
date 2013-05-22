using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Client.Model.Enum;
using AppActs.Client.Model;
using AppActs.Model.Enum;
using AppActs.Model.Enum;
using MongoDB.Bson;

namespace AppActs.Client.Repository.Interface
{
    public interface IDataRepository
    {
        List<GraphSeries> GetGraphAxis(string query, Guid applicationId, DateTime dateStart, DateTime dateEnd);
        List<GraphSeries> GetGraphAxis(string query, Guid applicationId, IEnumerable<PlatformType> platformTypes, DateTime dateStart, DateTime dateEnd);
        List<GraphSeries> GetGraphAxis(string query, IEnumerable<Guid> applicationId, DateTime dateStart, DateTime dateEnd);
        List<GraphSeries> GetGraphAxis(string query, Guid applicationId, IEnumerable<string> version, DateTime dateStart, DateTime dateEnd);
        object GetDetail(string query, Guid applicationId, DateTime dateStart, DateTime dateEnd, string detailId);
    }
}
