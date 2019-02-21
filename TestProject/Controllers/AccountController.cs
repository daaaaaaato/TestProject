using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestProject.Models;

namespace TestProject.Controllers
{
    public class AccountController : Controller
    {
        private BloggerEntities db = new BloggerEntities();

        [HttpGet]
        [Route("login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public ActionResult Login(AccountModel model)
        {
            using (var db = new BloggerEntities())
            {
                var passHash = Cryptography.SHA256(model.Password);
                var authResult = db.Users.SingleOrDefault(x => x.Email == model.Email && x.Password == passHash);

                if (authResult != null)
                {
                    Session["Users"] = authResult;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View();
                }
            }
        }



        [Route("logout")]
        public ActionResult Logout()
        {
            Session.Clear();
            return Redirect("/");
        }


        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(AccountModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    BirthDate = model.BirthDate,
                    Password = Cryptography.SHA256(model.Password)
                };
                db.Users.Add(entity);
                db.SaveChanges();
                return RedirectToAction("login", "Account");
            }
            return View();
        }
    }
}