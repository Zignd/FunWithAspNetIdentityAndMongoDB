﻿using FunWithAspNetIdentityAndMongoDB.Infrastructure;
using FunWithAspNetIdentityAndMongoDB.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FunWithAspNetIdentityAndMongoDB.Controllers
{
    // TODO: Remove this attribute in order to be able to perform the creation of the initial users
    // The ideal way of doing this is to seed the database when the application runs for the first time
    [Authorize(Roles = "Administrators")]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View(HttpContext.GetOwinContext().Get<AppIdentityDbContext>().Users);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.Name, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    AddErrorsFromResult(result);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            var user = await UserManager.FindByIdAsync(id);

            if (user != null)
            {
                var result = await UserManager.DeleteAsync(user);

                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    return View("Error", result.Errors);
            }
            else
            {
                return View("Error", new string[] { "User Not Found" });
            }
        }

        public async Task<ActionResult> Edit(string id)
        {
            var user = await UserManager.FindByIdAsync(id);

            if (user != null)
                return View(user);
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(string id, string email, string password)
        {
            var user = await UserManager.FindByIdAsync(id);

            if (user != null)
            {
                user.Email = email;
                var validEmail = await UserManager.UserValidator.ValidateAsync(user);

                if (!validEmail.Succeeded)
                    AddErrorsFromResult(validEmail);

                IdentityResult validPass = null;

                if (password != string.Empty)
                {
                    validPass = await UserManager.PasswordValidator.ValidateAsync(password);

                    if (validPass.Succeeded)
                        user.PasswordHash = UserManager.PasswordHasher.HashPassword(password);
                    else
                        AddErrorsFromResult(validPass);
                }

                if ((validEmail.Succeeded && validPass == null) || (validEmail.Succeeded && password != string.Empty && validPass.Succeeded))
                {
                    var result = await UserManager.UpdateAsync(user);

                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    else
                        AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }

            return View(user);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error);
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
    }
}