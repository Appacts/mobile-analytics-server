using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.API.Model.Event;
using MongoDB.Bson;

namespace AppActs.API.Repository.Interface
{
    public interface IEventRepository
    {
        void Save(Event entity);
        void Save(ScreenSummary summary);
        void Save(EventSummary summary);
        void Save(AppUsageDurationSummary summary);
        void Save(ContentLoadSummary summary);
        DeviceAppLastScreen GetDeviceAppLastScreenOneBy(Guid deviceId, Guid applicationId);
        void Save(DeviceAppLastScreen deviceAppLastScreen);
        void Remove(Guid deviceId, Guid applicationId);
        void Save(AppUsageSummary summary);
        Nullable<DateTime> GetDateOfDeviceLastVisit(Guid deviceId, Guid applicationId);
        void Save(ScreenRouteSummary routeSum);
    }
}
