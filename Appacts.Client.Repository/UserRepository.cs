using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Model;
using AppActs.Client.Model;
using System.Data;
using System.Data.SqlClient;
using AppActs.Core.Exceptions;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using AppActs.Repository;
using AppActs.Client.Repository.Interface;

namespace AppActs.Client.Repository
{
    public class UserRepository : NoSqlBase<User>, IUserRepository
    {
        public UserRepository(MongoClient client, string databaseName)
            : base(client, databaseName)
        {

        }

        public void Update(User accountUser)
        {
            try
            {
                var query = Query<User>.EQ<ObjectId>(x => x.Id, accountUser.Id);
                var update = Update<User>
                    .Set(x => x.Active, accountUser.Active)
                    .Set(x => x.DateModified, accountUser.DateModified)
                    .Set(x => x.Name, accountUser.Name)
                    .Set(x => x.Email, accountUser.Email)
                    .Set(x => x.ForgotPasswordGuid, accountUser.ForgotPasswordGuid);
                
                this.GetCollection().Update(query, update);
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }


        public User Find(ObjectId id)
        {
            try
            {
                return this.GetCollection().FindOne(Query<User>.EQ<ObjectId>(x => x.Id, id));
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }

        public User Find(Guid id)
        {
            try
            {
                return this.GetCollection().FindOne(Query<User>.EQ<Guid>(x => x.Guid, id));
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }

        public User Find(string email, string password)
        {
            try
            {
                var query = Query.And
                    (
                        Query<User>.EQ<string>(x => x.Email, email),
                        Query<User>.EQ<string>(x => x.Password, password)
                    );

                return this.GetCollection().FindOne(query);
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }

        public void UpdatePassword(ObjectId id, string password)
        {
            try
            {
                var query = Query<User>.EQ<ObjectId>(x => x.Id, id);
                var update = Update<User>
                        .Set(x => x.Password, password);
                this.GetCollection().Update(query, update);
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }

        public User Find(string accountUserEmail)
        {
            try
            {
                var query = Query.And
                    (
                        Query<User>.EQ<string>(x => x.Email, accountUserEmail),
                        Query<User>.EQ<bool>(x => x.Active, true)
                    );

                return this.GetCollection().FindOne(query);
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }

        public User FindByForgotPassword(Guid guidForgotPassword)
        {
            try
            {
                return this.GetCollection().FindOne(Query<User>.EQ<Guid>(x => x.ForgotPasswordGuid, guidForgotPassword));
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }

        public IEnumerable<User> FindAll()
        {
            try
            {
                return this.GetCollection().Find(Query<User>.EQ<bool>(x => x.Active, true));
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }
    }
}
