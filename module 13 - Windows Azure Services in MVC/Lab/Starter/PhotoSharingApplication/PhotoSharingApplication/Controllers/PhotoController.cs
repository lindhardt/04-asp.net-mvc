using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Globalization;
using PhotoSharingApplication.Models;

namespace PhotoSharingApplication.Controllers
{
    [HandleError(View = "Error")]
    [ValueReporter]
    public class PhotoController : Controller
    {
        private IPhotoSharingContext context;

        //Constructors
        public PhotoController()
        {
            context = new PhotoSharingContext();
        }

        public PhotoController(IPhotoSharingContext Context)
        {
            context = Context;
        }

        //
        // GET: /Photo/
        public ActionResult Index()
        {
            return View("Index");
        }

        [ChildActionOnly]
        public ActionResult _PhotoGallery(int number = 0)
        {
            List<Photo> photos;

            if (number == 0)
            {
                photos = context.Photos.ToList();
            }
            else
            {
                photos = (from p in context.Photos
                          orderby p.CreatedDate descending
                          select p).Take(number).ToList();
            }

            return PartialView("_PhotoGallery", photos);
        }

        public ActionResult Display(int id)
        {
            Photo photo = context.FindPhotoById(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View("Display", photo);
        }

        public ActionResult DisplayByTitle(string title)
        {
            Photo photo = context.FindPhotoByTitle(title);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View("Display", photo);
        }

        [Authorize]
        public ActionResult Create()
        {
            Photo newPhoto = new Photo();
            newPhoto.CreatedDate = DateTime.Today;
            return View("Create", newPhoto);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(Photo photo, HttpPostedFileBase image)
        {
            photo.CreatedDate = DateTime.Today;
            if (!ModelState.IsValid)
            {
                return View("Create", photo);
            }
            else
            {
                if (image != null)
                {
                    photo.ImageMimeType = image.ContentType;
                    photo.PhotoFile = new byte[image.ContentLength];
                    image.InputStream.Read(photo.PhotoFile, 0, image.ContentLength);
                }
                context.Add<Photo>(photo);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            Photo photo = context.FindPhotoById(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View("Delete", photo);
        }

        [Authorize]
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Photo photo = context.FindPhotoById(id);
            context.Delete<Photo>(photo);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public FileContentResult GetImage(int id)
        {
            Photo photo = context.FindPhotoById(id);
            if (photo != null)
            {
                return File(photo.PhotoFile, photo.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        public ActionResult SlideShow()
        {
            return View("SlideShow", context.Photos.ToList());
        }

        public ActionResult FavoritesSlideshow()
        {
            List<Photo> favPhotos = new List<Photo>();
            List<int> favoriteIds = Session["Favorites"] as List<int>;
            if (favoriteIds == null)
            {
                favoriteIds = new List<int>();
            }
            Photo currentPhoto;

            foreach (int favID in favoriteIds)
            {
                currentPhoto = context.FindPhotoById(favID);
                if (currentPhoto != null)
                {
                    favPhotos.Add(currentPhoto);
                }
            }

            return View("SlideShow", favPhotos);
        }

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

    }
}
