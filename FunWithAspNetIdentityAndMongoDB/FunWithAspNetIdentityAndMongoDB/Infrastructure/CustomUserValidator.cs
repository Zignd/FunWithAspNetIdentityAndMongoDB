using FunWithAspNetIdentityAndMongoDB.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace FunWithAspNetIdentityAndMongoDB.Infrastructure
{
    public class CustomUserValidator : UserValidator<AppUser>
    {
        public CustomUserValidator(AppUserManager manager) : base(manager)
        {
        }

        public override async Task<IdentityResult> ValidateAsync(AppUser user)
        {
            var result = await base.ValidateAsync(user);
            
            if (!user.Email.ToLower().EndsWith("@example.com"))
            {
                var errors = result.Errors.ToList();
                errors.Add("Only example.com email address are allowed");
                result = new IdentityResult(errors);
            }

            return result;
        }
    }
}