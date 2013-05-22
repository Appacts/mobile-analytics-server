using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Model;
using AppActs.Client.Model;
using AppActs.Client.Model.Enum;
using MongoDB.Bson;

namespace AppActs.Client.Service.Interface
{
    public interface IUserService
    {
        bool IsEmailAvailable(string email);
        void Save(User user);
        void Update(User user);
        User GetUser(ObjectId id);
        User GetUser(Guid id);
        User GetUser(string email, string password);
        IEnumerable<User> GetAll();
        string GeneratePassword();
        void UpdatePassword(ObjectId id, string password);
        User GetUser(string accountUserEmail);
        User GetUserByForgotPassword(Guid guidForgotPassword);
    }
}
