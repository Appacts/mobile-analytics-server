using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Client.Service.Interface;
using AppActs.Model;
using AppActs.Client.Repository.Interface;
using AppActs.Client.Model;
using AppActs.Client.Model.Enum;
using AppActs.Core.Encryption;
using AppActs.Core.Exceptions;
using System.Configuration;
using MongoDB.Bson;

namespace AppActs.Client.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository accountUserRepository;
        private readonly AppActs.Client.Model.Settings settings; 

        public UserService(IUserRepository accountUserRepository, 
            AppActs.Client.Model.Settings settings)
        {
            this.accountUserRepository = accountUserRepository;
            this.settings = settings;
        }

        public bool IsEmailAvailable(string email)
        {
            return this.accountUserRepository.Find(email) == null;
        }

        public void Save(User accountUser)
        {
            try
            {
                accountUser.Password = DES.Encrypt
                    (
                        this.settings.SecurityKey,
                        SHA2.GetSHA256Hash(accountUser.Password)
                    );

                accountUser.Name = accountUser.Name.Substring(0, 1).ToUpper() +
                                    accountUser.Name.Substring(1, accountUser.Name.Length - 1);

                this.accountUserRepository.Save(accountUser);
            }
            catch (DataAccessLayerException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceLayerException(ex);
            }
        }

        public void Update(User accountUser)
        {
            try
            {
                accountUser.Name = accountUser.Name.Substring(0, 1).ToUpper() +
                                    accountUser.Name.Substring(1, accountUser.Name.Length - 1);

                this.accountUserRepository.Update(accountUser);
            }
            catch (DataAccessLayerException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceLayerException(ex);
            }
        }

        public User GetUser(ObjectId accountUserId)
        {
            return this.accountUserRepository.Find(accountUserId);
        }

        public User GetUser(Guid accountUserGuid)
        {
            return this.accountUserRepository.Find(accountUserGuid);
        }

        public User GetUser(string email, string password)
        {
            User accountUser = null;

            try
            {
                password = DES.Encrypt(
                        this.settings.SecurityKey,
                        SHA2.GetSHA256Hash(password)
                    );

                accountUser = 
                    this.accountUserRepository.Find(email, password);
            }
            catch (DataAccessLayerException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceLayerException(ex);
            }

            return accountUser;
        }

        public IEnumerable<User> GetAll()
        {
            return this.accountUserRepository.FindAll();
        }

        public string GeneratePassword()
        {
            return Guid.NewGuid().ToString().Substring(0, 6);
        }

        public void UpdatePassword(ObjectId userId, string password)
        {
            try
            {
                password = DES.Encrypt(
                        this.settings.SecurityKey,
                        SHA2.GetSHA256Hash(password)
                    );

                this.accountUserRepository.UpdatePassword(userId, password);
            }
            catch (DataAccessLayerException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceLayerException(ex);
            }
        }

        public User GetUser(string accountUserEmail)
        {
            return this.accountUserRepository.Find(accountUserEmail);
        }

        public User GetUserByForgotPassword(Guid guidForgotPassword)
        {
            return this.accountUserRepository.FindByForgotPassword(guidForgotPassword);
        }

    }
}
