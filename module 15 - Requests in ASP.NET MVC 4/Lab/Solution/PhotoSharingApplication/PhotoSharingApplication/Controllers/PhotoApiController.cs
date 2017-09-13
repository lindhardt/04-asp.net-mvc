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

        private IPhotoSharingContext context = new PhotoSharingContext();

        public IEnumerable<Photo> GetAllPhotos()
        {
            return context.Photos.AsEnumerable();
        }

        public Photo GetPhotoById(int id)
        {
            Photo photo = context.FindPhotoById(id);
            if (photo == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return photo;
        }

        public Photo GetPhotoByTitle(string title)
        {
            Photo photo = context.FindPhotoByTitle(title);
            if (photo == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return photo;
        }
    }
}
