using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using AppActs.Model.Enum;
using MongoDB.Driver;
using AppActs.API.Repository.Interface;
using AppActs.API.Repository;
using AppActs.API.Service;
using MongoDB.Bson;
using AppActs.API.Model.Feedback;
using AppActs.API.Model.User;
using AppActs.API.DataMapper;

namespace AppActs.API.DataUploader
{
    class Program
    {
        //reference data
        static string[] Carriers = new string[] { "O2 UK", "Orange", "T-Mobile UK", "3 UK", "VODAFONE UK" };
        static PlatformType[] Platform = new PlatformType[] { PlatformType.Android, PlatformType.Blackberry, PlatformType.iOS, PlatformType.WP7 };
        static int[] TimeZoneOffset = new int[] { 0, -2, 5, 2, 1, -10, 10, 2, 3, 1, 0 };
        static FeedbackRatingType[] Ratings = new FeedbackRatingType[] { FeedbackRatingType.One, FeedbackRatingType.Two, FeedbackRatingType.Three, FeedbackRatingType.Four, FeedbackRatingType.Five };
        static SexType[] sex = new SexType[] { SexType.Male, SexType.Female };
        static int[] age = new int[] { 21, 25, 28, 30, 32, 35, 37, 40, 43, 45, 49, 55, 60, 70, 80 };
        static string[] feedbackMessage = new string[] { "love it, keep up good work", "not so great, this screen dosen't make sense", "it's alright, could be better"};
        static string[] platformOS = new string[] { "2.1.2", "4.3.2", "4.1.1", "2.2.2", "3.2.2" };
        static string[] deviceModel = new string[] { "9700", "iPhone 3GS", "iPhone 4S", "iPhone 5", "Nexus 4", "HTC Wildfire", "9520", "9790", "HTC Sensation 4G", "Transformer Prime TF201" };
        static string[] appVersion = new string[] { "1.1", "1.2", "1.3", "1.4", "1.5", "1.6", "1.7" };
        static string[] locale = new string[] { "en-GB", "en-US", "RU", "LV", "FR" };
        static Tuple<int, int>[] resolution = new Tuple<int, int>[] { new Tuple<int, int>(1200, 900), new Tuple<int, int>(1500, 1000), new Tuple<int, int>(2400, 2000) };
        static string[] manufact = new string[] { "Samsung", "Apple", "HTC", "RIM", "Motorola" };

        static Dictionary<string, int> appVersionMap = new Dictionary<string, int>()
        {

            { "1.1", 11 },
            { "1.2", 12 },
            { "1.3", 13 },
            { "1.4", 14 },
            { "1.5", 15 },
            { "1.6", 16 },
            { "1.7", 17 }
        };

        static int[] timeDifference = new int[] { -1, -5, -3, 5, 0, 2, 3, 7, 9, 10, -10, 2, 1, 3, 3, 5 };
        static long[] appTimeUsed = new long[] { 30000, 50000, 1000000, 500000 };
        static int[] appScreenLoaded = new int[] { 1000, 2000, 3000, 4000 };
        static long[] appScreenTimeUsed = new long[] { 30000, 5000, 100000, 50000 };
        static int[] contentLoading = new int[] { 1000, 4000, 2300, 400, 6000 };
        static string[][] location = new string[][] {  new string[]{"United Kingdom", "GB"} };
        static string[][] locationPrecise = new string[][] { new string[] { "England", "England" } , new string[] { "Scotland", "Scotland" }, new string[] { "Wales", "Wales" }, new string[] { "Northern Ireland", "Northern Ireland" } };
        static Guid appId = new Guid("36602527-142b-440f-bf3f-856f6e59d1a6");

        static List<Action<Device, DateTime, string>> journeys = new List<Action<Device, DateTime, string>>()
        {
            route_normal_journey,
            route_normal_journey_feedback,
            route_settings_normal_journey_scroll,
            route_settings_search_cancel_crash,
            route_settings_search_error
        };

        static int numberOfUsesMax = 100;
        static int numberOfUsesMin = 50;

