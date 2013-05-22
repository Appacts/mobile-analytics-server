using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.API.DataMapper.Interface;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using AppActs.Core.Exceptions;
using AppActs.API.Model.Device;
using AppActs.Model.Enum;
using AppActs.API.Model;

namespace AppActs.API.DataMapper
{
    public class DeviceMapper : NoSqlBase, IDeviceMapper
    {
        public DeviceMapper(MongoClient client, string databaseName)
            : base(client, databaseName)
        {

        }

        public DeviceInfo Find(Guid id)
        {
            try
            {
                return this.GetCollection<DeviceInfo>().FindOne(Query<DeviceInfo>.EQ<Guid>(x => x.Guid, id));
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }


        public void Save(DeviceInfo device)
        {
            base.Save(device);
        }

        public void Save(DeviceLocation location)
        {
            base.Save(location);
        }

        public void Save(DeviceSummary entity)
        {
            try
            {
                IMongoQuery queryBase = Query.And
                    (
                        Query<DeviceSummary>.EQ<DateTime>(mem => mem.Date, entity.Date),
                        Query<DeviceSummary>.EQ<Guid>(mem => mem.ApplicationId, entity.ApplicationId),
                        Query<DeviceSummary>.EQ<string>(mem => mem.Version, entity.Version),
                        Query<DeviceSummary>.EQ<PlatformType>(mem => mem.PlatformId, entity.PlatformId)
                    );

                IMongoUpdate update = Update<DeviceSummary>
                    .SetOnInsert(x => x.Version, entity.Version)
                    .SetOnInsert(x => x.Date, entity.Date)
                    .SetOnInsert(x => x.ApplicationId, entity.ApplicationId)
                    .SetOnInsert(x => x.PlatformId, entity.PlatformId)
                    .SetOnInsert(x => x.Locales, new List<Aggregate<string>>())
                    .SetOnInsert(x => x.Carriers, new List<Aggregate<string>>())
                    .SetOnInsert(x => x.OperatingSystems, new List<Aggregate<string>>())
                    .SetOnInsert(x => x.ManufacturerModels, new List<ManufacturerModelAggregate>())
                    .SetOnInsert(x => x.Resolutions, new List<Resolution>())
                    .Inc(x => x.Count, 1);

                this.GetCollection<DeviceSummary>().FindAndModify(queryBase, SortBy.Descending("Date"), update, false, true);
                this.GetCollection<DeviceSummary>().EnsureIndex(IndexKeys.Descending("Date"));

                //locales
                IMongoQuery queryLocaleCheckNotExist = Query.And
                    (
                        queryBase,
                        Query.NE("Locales.Key", BsonValue.Create(entity.Locales.First().Key))
                    );

                IMongoUpdate insertLocale = Update
                    .Push("Locales", BsonValue.Create(entity.Locales.First().CopyOnlyKeys().ToBsonDocument()));


                this.GetCollection<DeviceSummary>().Update(queryLocaleCheckNotExist, insertLocale);


                IMongoQuery queryGetExistingLocale = Query.And
                    (
                        queryBase,
                        Query.EQ("Locales.Key", BsonValue.Create(entity.Locales.First().Key))
                    );

                IMongoUpdate updateLocales = Update
                    .Inc("Locales.$.Count", 1);

                this.GetCollection<DeviceSummary>().Update(queryGetExistingLocale, updateLocales);

                //carriers
                IMongoQuery queryCarrierNotExist = Query.And
                    (
                        queryBase,
                        Query.NE("Carriers.Key", BsonValue.Create(entity.Carriers.First().Key))
                    );

                IMongoUpdate insertCarrier = Update
                    .Push("Carriers", BsonValue.Create(entity.Carriers.First().CopyOnlyKeys().ToBsonDocument()));

                this.GetCollection<DeviceSummary>().Update(queryCarrierNotExist, insertCarrier);


                IMongoQuery queryGetExistingCarrier = Query.And
                    (
                        queryBase,
                        Query.EQ("Carriers.Key", BsonValue.Create(entity.Carriers.First().Key))
                    );

                IMongoUpdate updateCarrier = Update
                    .Inc("Carriers.$.Count", 1);

                this.GetCollection<DeviceSummary>().Update(queryGetExistingCarrier, updateCarrier);


                //manufacturerModels
                IMongoQuery queryManufacturerModelExists = Query.And
                    (
                        queryBase,
                        Query.NE("ManufacturerModels.ManufacturerModel",
                            BsonValue.Create(entity.ManufacturerModels.First().ManufacturerModel))
                    );

                IMongoUpdate insertManufacturerModel = Update
                    .Push("ManufacturerModels", BsonValue.Create(entity.ManufacturerModels.First().CopyOnlyKeys().ToBsonDocument()));

                this.GetCollection<DeviceSummary>().Update(queryManufacturerModelExists, insertManufacturerModel);

                IMongoQuery queryGetExistingManufacturer = Query.And
                    (
                        queryBase,
                        Query.EQ("ManufacturerModels.ManufacturerModel", 
                            BsonValue.Create(entity.ManufacturerModels.First().ManufacturerModel))
                    );

                IMongoUpdate updateManufacturer = Update
                    .Inc("ManufacturerModels.$.Count", 1);

                this.GetCollection<DeviceSummary>().Update(queryGetExistingManufacturer, updateManufacturer);

                //operating systems
                IMongoQuery queryOperatingSystemExists = Query.And
                    (
                        queryBase,
                        Query.NE("OperatingSystems.Key", BsonValue.Create(entity.OperatingSystems.First().Key))
                    );

                IMongoUpdate insertOperatingSystem = Update
                    .Push("OperatingSystems", BsonValue.Create(entity.OperatingSystems.First().CopyOnlyKeys().ToBsonDocument()));

                this.GetCollection<DeviceSummary>().Update(queryOperatingSystemExists, insertOperatingSystem);

                IMongoQuery queryGetExistingOperatingSystems = Query.And
                    (
                        queryBase,
                        Query.EQ("OperatingSystems.Key", BsonValue.Create(entity.OperatingSystems.First().Key))
                    );

                IMongoUpdate updateOperatingSystems = Update
                    .Inc("OperatingSystems.$.Count", 1);

                this.GetCollection<DeviceSummary>().Update(queryGetExistingOperatingSystems, updateOperatingSystems);


                //resolutions
                IMongoQuery queryResolutonsExists = Query.And
                    (
                        queryBase,
                        Query.NE("Resolutions.WxH", BsonValue.Create(entity.Resolutions.First().WidthxHeight))
                    );

                IMongoUpdate insertResolution = Update
                    .Push("Resolutions", BsonValue.Create(entity.Resolutions.First().CopyOnlyKeys().ToBsonDocument()));

                this.GetCollection<DeviceSummary>().Update(queryResolutonsExists, insertResolution);

                IMongoQuery queryGetExistingResolution = Query.And
                    (
                        queryBase,
                        Query.EQ("Resolutions.WxH", BsonValue.Create(entity.Resolutions.First().WidthxHeight))
                    );

                IMongoUpdate updateResolution = Update
                    .Inc("Resolutions.$.Count", 1);

                this.GetCollection<DeviceSummary>().Update(queryGetExistingResolution, updateResolution);
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }

        public void Save(DeviceUpgradeSummary entity)
        {
            try
            {
                IMongoQuery query = Query.And
                    (
                        Query<DeviceUpgradeSummary>.EQ<DateTime>(mem => mem.Date, entity.Date),
                        Query<DeviceUpgradeSummary>.EQ<Guid>(mem => mem.ApplicationId, entity.ApplicationId),
                        Query<DeviceUpgradeSummary>.EQ<string>(mem => mem.Version, entity.Version),
                        Query<DeviceUpgradeSummary>.EQ<PlatformType>(mem => mem.PlatformType, entity.PlatformType)
                    );

                IMongoUpdate update = Update<DeviceUpgradeSummary>
                    .SetOnInsert(x => x.Version, entity.Version)
                    .SetOnInsert(x => x.Date, entity.Date)
                    .SetOnInsert(x => x.ApplicationId, entity.ApplicationId)
                    .SetOnInsert(x => x.PlatformType, entity.PlatformType)
                    .Inc(mem => mem.Count, entity.Count);

                this.GetCollection<DeviceUpgradeSummary>().FindAndModify(query, SortBy.Null, update, false, true);
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }
    }
}
