using OnlineAlbum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineAlbum.Controllers
{
    [Authorize]
    public class FriendImagesController : Controller
    {
        DatabaseContext db = new DatabaseContext();

        // GET: FriendImages
        public ActionResult Index(string name)
        {
            var currentUser = db.UserProfiles.Where(u => u.UserName == User.Identity.Name).First();

            if (currentUser.FriendList.Where(u => u.FriendName == name).Any())
            {
                if(db.UserProfiles.Where(u => u.UserName == name).First().Images.Any())
                {
                    var images = db.UserProfiles.Where(u => u.UserName == name).First().Images;
                    if (images == null) return View();
                    return View(images);
                }             
            }
            else
            {
                return View();
            }
            return View(); 
        }

        public ActionResult BigImage(int? id)
        {
            /*if (id == null)
            {
                var image = db.UserProfiles.
                    Where(u => u.).
                    FirstOrDefault().
                    Images.
                    OrderByDescending(d => d.ImageId).
                    FirstOrDefault();
                if (image != null) return View(db.Images.Where( i => i.ImageId == image.ImageId).ToList());
            }*/
            var data = db.Images.Where(i => i.ImageId == id);

            return PartialView(data.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}