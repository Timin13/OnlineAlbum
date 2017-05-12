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
            var images = UserImages();
            
            if(images == null) return View();

            return View(images.OrderByDescending(d => d.ImageId).ToList());                     
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload, string description)
        {
            if (upload != null)
            {
                Random ran = new Random();
                string filename = "Image" + User.Identity.Name + "_" + ran.Next(1000000) + ".jpg";
                upload.SaveAs(Server.MapPath("~/Content/Img/" + filename));

                var image = UserImages();

                image.Add(new ImageModel()
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
            if (id == null)
            {
                var image = UserImages().OrderByDescending(d => d.ImageId).FirstOrDefault();

                if (image != null) id = image.ImageId;
            }
            var data = db.Images.Where(i => i.ImageId == id);

            return PartialView(data.ToList());
        }

        [HttpGet]
        public ActionResult DeleteView(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var image = DeleteImage(id);

            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        [HttpPost]
        public ActionResult DeleteConfirm(int id)
        {
            var image = DeleteImage(id);

            db.Images.Remove(image);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    
        private List<ImageModel> UserImages()
        {
            var images = db.UserProfiles.
                    Where(u => u.UserName == User.Identity.Name).
                        FirstOrDefault().
                            Images;
            return (images);
        }

        private ImageModel DeleteImage(int? id)
        {
            ImageModel image = db.Images.Find(id);

            if (image.UserProfiles.UserName == User.Identity.Name)
            {
                return (image);
            }
            return (null);
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