        static double numberOfLoyalUsersMax = 0.40;
        static double numberOfLoyalusersMin = 0.10;

        static double numberOfUsersStartToBeLoyalMax = 0.30;
        static double numberOfUsersStartToBeLOyalMin = 0.04;

        static int upgradeChance = 10;

        static AppActs.API.Service.Interface.IDeviceService deviceService;

        static void Main(string[] args)
        {
            DateTime baseTime = DateTime.UtcNow.AddDays(-1);
     
            deviceService = setup();

            Random random = new Random();

            try
            {
                int numberOfRuns = random.Next(numberOfUsesMin, numberOfUsesMax);
                Console.WriteLine("Number of uploads: {0}", numberOfRuns);

                int numberOfLoyalusers = random.Next((int)(numberOfLoyalusersMin * 100), (int)(numberOfLoyalUsersMax * 100));
                Console.WriteLine("Number of loyal users: {0}", numberOfLoyalusers);

                string filePath = String.Format("{0}\\LoyalDevices.xml", Environment.CurrentDirectory);
                FileInfo fileInfo = new FileInfo(filePath);


                List<Device> deviceLoyalUsedThisTime = new List<Device>();
                List<Device> devicesLoyalTotal = new List<Device>();

                if (fileInfo.Exists)
                {
                    devicesLoyalTotal =
                        AppActs.Core.Xml.Serialization.Deserialize<List<Device>>(filePath, Encoding.UTF8);

                    int numOfLoyalUsers = (int)(devicesLoyalTotal.Count * ((decimal)numberOfLoyalusers / 100));

                    List<int> indexUsed = new List<int>();
                    int numberOfDevices = devicesLoyalTotal.Count();

                    for (int i = 0; i < numOfLoyalUsers; i++)
                    {
                        int index = -1;
                        do
                        {
                            index = random.Next(0, numberOfDevices);
                        } while (indexUsed.Contains(index));

                        deviceLoyalUsedThisTime.Add(devicesLoyalTotal[index]);
                        indexUsed.Add(index);
                    }

                    Console.WriteLine("Number of loyal users in system: {0}", devicesLoyalTotal.Count);
                }


                List<Device> newUsers = new List<Device>();

                int newUsersCount = numberOfRuns - deviceLoyalUsedThisTime.Count;
                Console.WriteLine("Number of new users", newUsersCount);
                for (int i = 0; i < newUsersCount; i++)
                {
                    Device device = new Device(deviceService, getValue<string>(appVersion), 
                        DateTime.SpecifyKind(baseTime.AddHours(getValue<int>(timeDifference)), DateTimeKind.Utc), appId);
                    device.Register(
                            getValue<string>(deviceModel), 
                            getValue<PlatformType>(Platform), 
                            getValue<string>(Carriers), 
                            getValue<string>(platformOS), 
                            getValue<int>(TimeZoneOffset),
                            getValue<string>(locale),
                            getValue<string>(manufact),
                            getValue<Tuple<int, int>>(resolution));

                    string[] loc = getValue<string>(location);
                    string[] locPrecise = getValue<string>(locationPrecise);

                    device.Location(loc[0], loc[1], locPrecise[0], locPrecise[1]);

                    device.User(getValue<SexType>(sex), getValue<int>(age));

                    newUsers.Add(device);
                }

                string lastValue = appVersionMap.OrderBy(x => x.Value).Last().Key;

                //invoke old users, upgrade them
                Console.WriteLine("Processing old users: {0}", deviceLoyalUsedThisTime.Count);
                for (int i = 0; i < deviceLoyalUsedThisTime.Count; i++)
                {
                    Device device = deviceLoyalUsedThisTime[i];

                    //don't upgrade the last version, there is nothing to upgrade to
                    if (lastValue != device.Version)
                    {
                        //higher the upgrade chance is more likely device will upgrade
                        if (random.Next(0, upgradeChance) == 1)
                        {
                            int appVersionInt = appVersionMap[device.Version];
                            string nextVersion = appVersionMap.OrderBy(x => x.Value > appVersionInt).First().Key;
                            device.Version = nextVersion;
                            route_normal_journey_upgrade(device, DateTime.SpecifyKind(baseTime.AddHours(getValue<int>(timeDifference)), DateTimeKind.Utc), device.Version);
                        }
                    }

                    random_journey(device, baseTime.AddHours(getValue<int>(timeDifference)), device.Version);
                }

                //process new users
                Console.WriteLine("Processing new users: {0}", newUsers.Count);
                for (int i = 0; i < newUsers.Count; i++)
                {
                    random_journey(newUsers[i],  DateTime.SpecifyKind(newUsers[i].DateCreated, DateTimeKind.Utc), newUsers[i].Version);
                }

                //save new users
                int numberOfUsersToBecomeLoyal = newUsers.Count * random.Next((int)(numberOfUsersStartToBeLOyalMin * 100), (int)(numberOfUsersStartToBeLoyalMax * 100)) / 100;
                Console.WriteLine("Saving new users: {0}", numberOfUsersToBecomeLoyal);
                for (int i = 0; i < numberOfUsersToBecomeLoyal; i++)
                {
                    devicesLoyalTotal.Add(newUsers[new Random().Next(0, newUsers.Count - 1)]);
                }

                AppActs.Core.Xml.Serialization.Serialize<List<Device>>(devicesLoyalTotal, filePath, Encoding.UTF8);
            }
            catch (Exception ex)
            {

            }
        }

