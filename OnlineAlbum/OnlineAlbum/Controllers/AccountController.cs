using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineAlbum.Models;
using System.Web.Security;
using System.IO;
using System.Web.Providers.Entities;
using static OnlineAlbum.Models.RegAndLog;

namespace OnlineAlbum.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if(Membership.ValidateUser(model.Login, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Login, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }else
                {
                    ModelState.AddModelError("", "Login or password are incorrect!");
                }
            }
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.Login, model.Password, model.Email, 
                    passwordQuestion: null, passwordAnswer:null, isApproved: true, 
                    providerUserKey: null, status: out createStatus);

                if(createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.Login, false);
                    using(DatabaseContext db = new DatabaseContext())
                    {
                        db.UserProfiles.Add(new UserProfile()
                        {
                            UserName = model.Login
                        });
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Registration error!");
                }
            }
            return View(model);
        }

        public ActionResult Manage()
        {
            return View();
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}