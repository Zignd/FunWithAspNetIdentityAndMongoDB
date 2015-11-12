using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}