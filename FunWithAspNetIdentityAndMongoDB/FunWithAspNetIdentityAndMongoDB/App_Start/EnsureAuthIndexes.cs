using AspNet.Identity.MongoDB;
using FunWithAspNetIdentityAndMongoDB.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FunWithAspNetIdentityAndMongoDB
{
    public class EnsureAuthIndexes
    {
        public static void Exist()
        {
            var context = AppIdentityDbContext.Create();
            IndexChecks.EnsureUniqueIndexOnUserName(context.Users);
            IndexChecks.EnsureUniqueIndexOnRoleName(context.Roles);
        }
    }
}