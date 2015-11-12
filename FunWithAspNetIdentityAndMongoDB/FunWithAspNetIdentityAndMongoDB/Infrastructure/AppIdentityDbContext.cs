using AspNet.Identity.MongoDB;
using FunWithAspNetIdentityAndMongoDB.Infrastructure;
using FunWithAspNetIdentityAndMongoDB.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FunWithAspNetIdentityAndMongoDB.Infrastructure
{
    public class AppIdentityDbContext : IDisposable
    {
        public static AppIdentityDbContext Create()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("FunWithAspNetIdentityAndMongoDB");
            var users = database.GetCollection<AppUser>("Users");
            var roles = database.GetCollection<AppRole>("Roles");
            return new AppIdentityDbContext(users, roles);
        }
        
        private AppIdentityDbContext(IMongoCollection<AppUser> users, IMongoCollection<AppRole> roles)
        {
            Users = users;
            Roles = roles;
        }

        public IMongoCollection<AppUser> Users { get; set; }

        public IMongoCollection<AppRole> Roles { get; set; }

        public void Dispose()
        {
            // Purposely left empty
        }
    }
}