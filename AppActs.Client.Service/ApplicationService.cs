using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Client.Service.Interface;
using AppActs.Model;
using AppActs.Client.Model;
using AppActs.Client.Repository.Interface;
using AppActs.Core.Exceptions;
using MongoDB.Bson;

namespace AppActs.Client.Service
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository applicationRepository;
        private readonly IPlatformRepository platformRepository;

        public ApplicationService(IApplicationRepository applicationRepository, 
            IPlatformRepository platformRepository)
        {
            this.applicationRepository = applicationRepository;
            this.platformRepository = platformRepository;
        }

        public bool IsApplicationNameAvailable(string applicationName)
        {
            return this.applicationRepository.Find(applicationName) == null;
        }

        public void Save(Application application)
        {
            this.applicationRepository.Save(application);
        }

        public void Update(Application application)
        {
            this.applicationRepository.Update(application);
        }

        public Application Get(Guid applicationId)
        {
            return this.applicationRepository.Find(applicationId);
        }

        public IEnumerable<string> GetVersions(Guid applicationId)
        {
            return this.applicationRepository.GetVersionsByApplication(applicationId);
        }

        public IEnumerable<Platform> GetPlatforms()
        {
            return this.platformRepository.FindAll();
        }

        public void Save(IEnumerable<Platform> platforms)
        {
            this.platformRepository.Save(platforms);
        }

        public IEnumerable<Application> GetAll()
        {
            return this.applicationRepository.FindAll();
        }
    }
}
