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

            return PartialView(searching.FirstOrDefault());
        }

        public ActionResult SubscribeMenu(string name)
        {
            var friendList = UserFriendList(name);

            if (db.UserProfiles.Where( n => n.UserName == name).Any() && name != User.Identity.Name)
            {
                if (friendList.Where(n => n.FriendName == name).Any())
                {
                    return PartialView("_UnSubscribe", name);
                }
                else
                {
                    return PartialView("_Subscribe", name);
                }  
            }
            return PartialView();
        }

       
        public ActionResult Subscribe(string name)
        {
            var subscribe = UserFriendList(name);

            subscribe.
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

        private List<FriendList> UserFriendList(string name)
        {
            var subscribe = db.UserProfiles.
            Where(u => u.UserName == User.Identity.Name).
                First().
                    FriendList;
            return (subscribe);
        }
    }
}