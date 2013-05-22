using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.API.Service.Interface;
using MongoDB.Bson;
using AppActs.API.Model.Feedback;
using AppActs.API.Model.Crash;
using AppActs.API.Model.Error;
using AppActs.API.Model.Enum;
using AppActs.API.Model.Upgrade;
using AppActs.API.Model.Event;

namespace AppActs.API.DataUploader
{
    public class AppSession
    {
        readonly Guid deviceId;
        readonly Guid applicationId;
        readonly IDeviceService iDeviceService;
        readonly DateTime timeWhenUsed;
        readonly string version;

        Guid sessionId;
        string lastScreen;

        public AppSession(Guid deviceId, Guid applicationId, 
            DateTime timeWhenUsed, string version, IDeviceService iDeviceService)
        {
            this.deviceId = deviceId;
            this.applicationId = applicationId;
            this.iDeviceService = iDeviceService;
            this.timeWhenUsed = timeWhenUsed;
            this.version = version;
        }

        public void Open()
        {
            sessionId = Guid.NewGuid();
            this.logEvent(EventType.ApplicationOpen, null, null, 0);
        }

        public void Upgrade(string appVersion)
        {
            this.iDeviceService.Log
                (
                    new UpgradeInfo()
                    {
                        ApplicationId = this.applicationId,
                        DeviceId = this.deviceId,
                        Version = appVersion
                    }
                );
        }

        public void NavigateToSplash()
        {
            string screenName = "Splash";
            this.logScreenOpen(screenName);
            this.lastScreen = screenName;
        }

        public void NavigateToMain(long timeUsedPrevScreen)
        {
            this.navigateToNextScreen("Main", this.lastScreen, timeUsedPrevScreen);
        }

        public void NavigateToSettings(long timeUsedPrevScreen)
        {
            this.navigateToNextScreen("Settings", this.lastScreen, timeUsedPrevScreen);
        }

        public void NavigateToSearching(long timeUsedPrevScreen)
        {
            this.navigateToNextScreen("Searching", this.lastScreen, timeUsedPrevScreen);
        }

        public void NavigateToResults(long timeUsedPrevScreen)
        {
            this.navigateToNextScreen("Results", this.lastScreen, timeUsedPrevScreen);
        }

        public void NavigateToProfile(long timeUsedPrevScreen)
        {
            this.navigateToNextScreen("Profile", this.lastScreen, timeUsedPrevScreen);
        }


        public void ScreenMainActionSearch()
        {
            this.logEvent(EventType.Event, "Main", "Search", 0);     
        }

        public void ScreenMainActionCancelSearch()
        {
            this.logEvent(EventType.Event, "Main", "Cancel Search", 0);  
        }

        public void ScreenResultsActionScroll()
        {
            this.logEvent(EventType.Event, "Results", "Scroll", 0);  
        }

        public void ScreenResultActionViewProfile()
        {
            this.logEvent(EventType.Event, "Results", "View Profile", 0);  
        }

        public void Close(long timeUsed)
        {
            this.logEvent(EventType.ApplicationClose, null, null, timeUsed);
        }

        public void Searching(int searching)
        {
            this.logEvent(EventType.ContentLoaded, "Searching", "ContentLookup", searching);
        }

        public void Error(string errorMessage, string action, long flashSize, long memSize, int battery)
        {
            this.addTime();
            this.iDeviceService.Log
                (
                    new Error()
                    {
                        ApplicationId = applicationId,
                        DeviceId = deviceId,
                        EventName = action,
                        AvailableFlashDriveSize = flashSize,
                        AvailableMemorySize = memSize,
                        Battery = battery,
                        Message = errorMessage,
                        ScreenName = this.lastScreen,
                        SessionId = sessionId,
                        DateCreatedOnDevice = this.timeWhenUsed,
                        Version = version,
                        Date = this.timeWhenUsed.Date,
                        DateCreated = this.timeWhenUsed
                    }
                );
        }

        public void Crash()
        {
            this.addTime();

            this.iDeviceService.Log
                (
                    new Crash()
                    {
                        ApplicationId = applicationId,
                        DeviceId = deviceId,
                        Version = version,
                        SessionId = sessionId,
                        DateCreatedOnDevice = this.timeWhenUsed,
                        Date = this.timeWhenUsed.Date,
                        DateCreated = this.timeWhenUsed
                    }
                );

        }

        public void Feedback(FeedbackRatingType ratingType, string comment)
        {
            this.addTime();

            this.iDeviceService.Log
                (
                    new Feedback()
                    {
                        ApplicationId = applicationId,
                        DeviceId = deviceId,
                        Message = comment,
                        Rating = ratingType,
                        ScreenName = this.lastScreen,
                        SessionId = sessionId,
                        DateCreatedOnDevice = timeWhenUsed,
                        Version = version,
                        Date = this.timeWhenUsed.Date,
                        DateCreated = this.timeWhenUsed
                    }
                );

        }

        private void navigateToNextScreen(string nextScreen, string lastSreen, long lastScreenUsedTime)
        {
            this.logScreenClose(lastScreen, lastScreenUsedTime);
            this.logScreenOpen(nextScreen);
            this.lastScreen = nextScreen;
        }

        private void logScreenOpen(string screenName)
        {
            this.logEvent(EventType.ScreenOpen, screenName, null, 0);
        }

        private void logScreenClose(string screenName, long timeUsed)
        {
            this.logEvent(EventType.ScreenClose, screenName, null, timeUsed);
        }

        private void logEvent(EventType eventType, string screenName, string action, long timeUsed)
        {
            this.addTime();
            this.iDeviceService.Log
                (
                    new Event()
                    {
                        ApplicationId = applicationId,
                        DeviceId = deviceId,
                        EventTypeId = eventType,
                        EventName = action,
                        DateCreatedOnDevice = timeWhenUsed,
                        ScreenName = screenName,
                        SessionId = sessionId,
                        Length = timeUsed,
                        Version = version,
                        Date = timeWhenUsed.Date,
                        DateCreated = timeWhenUsed
                    }
                );
        }
        
        private void addTime()
        {
            this.timeWhenUsed.AddSeconds(5);
        }
    }
}
