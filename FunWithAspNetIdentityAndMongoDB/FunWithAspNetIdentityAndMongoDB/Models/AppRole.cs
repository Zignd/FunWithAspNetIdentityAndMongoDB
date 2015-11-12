using AspNet.Identity.MongoDB;
using FunWithAspNetIdentityAndMongoDB.Infrastructure;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FunWithAspNetIdentityAndMongoDB.Models
{
    public class AppRole : IdentityRole
    {
        public AppRole() : base()
        {
        }

        public AppRole(string roleName) : base(roleName)
        {
        }

        public List<AppUser> GetUsers(AppIdentityDbContext identityDbContext)
        {
            var filter = Builders<AppUser>.Filter.AnyIn(x => x.Roles, new string[] { Name });
            var task = identityDbContext.Users.Find(filter).ToListAsync();

            task.Wait();

            return task.Result;
        }
    }
}