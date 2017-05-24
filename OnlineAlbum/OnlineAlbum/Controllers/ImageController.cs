using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using OnlineAlbum.Models;
using System.Drawing;
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
                var currentUserImage = UserImages();
                Random ran = new Random();
                string filename = "Image" + User.Identity.Name + "_" + ran.Next(1000000) + ".jpg";
                
                currentUserImage.Add(new ImageModel()
                {
                    ImageDescription = description,
                    Content = imageToByteArray(upload),
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

        public byte[] imageToByteArray(HttpPostedFileBase source)
        {
            byte[] destination = new byte[source.ContentLength];
            source.InputStream.Position = 0;
            source.InputStream.Read(destination, 0, source.ContentLength);
            return destination;
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