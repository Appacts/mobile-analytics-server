using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AppActs.Client.Model
{
    public class User
    {
        public ObjectId Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public Guid ForgotPasswordGuid { get; set; }

        public User()
        {
           
        }

        public User(string name, string email, string password)
        {
            this.Id = ObjectId.GenerateNewId();
            this.Guid = Guid.NewGuid();
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.Active = true;
            this.DateCreated = DateTime.Now;
            this.DateModified = DateTime.Now;
        }

        public void Consume(User accountUser)
        {
            this.Id = accountUser.Id;
            this.Guid = accountUser.Guid;
            this.Name = accountUser.Name;
            this.Email = accountUser.Email;
            this.Active = accountUser.Active;
            this.DateCreated = accountUser.DateCreated;
            this.DateModified = accountUser.DateModified;
        }

        public void Clear()
        {
            this.Id = ObjectId.Empty;
            this.Guid = Guid.Empty;
            this.Name = null;
            this.Email = null;
            this.Password = null;
            this.Active = false;
        }

        public bool IsEmpty()
        {
            return this.Id == ObjectId.Empty;
        }
    }
}
