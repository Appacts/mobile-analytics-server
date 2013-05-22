using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.API.Model.Event;
using MongoDB.Bson;

namespace AppActs.API.DataMapper.Interface
{
    public interface IEventMapper
    {
        void Save(Event entity);
        void Save(AppUsageSummary entity);
        void Save(EventSummary summary);
        void Save(AppUsageDurationSummary summary);
        void Save(ContentLoadSummary summary);
        void Save(DeviceAppLastScreen deviceAppLastScreen);
        DeviceAppLastScreen GetDeviceAppLastScreenOneBy(Guid deviceId, Guid applicationId);
        void Remove(Guid deviceId, Guid applicationId);
        void Save(ScreenSummary screenUsageSummary);
        Nullable<DateTime> GetDateOfDeviceLastVisit(Guid deviceId, Guid applicationId);
        void Save(ScreenRouteSummary routeSum);
    }
}
