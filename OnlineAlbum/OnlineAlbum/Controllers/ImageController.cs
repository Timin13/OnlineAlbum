using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using OnlineAlbum.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using System.Net;

namespace OnlineAlbum.Controllers
{
    [Authorize]
    public class ImageController : Controller
    {
        DatabaseContext db = new DatabaseContext();

        // GET: Image
        public ActionResult Index()
        {
            var images = db.UserProfiles.Where(u => u.UserName == User.Identity.Name).
                FirstOrDefault().
                Images.OrderByDescending(d=>d.ImageId);

            if(images == null) return View();
            return View(images.ToList());                     
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload, string description)
        {
            if (upload != null)
            {
                Random ran = new Random();
                string filename = "Image" + User.Identity.Name + "_" + ran.Next(1000000) + ".jpg";
                upload.SaveAs(Server.MapPath("~/Content/Img/" + filename));

                db.UserProfiles.Where(u => u.UserName == User.Identity.Name).
                    FirstOrDefault().
                    Images.
                    Add(new ImageModel()
                    {
                        ImageDescription = description,
                        ImagePath = "/Content/Img/" + filename,
                        Rating = 0,
                        UploadDate = DateTime.Now
                    });
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult BigImage(int? id)
        {
            if(id == null)
            {
                var image = db.UserProfiles.
                    Where(u => u.UserName == User.Identity.Name).
                    FirstOrDefault().
                    Images.
                    OrderByDescending(d => d.ImageId).
                    FirstOrDefault();
                if (image != null) id = image.ImageId;
            }
            var data = db.Images.Where(i => i.ImageId == id);
            
            return PartialView(data.ToList());
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageModel image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        [HttpPost]
        public ActionResult DeleteConfirm(int id)
        {
            ImageModel image = db.Images.Find(id);
            db.Images.Remove(image);
            db.SaveChanges();
            return RedirectToAction("Index");
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