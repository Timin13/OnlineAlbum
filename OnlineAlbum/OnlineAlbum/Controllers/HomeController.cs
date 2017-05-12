using OnlineAlbum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineAlbum.Controllers
{
    public class HomeController : Controller
    {
        DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                var friendList = db.UserProfiles.
                        Where(u => u.UserName == User.Identity.Name).
                            First().
                                FriendList;
                if (friendList == null) return View();
                return View(friendList);
            }
            return View();
        }
    }
}