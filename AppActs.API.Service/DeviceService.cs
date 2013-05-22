using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Model;
using AppActs.API.Service.Interface;
using AppActs.Model.Enum;
using log4net;
using AppActs.API.Repository.Interface;
using System.Collections.Concurrent;
using AppActs.API.Model;
using MongoDB.Bson;
using AppActs.API.Model.Exception;
using AppActs.API.Model.Enum;
using AppActs.API.Model.Crash;
using AppActs.API.Model.Device;
using AppActs.API.Model.Error;
using AppActs.API.Model.Event;
using AppActs.API.Model.Feedback;
using AppActs.API.Model.SystemError;
using AppActs.API.Model.User;
using AppActs.API.Model.Upgrade;

namespace AppActs.API.Service
{
    public class DeviceService : IDeviceService
    {
        readonly IDeviceRepository deviceRepository;
        readonly IErrorRepository errorRepository;
        readonly IEventRepository eventRepository;
        readonly ICrashRepository crashRepository;
        readonly IFeedbackRepository feedbackRepository;
        readonly ISystemErrorRepository systemErrorRepository;
        readonly IAppUserRepository appUserRepository;
        readonly AppActs.Repository.Interface.IApplicationRepository applicationRepository;
        readonly AppActs.API.Model.Settings settings;

        public DeviceService(IDeviceRepository deviceRepository, IErrorRepository errorRepository,
            IEventRepository eventRepository, ICrashRepository crashRepository, IFeedbackRepository feedbackRepository,
            ISystemErrorRepository systemErrorRepository, IAppUserRepository appUserRepository,
             AppActs.Repository.Interface.IApplicationRepository applicationRepository, AppActs.API.Model.Settings settings)
        {
            this.deviceRepository = deviceRepository;
            this.errorRepository = errorRepository;
            this.eventRepository = eventRepository;
            this.crashRepository = crashRepository;
            this.feedbackRepository = feedbackRepository;
            this.systemErrorRepository = systemErrorRepository;
            this.appUserRepository = appUserRepository;
            this.applicationRepository = applicationRepository;
            this.settings = settings;
        } 

        public void Log(Crash crash)
        {
            Application application = this.applicationRepository.Find(crash.ApplicationId);

            if (application != null)
            {
                DeviceInfo device = this.deviceRepository.Find(crash.DeviceId);
                
                if (device != null)
                {
                    string lastScreenBeforeCrash = null;
                    DeviceAppLastScreen appLastScreen =
                        this.eventRepository.GetDeviceAppLastScreenOneBy(crash.DeviceId, crash.ApplicationId);

                    if (appLastScreen != null)
                    {
                        lastScreenBeforeCrash = appLastScreen.ScreenName;
                    }

                    crash.Add(device, lastScreenBeforeCrash);

                    CrashSummary crashSummary = new CrashSummary(crash);

                    this.crashRepository.Save(crash);
                    this.crashRepository.Save(crashSummary);
                }
                else
                {
                    throw new NoDeviceException(crash.DeviceId);
                }
            }
            else
            {
                throw new InactiveApplicationException(crash.ApplicationId);
            }
        }

        public void Log(DeviceInfo device, ApplicationInfo applicationInfo)
        {
            Application application = this.applicationRepository.Find(applicationInfo.ApplicationId);

            if (application != null)
            {
                device.Guid = Guid.NewGuid();
                this.deviceRepository.Save(device);

                DeviceSummary deviceSum = new DeviceSummary(device, applicationInfo);
                this.deviceRepository.Save(deviceSum);
            }
            else
            {
                throw new InactiveApplicationException(applicationInfo.ApplicationId);
            }
        }

        public void Log(Error error)
        {
            Application application = this.applicationRepository.Find(error.ApplicationId);

            if (application != null)
            {
                DeviceInfo device = this.deviceRepository.Find(error.DeviceId);

                if (device != null)
                {
                    error.Add(device);

                    ErrorSummary errorSummary = new ErrorSummary(error);

                    this.errorRepository.Save(error);
                    this.errorRepository.Save(errorSummary);
                }
                else
                {
                    throw new NoDeviceException(error.DeviceId);
                }
            }
            else
            {
                throw new InactiveApplicationException(error.ApplicationId);
            }
        }