        static void random_journey(Device device, DateTime timeWhenUsed, string version)
        {
            Action<Device, DateTime, string> journey = journeys[new Random().Next(0, journeys.Count)];
            journey(device, timeWhenUsed, version);
        }

        static void route_normal_journey(Device device, DateTime timeWhenUsed, string version)
        {
            AppSession appSession = new AppSession(device.DeviceId, appId, timeWhenUsed, version, deviceService);
            appSession.Open();
            appSession.NavigateToSplash();
            appSession.NavigateToMain(getValue<long>(appScreenTimeUsed));
            appSession.ScreenMainActionSearch();
            appSession.NavigateToSearching(getValue<long>(appScreenTimeUsed));
            appSession.Searching(getValue<int>(contentLoading));
            appSession.NavigateToResults(getValue<long>(appScreenTimeUsed));
            appSession.ScreenResultActionViewProfile();
            appSession.NavigateToProfile(getValue<long>(appScreenTimeUsed));
            appSession.Close(getValue<long>(appTimeUsed));
        }

        static void route_settings_normal_journey_scroll(Device device, DateTime timeWhenUsed, string version)
        {
            AppSession appSession = new AppSession(device.DeviceId, appId, timeWhenUsed, version, deviceService);
            appSession.Open();
            appSession.NavigateToSplash();
            appSession.NavigateToMain(getValue<long>(appScreenTimeUsed));
            appSession.NavigateToSettings(getValue<long>(appScreenTimeUsed));
            appSession.NavigateToMain(getValue<long>(appScreenTimeUsed));
            appSession.ScreenMainActionSearch();
            appSession.NavigateToSearching(getValue<long>(appScreenTimeUsed));
            appSession.NavigateToResults(getValue<long>(appScreenTimeUsed));
            appSession.ScreenResultsActionScroll();
            appSession.NavigateToProfile(getValue<long>(appScreenTimeUsed));
            appSession.Close(getValue<long>(appTimeUsed));
        }

        static void route_settings_search_cancel_crash(Device device, DateTime timeWhenUsed, string version)
        {
            AppSession appSession = new AppSession(device.DeviceId, appId, timeWhenUsed, version, deviceService);
            appSession.Open();
            appSession.NavigateToSplash();
            appSession.NavigateToMain(getValue<long>(appScreenTimeUsed));
            appSession.NavigateToSettings(getValue<long>(appScreenTimeUsed));
            appSession.NavigateToSearching(getValue<long>(appScreenTimeUsed));
            appSession.ScreenMainActionCancelSearch();
            appSession.Crash();
            appSession.Close(getValue<long>(appTimeUsed));
        }

