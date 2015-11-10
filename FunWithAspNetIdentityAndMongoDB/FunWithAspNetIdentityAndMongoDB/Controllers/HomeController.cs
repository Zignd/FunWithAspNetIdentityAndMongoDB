using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FunWithAspNetIdentityAndMongoDB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var data = new Dictionary<string, object>();
            data.Add("Placeholder", "Placeholder");
            return View(data);
        }
    }
}