        public void Log(Event eventItem)
        {
            Application application = this.applicationRepository.Find(eventItem.ApplicationId);

            if (application != null)
            {
                DeviceInfo device = this.deviceRepository.Find(eventItem.DeviceId);

                if (device != null)
                {
                    eventItem.PlatformId = device.PlatformType;

                    switch (eventItem.EventTypeId)
                    {
                        case EventType.ApplicationOpen:
                            Nullable<DateTime> lastDeviceVisit = this.eventRepository
                                .GetDateOfDeviceLastVisit(eventItem.DeviceId, eventItem.ApplicationId);

                            AppUsageSummary appUsageSummary = new AppUsageSummary(eventItem, lastDeviceVisit);
                            this.eventRepository.Save(appUsageSummary);
                            break;

                        case EventType.ApplicationClose:
                            AppUsageDurationSummary appUsageSum = new AppUsageDurationSummary(eventItem);
                            this.eventRepository.Save(appUsageSum);

                            DeviceAppLastScreen appLastScreen =
                                this.eventRepository.GetDeviceAppLastScreenOneBy(eventItem.DeviceId, eventItem.ApplicationId);

                            if (appLastScreen != null)
                            {
                                ScreenRouteSummary routeSum = 
                                    new ScreenRouteSummary(eventItem, appLastScreen.ScreenName, string.Empty);

                                this.eventRepository.Save(routeSum);
                                this.eventRepository.Remove(eventItem.DeviceId, eventItem.ApplicationId);
                            }
                            break;

                        case EventType.Event:
                            EventSummary eventSum = new EventSummary(eventItem);
                            this.eventRepository.Save(eventSum);
                            break;

                        case EventType.ScreenClose:
                            ScreenSummary screenUsageSum = new ScreenSummary(eventItem);
                            this.eventRepository.Save(screenUsageSum); 
                            this.eventRepository.Remove(eventItem.DeviceId, eventItem.ApplicationId);
                            this.eventRepository.Save(new DeviceAppLastScreen(eventItem));
                            break;

                        case EventType.ScreenOpen:
                            DeviceAppLastScreen lastScreen = 
                                this.eventRepository.GetDeviceAppLastScreenOneBy(eventItem.DeviceId, eventItem.ApplicationId);

                            ScreenRouteSummary routeSum2 = new ScreenRouteSummary(eventItem, string.Empty, eventItem.ScreenName);

                            if (lastScreen != null)
                            {
                                if (lastScreen.SessionId == eventItem.SessionId)
                                {
                                    routeSum2 = new ScreenRouteSummary(eventItem, lastScreen.ScreenName, eventItem.ScreenName);
                                }

                                this.eventRepository.Remove(eventItem.DeviceId, eventItem.ApplicationId);
                            }

                            this.eventRepository.Save(routeSum2);
                            this.eventRepository.Save(new DeviceAppLastScreen(eventItem));
                            break;

                        case EventType.ContentLoaded:
                            ContentLoadSummary contentSum = new ContentLoadSummary(eventItem);
                            this.eventRepository.Save(contentSum);
                            break;
                    }

                    if (this.settings.DataLoggingRecordRaw)
                    {
                        this.eventRepository.Save(eventItem);
                    }
                }
                else
                {
                    throw new NoDeviceException(eventItem.DeviceId);
                }
            }
            else
            {
                throw new InactiveApplicationException(eventItem.ApplicationId);
            }
        }

        public void Log(Feedback feedbackItem)
        {
            Application application = this.applicationRepository.Find(feedbackItem.ApplicationId);

            if (application != null)
            {
                DeviceInfo device = this.deviceRepository.Find(feedbackItem.DeviceId);

                if (device != null)
                {
                    feedbackItem.PlatformId = device.PlatformType;

                    FeedbackSummary feedbackSum = new FeedbackSummary(feedbackItem);

                    this.feedbackRepository.Save(feedbackSum);
                    this.feedbackRepository.Save(feedbackItem);
                }
                else
                {
                    throw new NoDeviceException(feedbackItem.DeviceId);
                }
            }
            else
            {
                throw new InactiveApplicationException(feedbackItem.ApplicationId);
            }
        }

        public void Log(SystemError systemError)
        {
            Application application = this.applicationRepository.Find(systemError.ApplicationId);

            if (application != null)
            {
                DeviceInfo device = this.deviceRepository.Find(systemError.DeviceId);

                if (device != null && this.settings.DataLoggingRecordSystemErrors)
                {
                    this.systemErrorRepository.Save(systemError);
                }
                else
                {
                    throw new NoDeviceException(systemError.DeviceId);
                }
            }
            else
            {
                throw new InactiveApplicationException(systemError.ApplicationId);
            }
        }

        public void Log(AppUser user)
        {
            Application application = this.applicationRepository.Find(user.ApplicationId);

            if (application != null)
            {
                DeviceInfo device = this.deviceRepository.Find(user.DeviceId);

                if (device != null)
                {
                    device.PlatformType = user.PlatformId;

                    AppUserSummary appUserSum = new AppUserSummary(user);

                    this.appUserRepository.Save(appUserSum);

                    if (this.settings.DataLoggingRecordRaw)
                    {
                        this.appUserRepository.Save(user);
                    }
                }
                else
                {
                    throw new NoDeviceException(user.DeviceId);
                }
            }
            else
            {
                throw new InactiveApplicationException(user.ApplicationId);
            }
        }

        public void Log(DeviceLocation deviceLocation, ApplicationInfo applicationInfo)
        {
            Application application = this.applicationRepository.Find(applicationInfo.ApplicationId);

            if (application != null)
            {
                DeviceInfo device = this.deviceRepository.Find(deviceLocation.DeviceId);

                if (device != null)
                {
                    this.deviceRepository.Save(deviceLocation);
                }
                else
                {
                    throw new NoDeviceException(deviceLocation.DeviceId);
                }
            }
            else
            {
                throw new InactiveApplicationException(applicationInfo.ApplicationId);
            }
        }

        public void Log(UpgradeInfo upgradeInfo)
        {
            Application application = this.applicationRepository.Find(upgradeInfo.ApplicationId);

            if (application != null)
            {
                DeviceInfo device = this.deviceRepository.Find(upgradeInfo.DeviceId);

                if (device != null)
                {
                    DeviceUpgradeSummary deviceUpgradeSum =
                        new DeviceUpgradeSummary(upgradeInfo.Version, DateTime.Today, upgradeInfo.ApplicationId, device.PlatformType);

                    this.deviceRepository.Save(deviceUpgradeSum);
                }
                else
                {
                    throw new NoDeviceException(upgradeInfo.DeviceId);
                }
            }
            else
            {
                throw new InactiveApplicationException(upgradeInfo.ApplicationId);
            }
        }

    }
}
