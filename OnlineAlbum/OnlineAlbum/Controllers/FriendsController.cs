using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineAlbum.Models;

namespace OnlineAlbum.Controllers
{

    [Authorize]
    public class FriendsController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        // GET: Followers
        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult SearchPeople(string name)
        {
            if(name == null)
            {
                return RedirectToAction("Index");
            }
            
            var searching = db.UserProfiles.Where(u => u.UserName == name);

            if (searching.Any() == false)
            {
                return PartialView();
            }
            return PartialView(searching.First());
        }

        public ActionResult SubscribeMenu(string name)
        {
            var subscribe = db.UserProfiles.
            Where(u => u.UserName == User.Identity.Name).
            First().
            FriendList;
            if (db.UserProfiles.Where( n => n.UserName == name).Any())
            {
                if (subscribe.Where(n => n.FriendName == name).Any())
                {
                    return PartialView("_UnSubscribe", name);
                }
                else
                {
                    return PartialView("_Subscribe", name);
                } 
            }
            return RedirectToAction("Index");
        }

       
        public ActionResult Subscribe(string name)
        {
            db.UserProfiles.
            Where(u => u.UserName == User.Identity.Name).
            First().
            FriendList.
            Add(new FriendList()
            {
                FriendName = name
            });
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

    
        public ActionResult UnSubscribe(string name)
        {
            var friend = db.FriendList.Where(n => n.FriendName == name).First();
            db.FriendList.Remove(friend);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}