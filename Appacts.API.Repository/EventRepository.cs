using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.API.Repository.Interface;
using AppActs.Core.Exceptions;
using System.Data.SqlClient;
using MongoDB.Driver;
using AppActs.Repository;
using AppActs.API.Model.Event;
using AppActs.API.DataMapper.Interface;
using MongoDB.Bson;
namespace AppActs.API.Repository
{
    public class EventRepository : IEventRepository
    {
        readonly IEventMapper eventMapper;

        public EventRepository(IEventMapper eventMapper)
        {
            this.eventMapper = eventMapper;
        }

        public void Save(Event entity)
        {
            this.eventMapper.Save(entity);
        }

        public void Save(ScreenSummary summary)
        {
            this.eventMapper.Save(summary);
        }

        public void Save(EventSummary summary)
        {
            this.eventMapper.Save(summary);
        }

        public void Save(AppUsageDurationSummary summary)
        {
            this.eventMapper.Save(summary);
        }

        public void Save(ContentLoadSummary summary)
        {
            this.eventMapper.Save(summary);
        }

        public DeviceAppLastScreen GetDeviceAppLastScreenOneBy(Guid deviceId, Guid applicationId)
        {
            return this.eventMapper.GetDeviceAppLastScreenOneBy(deviceId, applicationId);
        }

        public void Save(DeviceAppLastScreen deviceAppLastScreen)
        {
            this.eventMapper.Save(deviceAppLastScreen);
        }

        public void Remove(Guid deviceId, Guid applicationId)
        {
            this.eventMapper.Remove(deviceId, applicationId);
        }

        public void Save(AppUsageSummary summary)
        {
            this.eventMapper.Save(summary);
        }

        public Nullable<DateTime> GetDateOfDeviceLastVisit(Guid deviceId, Guid applicationId)
        {
            return this.eventMapper.GetDateOfDeviceLastVisit(deviceId, applicationId);
        }

        public void Save(ScreenRouteSummary routeSum)
        {
            this.eventMapper.Save(routeSum);
        }
    }
}