        static void route_settings_search_error(Device device, DateTime timeWhenUsed, string version)
        {
            AppSession appSession = new AppSession(device.DeviceId, appId, timeWhenUsed, version, deviceService);
            appSession.Open();
            appSession.NavigateToSplash();
            appSession.NavigateToMain(getValue<long>(appScreenTimeUsed));
            appSession.NavigateToSettings(getValue<long>(appScreenTimeUsed));
            appSession.NavigateToSearching(getValue<long>(appScreenTimeUsed));
            appSession.Error("Unexcepted exception", "Searching", 292838833, 20201111, 22);
            appSession.Close(getValue<long>(appTimeUsed));
        }

        static void route_normal_journey_feedback(Device device, DateTime timeWhenUsed, string version)
        {
            AppSession appSession = new AppSession(device.DeviceId, appId, timeWhenUsed, version, deviceService);
            appSession.Open();
            appSession.NavigateToSplash();
            appSession.NavigateToMain(getValue<long>(appScreenTimeUsed));
            appSession.ScreenMainActionSearch();
            appSession.NavigateToSearching(getValue<long>(appScreenTimeUsed));
            appSession.NavigateToResults(getValue<long>(appScreenTimeUsed));
            appSession.ScreenResultActionViewProfile();
            appSession.NavigateToProfile(getValue<long>(appScreenTimeUsed));
            appSession.Feedback(getValue<FeedbackRatingType>(Ratings), getValue<string>(feedbackMessage));
            appSession.Close(getValue<long>(appTimeUsed));
        }

        static void route_normal_journey_upgrade(Device device, DateTime timeWhenUsed, string version)
        {
            AppSession appSession = new AppSession(device.DeviceId, appId, timeWhenUsed, version, deviceService);
            appSession.Upgrade(version);
            appSession.Open();
            appSession.NavigateToSplash();
            appSession.NavigateToMain(getValue<long>(appScreenTimeUsed));
            appSession.ScreenMainActionSearch();
            appSession.NavigateToSearching(getValue<long>(appScreenTimeUsed));
            appSession.NavigateToResults(getValue<long>(appScreenTimeUsed));
            appSession.ScreenResultActionViewProfile();
            appSession.NavigateToProfile(getValue<long>(appScreenTimeUsed));
            appSession.Close(getValue<long>(appTimeUsed));
        }

        static T getValue<T>(T[] values)
        {
            return values[new Random().Next(0, values.Count())];
        }

        static T[] getValue<T>(T[][] values)
        {
            return values[new Random().Next(0, values.Count() - 1)];
        }

        static AppActs.API.Service.Interface.IDeviceService setup()
        {
            MongoClient client = new MongoClient(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            string database = ConfigurationManager.AppSettings["database"];

            AppActs.Repository.Interface.IApplicationRepository applicationRepository = new AppActs.Repository.ApplicationRepository(client, database);
            IDeviceRepository deviceRepository = new DeviceRepository(new DeviceMapper(client, database));
            IFeedbackRepository feedbackRepository = new FeedbackRepository(new FeedbackMapper(client, database));
            IEventRepository eventRep = new EventRepository(new EventMapper(client, database));
            ICrashRepository crashRep = new CrashRepository(new CrashMapper(client, database));
            IAppUserRepository appUserRep = new AppUserRepository(new AppUserMapper(client, database));
            IErrorRepository errorRep = new ErrorRepository(new ErrorMapper(client, database));
            ISystemErrorRepository systemErrorRep = new SystemErrorRepository(new SystemErrorMapper(client, database));

            return new DeviceService
                    (
                        deviceRepository,
                        errorRep,
                        eventRep,
                        crashRep,
                        feedbackRepository,
                        systemErrorRep,
                        appUserRep,
                        applicationRepository,
                        new Model.Settings()
                        {
                            DataLoggingRecordRaw = true,
                            DataLoggingRecordSystemErrors = true
                        }
                   );
        }
    }
}
