using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Client.Model.Enum;
using MongoDB.Bson;

namespace AppActs.Client.Service.Interface
{
    public interface ITileService
    {
        object GetTile(Guid tileGuid, Guid applicationGuid, DateTime startDate, 
            DateTime endDate, DateTime? startDateCompare, DateTime? endDateCompare);
    }
}
