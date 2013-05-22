using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.DomainModel;
using System.Configuration;

namespace AppActs.API.Repository.MSTest
{
    public class TestBase
    {
        protected string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        /// <summary>
        /// Gets or sets the account.
        /// </summary>
        /// <value>
        /// The account.
        /// </value>
        protected Account Account 
        {
            get
            {
                if (account == null)
                {
                    AppActs.Client.Repository.Interface.IAccountRepository iAccountRepositoryClient =
                        new AppActs.Client.Repository.AccountRepository(this.connectionString);

                    account = new AppActs.DomainModel.Account();
                    account.Guid = Guid.NewGuid();
                    account.AccountType = AppActs.DomainModel.Enum.AccountType.Developer;
                    account.Active = true;
                    account.DateModified = DateTime.Now;
                    account.DateCreated = DateTime.Now;

                    iAccountRepositoryClient.Save(account);
                }

                return account;
            }
        }

        /// <summary>
        /// Gets or sets the application.
        /// </summary>
        /// <value>
        /// The application.
        /// </value>
        protected Application Application
        {
            get
            {
                if (application == null)
                {
                    AppActs.Client.Repository.Interface.IApplicationRepository iApplicationRepositoryClient =
                        new AppActs.Client.Repository.ApplicationRepository(this.connectionString);

                    application = new Application("Random Name", this.Account.Id);

                    iApplicationRepositoryClient.Save(application);
                }

                return application;
            }
        
        }

        /// <summary>
        /// Gets or sets the device.
        /// </summary>
        /// <value>
        /// The device.
        /// </value>
        protected AppActs.DomainModel.Device Device 
        {
            get
            {
                if(device == null)
                {
                    AppActs.API.Repository.Interface.IDeviceRepository iDeviceRepository =
                        new AppActs.API.Repository.DeviceRepository(this.connectionString);

                    device =
                        new AppActs.DomainModel.Device("9700", AppActs.DomainModel.Enum.PlatformType.Blackberry, "o2", "5.2", -1, "en", null, null, null);

                    iDeviceRepository.Save(device);
                }

                return device;
            }
        }

        private static Account account = null;
        private static Application application = null;
        private static AppActs.DomainModel.Device device = null;
    }
}
