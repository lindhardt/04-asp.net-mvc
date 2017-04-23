using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PhotoSharingApplication.Models;

namespace PhotoSharingApplication.Controllers
{
    public class PhotoApiController : ApiController
    {
        private PhotoSharingDB db = new PhotoSharingDB();

        public PhotoApiController()
        {
            //Disable proxy creation because this prevents the serialization of JSON responses
            db.Configuration.ProxyCreationEnabled = false;
        }

        [HttpGet]
        public IEnumerable<Photo> GetAllPhotos() 
        {
            return db.Photos.ToList();
        }

        [HttpGet]
        public Photo GetPhotoById(int id)
        {
            Photo selectedPhoto = db.Photos.FirstOrDefault(p => p.PhotoID == id) as Photo;
            if (selectedPhoto == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            return selectedPhoto;
        }

        [HttpGet]
        public IEnumerable<Photo> GetPhotosByUser(string userName)
        {
            List<Photo> usersPhotos = (from p in db.Photos
                                       where p.UserName == userName
                                       select p).ToList();
            if (usersPhotos.Count == 0)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }

            return usersPhotos;
        }

        //To Do: Implement an action download the photo

    }
}
