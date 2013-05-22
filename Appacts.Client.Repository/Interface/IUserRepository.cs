using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Client.Model;
using MongoDB.Bson;

namespace AppActs.Client.Repository.Interface
{
    public interface IUserRepository
    {
        void Save(User accountUser);
        void Update(User accountUser);
        User Find(ObjectId id);
        User Find(Guid id);
        User Find(string email, string password);
        void UpdatePassword(ObjectId id, string password);
        User Find(string accountUserEmail);
        User FindByForgotPassword(Guid guidForgotPassword);
        IEnumerable<User> FindAll();
    }
}
