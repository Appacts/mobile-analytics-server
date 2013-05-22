using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace AppActs.API.Model.Event
{
    [BsonIgnoreExtraElements]
    public class AppUsageSummary : Summary
    {
        public TimeOfDayGroup TimeGroup { get; set; }
        public DayOfWeekGroup WeekGroup { get; set; }
        public FrequencyOfUsageGroup FrequencyUsageGroup { get; set; }
        public List<DeviceAppVisit> DevicesVisits { get; set; }
        public NewReturningGroup NewReturningGroup { get; set; }

        public AppUsageSummary()
            : base()
        {

        }

        public AppUsageSummary(Event eventItem, Nullable<DateTime> deviceLastVisit)
            : base(eventItem)
        {
            this.TimeGroup = new TimeOfDayGroup(eventItem.DateCreatedOnDevice.Hour);
            this.WeekGroup = new DayOfWeekGroup(eventItem.DateCreatedOnDevice.DayOfWeek);
            this.FrequencyUsageGroup = new FrequencyOfUsageGroup(deviceLastVisit);
            this.DevicesVisits = new List<DeviceAppVisit>();
            this.DevicesVisits.Add(new DeviceAppVisit(eventItem.DeviceId));
            this.NewReturningGroup = new NewReturningGroup(1, 0);

            if (deviceLastVisit.HasValue)
            {
                this.NewReturningGroup = new NewReturningGroup(0, 1);
            }
        }
    }
}
