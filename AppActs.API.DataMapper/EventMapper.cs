using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.API.DataMapper.Interface;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using AppActs.Core.Exceptions;
using AppActs.API.Model.Event;
using AppActs.Model.Enum;

namespace AppActs.API.DataMapper
{
    public class EventMapper : NoSqlBase, IEventMapper
    {
        public EventMapper(MongoClient client, string databaseName)
            : base(client, databaseName)
        {

        }

        public void Save(Event entity)
        {
            base.Save(entity);
        }

        public void Save(ScreenSummary entity)
        {
            try
            {
                IMongoQuery queryBase = Query.And
                    (
                        Query<ScreenSummary>.EQ<DateTime>(mem => mem.Date, entity.Date),
                        Query<ScreenSummary>.EQ<Guid>(mem => mem.ApplicationId, entity.ApplicationId),
                        Query<ScreenSummary>.EQ<string>(mem => mem.Version, entity.Version),
                        Query<ScreenSummary>.EQ<PlatformType>(mem => mem.PlatformId, entity.PlatformId)
                    );

                IMongoUpdate update = Update<ScreenSummary>
                    .SetOnInsert(x => x.Version, entity.Version)
                    .SetOnInsert(x => x.Date, entity.Date)
                    .SetOnInsert(x => x.ApplicationId, entity.ApplicationId)
                    .SetOnInsert(x => x.PlatformId, entity.PlatformId)
                    .AddToSet(x => x.Durations, entity.Durations.First())
                    .Inc(mem => mem.Count, entity.Count);

                this.GetCollection<ScreenSummary>().FindAndModify(queryBase, SortBy.Descending("Date"), update, false, true);
                //TODO: need a more elegant solution going forward
                this.GetCollection<ScreenSummary>().EnsureIndex(IndexKeys.Descending("Date"));

                IMongoQuery queryDuration = Query.And
                    (
                        queryBase,
                        Query.EQ("Durations.Key", BsonValue.Create(entity.Durations.First().Key))
                    );

                IMongoUpdate updateDuration = Update
                    .Inc("Durations.$.Count", 1);

                this.GetCollection<ScreenSummary>().Update(queryDuration, updateDuration);
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }

        public void Save(ScreenRouteSummary entity)
        {
            try
            {
                IMongoQuery queryBase = Query.And
                    (
                        Query<ScreenRouteSummary>.EQ<DateTime>(mem => mem.Date, entity.Date),
                        Query<ScreenRouteSummary>.EQ<Guid>(mem => mem.ApplicationId, entity.ApplicationId),
                        Query<ScreenRouteSummary>.EQ<string>(mem => mem.Version, entity.Version),
                        Query<ScreenRouteSummary>.EQ<PlatformType>(mem => mem.PlatformId, entity.PlatformId)
                    );

                IMongoUpdate update = Update<ScreenRouteSummary>
                    .SetOnInsert(x => x.Version, entity.Version)
                    .SetOnInsert(x => x.Date, entity.Date)
                    .SetOnInsert(x => x.ApplicationId, entity.ApplicationId)
                    .SetOnInsert(x => x.PlatformId, entity.PlatformId)
                    .SetOnInsert(x => x.ScreenRoutes, new List<ScreenRoute>())
                    .Inc(mem => mem.Count, entity.Count);

                this.GetCollection<ScreenRouteSummary>().FindAndModify(queryBase, SortBy.Descending("Date"), update, false, true);
                this.GetCollection<ScreenRouteSummary>().EnsureIndex(IndexKeys.Descending("Date"));

                IMongoQuery queryFeedbackInsert = Query.And
                    (
                        queryBase,
                        Query.NE("ScreenRoutes.AB", BsonValue.Create(entity.ScreenRoutes.First().LastAndCurrentScreen))
                    );

                IMongoUpdate updateFeedback = Update
                    .Push("ScreenRoutes", BsonValue.Create(entity.ScreenRoutes.First().CopyOnlyKeys().ToBsonDocument()));

                this.GetCollection<ScreenRouteSummary>().Update(queryFeedbackInsert, updateFeedback);

                IMongoQuery queryScreenFlows = Query.And
                    (
                        queryBase,
                        Query.EQ("ScreenRoutes.AB", BsonValue.Create(entity.ScreenRoutes.First().LastAndCurrentScreen))
                    );

                IMongoUpdate updateScreenFlows = Update
                    .Inc("ScreenRoutes.$.Count", 1);

                this.GetCollection<ScreenRouteSummary>().Update(queryScreenFlows, updateScreenFlows);
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }

        public void Save(EventSummary entity)
        {
            try
            {
                IMongoQuery queryBase = Query.And
                    (
                        Query<EventSummary>.EQ<DateTime>(mem => mem.Date, entity.Date),
                        Query<EventSummary>.EQ<Guid>(mem => mem.ApplicationId, entity.ApplicationId),
                        Query<EventSummary>.EQ<string>(mem => mem.Version, entity.Version),
                        Query<EventSummary>.EQ<PlatformType>(mem => mem.PlatformId, entity.PlatformId)
                    );

                IMongoUpdate update = Update<EventSummary>
                    .SetOnInsert(x => x.Version, entity.Version)
                    .SetOnInsert(x => x.Date, entity.Date)
                    .SetOnInsert(x => x.ApplicationId, entity.ApplicationId)
                    .SetOnInsert(x => x.PlatformId, entity.PlatformId)
                    .Inc(mem => mem.Count, entity.Count);

                this.GetCollection<EventSummary>().FindAndModify(queryBase, SortBy.Descending("Date"), update, false, true);
                this.GetCollection<EventSummary>().EnsureIndex(IndexKeys.Descending("Date"));

                IMongoQuery queryEventInsert = Query.And
                    (
                        queryBase,
                        Query.NE("ScreenEvents.ScreenAndEvent", 
                            BsonValue.Create(entity.ScreenEvents.First().ScreenAndEvent))
                    );

                IMongoUpdate insertEvent = Update
                    .Push("ScreenEvents", BsonValue.Create(entity.ScreenEvents.First()
                    .CopyOnlyKeys().ToBsonDocument()));

                this.GetCollection<EventSummary>().Update(queryEventInsert, insertEvent);


                IMongoQuery queryGetExistingScreenEvents = Query.And
                    (
                        queryBase,
                        Query.EQ("ScreenEvents.ScreenAndEvent", 
                            BsonValue.Create(String.Concat(entity.ScreenEvents.First().ScreenAndEvent)))
                    );

                IMongoUpdate updateScreenEvents = Update
                    .Inc("ScreenEvents.$.Count", 1);

                this.GetCollection<EventSummary>().Update(queryGetExistingScreenEvents, updateScreenEvents);
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }

        public void Save(AppUsageDurationSummary entity)
        {
            try
            {
                IMongoQuery query = Query.And
                    (
                        Query<AppUsageSummary>.EQ<DateTime>(mem => mem.Date, entity.Date),
                        Query<AppUsageSummary>.EQ<Guid>(mem => mem.ApplicationId, entity.ApplicationId),
                        Query<AppUsageSummary>.EQ<string>(mem => mem.Version, entity.Version),
                        Query<AppUsageSummary>.EQ<PlatformType>(mem => mem.PlatformId, entity.PlatformId)
                    );

                IMongoUpdate update = Update<AppUsageDurationSummary>
                    .SetOnInsert(x => x.Version, entity.Version)
                    .SetOnInsert(x => x.Date, entity.Date)
                    .SetOnInsert(x => x.ApplicationId, entity.ApplicationId)
                    .SetOnInsert(x => x.PlatformId, entity.PlatformId)
                    //len
                    .Inc(mem => mem.LengthGroup._10sec, entity.LengthGroup._10sec)
                    .Inc(mem => mem.LengthGroup._20sec, entity.LengthGroup._20sec)
                    .Inc(mem => mem.LengthGroup._30sec, entity.LengthGroup._30sec)
                    .Inc(mem => mem.LengthGroup._1min, entity.LengthGroup._1min)
                    .Inc(mem => mem.LengthGroup._2min, entity.LengthGroup._2min)
                    .Inc(mem => mem.LengthGroup._4min, entity.LengthGroup._4min)
                    .Inc(mem => mem.LengthGroup._8min, entity.LengthGroup._8min)
                    .Inc(mem => mem.LengthGroup._16min, entity.LengthGroup._16min)
                    .Inc(mem => mem.LengthGroup._32min, entity.LengthGroup._32min)
                    .Inc(mem => mem.LengthGroup._1hr, entity.LengthGroup._1hr)
                    .Inc(mem => mem.LengthGroup.Over1hr, entity.LengthGroup.Over1hr)
                    .Inc(mem => mem.Count, entity.Count);

                this.GetCollection<AppUsageDurationSummary>().FindAndModify(query, SortBy.Descending("Date"), update, false, true);
                this.GetCollection<AppUsageDurationSummary>().EnsureIndex(IndexKeys.Descending("Date"));
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }

        public void Save(AppUsageSummary entity)
        {
            try
            {
                IMongoQuery queryBase = Query.And
                    (
                        Query<AppUsageSummary>.EQ<DateTime>(mem => mem.Date, entity.Date),
                        Query<AppUsageSummary>.EQ<Guid>(mem => mem.ApplicationId, entity.ApplicationId),
                        Query<AppUsageSummary>.EQ<string>(mem => mem.Version, entity.Version),
                        Query<AppUsageSummary>.EQ<PlatformType>(mem => mem.PlatformId, entity.PlatformId)
                    );

                IMongoUpdate updateExistCheck = Update<AppUsageSummary>
                    .SetOnInsert(x => x.Version, entity.Version)
                    .SetOnInsert(x => x.Date, entity.Date)
                    .SetOnInsert(x => x.ApplicationId, entity.ApplicationId)
                    .SetOnInsert(x => x.PlatformId, entity.PlatformId)
                    .SetOnInsert(x => x.DevicesVisits, new List<DeviceAppVisit>())
                    .Inc(x => x.TimeGroup._0, entity.TimeGroup._0)
                    .Inc(x => x.TimeGroup._1, entity.TimeGroup._1)
                    .Inc(x => x.TimeGroup._2, entity.TimeGroup._2)
                    .Inc(x => x.TimeGroup._3, entity.TimeGroup._3)
                    .Inc(x => x.TimeGroup._4, entity.TimeGroup._4)
                    .Inc(x => x.TimeGroup._5, entity.TimeGroup._5)
                    .Inc(x => x.TimeGroup._6, entity.TimeGroup._6)
                    .Inc(x => x.TimeGroup._7, entity.TimeGroup._7)
                    .Inc(x => x.TimeGroup._8, entity.TimeGroup._8)
                    .Inc(x => x.TimeGroup._9, entity.TimeGroup._9)
                    .Inc(x => x.TimeGroup._10, entity.TimeGroup._10)
                    .Inc(x => x.TimeGroup._11, entity.TimeGroup._11)
                    .Inc(x => x.TimeGroup._12, entity.TimeGroup._12)
                    .Inc(x => x.TimeGroup._13, entity.TimeGroup._13)
                    .Inc(x => x.TimeGroup._14, entity.TimeGroup._14)
                    .Inc(x => x.TimeGroup._15, entity.TimeGroup._15)
                    .Inc(x => x.TimeGroup._16, entity.TimeGroup._16)
                    .Inc(x => x.TimeGroup._17, entity.TimeGroup._17)
                    .Inc(x => x.TimeGroup._18, entity.TimeGroup._18)
                    .Inc(x => x.TimeGroup._19, entity.TimeGroup._19)
                    .Inc(x => x.TimeGroup._20, entity.TimeGroup._20)
                    .Inc(x => x.TimeGroup._21, entity.TimeGroup._21)
                    .Inc(x => x.TimeGroup._22, entity.TimeGroup._22)
                    .Inc(x => x.TimeGroup._23, entity.TimeGroup._23)
                    .Inc(x => x.WeekGroup.Mon, entity.WeekGroup.Mon)
                    .Inc(x => x.WeekGroup.Tue, entity.WeekGroup.Tue)
                    .Inc(x => x.WeekGroup.Wed, entity.WeekGroup.Wed)
                    .Inc(x => x.WeekGroup.Thu, entity.WeekGroup.Thu)
                    .Inc(x => x.WeekGroup.Fri, entity.WeekGroup.Fri)
                    .Inc(x => x.WeekGroup.Sat, entity.WeekGroup.Sat)
                    .Inc(x => x.WeekGroup.Sun, entity.WeekGroup.Sun)
                    .Inc(x => x.FrequencyUsageGroup._24hrs, entity.FrequencyUsageGroup._24hrs)
                    .Inc(x => x.FrequencyUsageGroup._1day, entity.FrequencyUsageGroup._1day)
                    .Inc(x => x.FrequencyUsageGroup._2days, entity.FrequencyUsageGroup._2days)
                    .Inc(x => x.FrequencyUsageGroup._3days, entity.FrequencyUsageGroup._3days)
                    .Inc(x => x.FrequencyUsageGroup._4days, entity.FrequencyUsageGroup._4days)
                    .Inc(x => x.FrequencyUsageGroup._5days, entity.FrequencyUsageGroup._5days)
                    .Inc(x => x.FrequencyUsageGroup._6days, entity.FrequencyUsageGroup._6days)
                    .Inc(x => x.FrequencyUsageGroup._1wk, entity.FrequencyUsageGroup._1wk)
                    .Inc(x => x.FrequencyUsageGroup._2wk, entity.FrequencyUsageGroup._2wk)
                    .Inc(x => x.FrequencyUsageGroup._3wk, entity.FrequencyUsageGroup._3wk)
                    .Inc(x => x.FrequencyUsageGroup._1mt, entity.FrequencyUsageGroup._1mt)
                    .Inc(x => x.FrequencyUsageGroup.Over1Mt, entity.FrequencyUsageGroup.Over1Mt)
                    .Inc(x => x.NewReturningGroup.New,  entity.NewReturningGroup.New)
                    .Inc(x => x.NewReturningGroup.Returning, entity.NewReturningGroup.Returning)
                    .Inc(x => x.Count, 1);

                this.GetCollection<AppUsageSummary>().FindAndModify(queryBase,
                    SortBy.Descending("Date", "DevicesVisits.DeviceId"), updateExistCheck, false, true);
                this.GetCollection<AppUsageDurationSummary>().EnsureIndex(IndexKeys.Descending("Date", "DevicesVisits.DeviceId"));

                IMongoQuery queryGetExistingValues = Query.And
                    (
                        queryBase,
                        Query.NE("DevicesVisits.DeviceId", BsonValue.Create(entity.DevicesVisits.First().DeviceId))
                    );

                IMongoUpdate insertDeviceVisits = Update
                    .Push("DevicesVisits", BsonValue.Create(entity.DevicesVisits.First().CopyOnlyKey().ToBsonDocument()));

                this.GetCollection<AppUsageSummary>().Update(queryGetExistingValues, insertDeviceVisits);

                IMongoQuery queryDeviceVisits = Query.And
                    (
                        queryBase,
                        Query.EQ("DevicesVisits.DeviceId", BsonValue.Create(entity.DevicesVisits.First().DeviceId))
                    );

                IMongoUpdate updateDeviceVisits = Update
                    .Inc("DevicesVisits.$.Count", 1);

                this.GetCollection<AppUsageSummary>().Update(queryDeviceVisits, updateDeviceVisits);
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }

        public void Save(ContentLoadSummary entity)
        {
            try
            {
                IMongoQuery queryBase = Query.And
                    (
                        Query<ContentLoadSummary>.EQ<DateTime>(mem => mem.Date, entity.Date),
                        Query<ContentLoadSummary>.EQ<Guid>(mem => mem.ApplicationId, entity.ApplicationId),
                        Query<ContentLoadSummary>.EQ<string>(mem => mem.Version, entity.Version),
                        Query<ContentLoadSummary>.EQ<PlatformType>(mem => mem.PlatformId, entity.PlatformId)
                    );

                IMongoUpdate update = Update<ContentLoadSummary>
                    .SetOnInsert(x => x.Version, entity.Version)
                    .SetOnInsert(x => x.Date, entity.Date)
                    .SetOnInsert(x => x.ApplicationId, entity.ApplicationId)
                    .SetOnInsert(x => x.PlatformId, entity.PlatformId)
                    .SetOnInsert(x => x.Loads, new List<ContentDurationAggregate>())
                    .Inc(mem => mem.Count, entity.Count);

                this.GetCollection<ContentLoadSummary>().FindAndModify(queryBase, SortBy.Descending("Date"), update, false, true);
                this.GetCollection<ContentLoadSummary>().EnsureIndex(IndexKeys.Descending("Date"));

                IMongoQuery queryLoadInsert = Query.And
                    (
                        queryBase,
                        Query.NE("Loads.ScreenContent", BsonValue.Create(entity.Loads.First().ScreenContent))
                    );

                IMongoUpdate insertLoad = Update
                    .Push("Loads", BsonValue.Create(entity.Loads.First().CopyOnlyKey().ToBsonDocument()));

                this.GetCollection<ContentLoadSummary>().Update(queryLoadInsert, insertLoad);


                IMongoQuery queryLoadUpdate = Query.And
                    (
                        queryBase,
                        Query.EQ("Loads.ScreenContent", BsonValue.Create(entity.Loads.First().ScreenContent))
                    );

                IMongoUpdate updateContentLoad = Update
                    .Inc("Loads.$.Seconds", entity.Loads.First().Seconds)
                    .Inc("Loads.$.Count", 1);

                this.GetCollection<ContentLoadSummary>().Update(queryLoadUpdate, updateContentLoad);
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }

        public void Remove(Guid deviceId, Guid applicationId)
        {
            try
            {
               IMongoQuery query = Query.And
                   (
                        Query<DeviceAppLastScreen>.EQ<Guid>(mem => mem.DeviceId, deviceId),
                        Query<DeviceAppLastScreen>.EQ<Guid>(mem => mem.ApplicationId, applicationId)
                   );

                this.GetCollection<DeviceAppLastScreen>().Remove(query);
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }

        public void Save(DeviceAppLastScreen deviceAppLastScreen)
        {
            base.Save(deviceAppLastScreen);
        }

        public DeviceAppLastScreen GetDeviceAppLastScreenOneBy(Guid deviceId, Guid applicationId)
        {
            try
            {
                IMongoQuery query = Query.And
                    (
                        Query<DeviceAppLastScreen>.EQ<Guid>(mem => mem.DeviceId, deviceId),
                        Query<DeviceAppLastScreen>.EQ<Guid>(mem => mem.ApplicationId, applicationId)
                    );

                return this.GetCollection<DeviceAppLastScreen>().FindOne(query);
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }

        public Nullable<DateTime> GetDateOfDeviceLastVisit(Guid deviceId, Guid applicationId)
        {
            try
            {
                IMongoQuery queryBase = Query.And
                    (
                        Query<AppUsageSummary>.EQ<Guid>(mem => mem.ApplicationId, applicationId),
                        Query.EQ("DevicesVisits.DeviceId", BsonValue.Create(deviceId))
                    );

                MongoCursor<AppUsageSummary> appUsageSummaries = 
                    this.GetCollection<AppUsageSummary>().Find(queryBase);

                appUsageSummaries.SetSortOrder(SortBy<AppUsageSummary>.Descending(x => x.Date));
                AppUsageSummary appUsageSummary = appUsageSummaries.FirstOrDefault();

                if (appUsageSummary != null)
                {
                    return appUsageSummary.Date;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }
    }
}
