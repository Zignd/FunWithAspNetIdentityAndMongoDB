using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace FunWithAspNetIdentityAndMongoDB.Infrastructure
{
    public static class IdentityHelpers
    {
        public static MvcHtmlString GetUserName(this HtmlHelper html, string id)
        {
            var manager = HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>();
            var task = manager.FindByIdAsync(id);

            task.Wait();

            return new MvcHtmlString(task.Result.UserName);
        }

        public static MvcHtmlString ClaimType(this HtmlHelper html, string claimType)
        {
            var fields = typeof(ClaimTypes).GetFields();

            foreach (var field in fields)
            {
                if (field.GetValue(null).ToString() == claimType)
                    return new MvcHtmlString(field.Name);
            }

            return new MvcHtmlString(string.Format("{0}", claimType.Split('/', '.').Last()));
        }
    }
}