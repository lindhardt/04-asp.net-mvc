using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoSharingApplication.Models;
using System.Drawing;


namespace PhotoSharingApplication.Controllers
{
    public class PhotoController : Controller
    {
        private PhotoSharingDB db = new PhotoSharingDB();

        //
        // GET: /Photo/
        public ActionResult Index()
        {
            return View("Index", db.Photos.ToList());
        }

        //
        // GET: A Partial View for displaying many photos as cards
        [ChildActionOnly] //This attribute means the action cannot be accessed from the brower's address bar
        public ActionResult _PhotoGallery(int number = 0)
        {
            //We want to display only the latest photos when a positive integer is supplied to the view.
            //Otherwise we'll display them all
            List<Photo> photos;

            if (number == 0)
            {
                photos = db.Photos.ToList();
            }
            else
            {
                 photos = (from p in db.Photos
                          orderby p.CreatedDate descending
                          select p).Take(number).ToList();
            }

            return PartialView("_PhotoGallery", photos);
        }

        //
        // GET: This action shows a jQuery-powered slide show that shows all pics in the application
        public ActionResult SlideShow()
        {
            return View(db.Photos.ToList());
        }

        //
        // GET: This actions show the same slideshow view with only the users favorate photos
        public ActionResult FavoritesSlideshow()
        {
            //List<Photo> allPhotos = db.Photos.ToList();
            List<Photo> favPhotos = new List<Photo>();
            List<int> favoriteIds = Session["Favorites"] as List<int>;
            Photo currentPhoto;

            foreach (int favID in favoriteIds)
            {
                currentPhoto = db.Photos.Find(favID);
                if (currentPhoto != null)
                {
                    favPhotos.Add(currentPhoto);
                }
            }

            return View("SlideShow", favPhotos);
        }

        //
        // GET: /Photo/5
        // GET: /Photo/Details/5
        public ActionResult Details(int id = 0)
        {
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View("Details", photo);
        }

        //
        // GET: /Photo/IncompleteAction 
        //This action is to illustrate exception handling
        public ActionResult IncompleteAction()
        {
            throw new NotImplementedException("This Action is not yet complete");
        }

        //
        // GET: /Photo/Create
        [Authorize]
        public ActionResult Create()
        {
            //Create the new photo
            Photo newPhoto = new Photo();
            newPhoto.UserName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(User.Identity.Name);
            newPhoto.CreatedDate = DateTime.Today;
            newPhoto.ModifiedDate = DateTime.Today;
            return View(newPhoto);
        }

        //
        // POST: /Photo/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(Photo photo, HttpPostedFileBase image)
        {
            photo.UserName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(User.Identity.Name);
            photo.CreatedDate = DateTime.Today;
            photo.ModifiedDate = DateTime.Today;
            if (ModelState.IsValid)
            {
                //Is there a photo? If so save it
                if (image != null)
                {
                    photo.ImageMimeType = image.ContentType;
                    photo.PhotoFile = new byte[image.ContentLength];
                    image.InputStream.Read(photo.PhotoFile, 0, image.ContentLength);
                }

                //Add the photo to the database and save it
                db.Photos.Add(photo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(photo);
        }

        //
        // GET: /Photo/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        //
        // POST: /Photo/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Photo photo = db.Photos.Find(id);
            db.Photos.Remove(photo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //This action gets the photo file for a given Photo ID
        public FileContentResult GetImage(int PhotoId)
        {
            //Get the right photo
            Photo requestedPhoto = db.Photos.FirstOrDefault(p => p.PhotoID == PhotoId);
            if (requestedPhoto != null)
            {
                return File(requestedPhoto.PhotoFile, requestedPhoto.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        //This action adds a photo ID to the list of favorites for the current session
        public ContentResult AddFavorite(int PhotoID)
        {
            List<int> favorites = Session["Favorites"] as List<int>;
            if (favorites == null)
            {
                favorites = new List<int>();
            }
            favorites.Add(PhotoID);
            Session["Favorites"] = favorites;
            return Content("The picture has been added to your favorites", "text/plain", System.Text.Encoding.Default);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}