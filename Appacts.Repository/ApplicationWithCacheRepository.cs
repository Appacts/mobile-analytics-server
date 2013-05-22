using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Repository.Interface;
using AppActs.Model;
using CacheProvider.Interface;
using MongoDB.Bson;

namespace AppActs.Repository
{
    public class ApplicationWithCacheRepository : IApplicationRepository
    {
        readonly IApplicationRepository applicationRepository;
        readonly ICacheProvider<Application> cacheProvider;

        public ApplicationWithCacheRepository(IApplicationRepository applicationRepository, ICacheProvider<Application> cacheProvider)
        {
            this.applicationRepository = applicationRepository;
            this.cacheProvider = cacheProvider;
        }

        public Application Find(Guid id)
        {
            return this.cacheProvider.Fetch
                (
                    String.Format("IApplicationRepository.Get.{0}", id),
                    () => this.applicationRepository.Find(id), 
                    null, 
                    new TimeSpan(0, 15, 0)
               );
        }


        public IEnumerable<string> GetVersionsByApplication(Guid id)
        {
            return this.applicationRepository.GetVersionsByApplication(id);
        }
    }
}
