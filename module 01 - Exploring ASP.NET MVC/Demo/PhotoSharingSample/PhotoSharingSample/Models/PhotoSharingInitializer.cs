using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PhotoSharingApplication.Models;

namespace PhotoSharingApplication.Models
{

    //This class is used during development to make sure that the database get recreated
    public class PhotoSharingInitializer : DropCreateDatabaseAlways<PhotoSharingDB>
    {

        //This method puts sample data into the database
        protected override void Seed(PhotoSharingDB context)
        {
            base.Seed(context);

            //Create some photos
            var photos = new List<Photo>
            {
                new Photo {
                    Title = "Flower",
                    Description = "Cowparsley, photographed in close up.",
                    UserName = "Jeff Hay",
                    PhotoFile = getFileBytes("\\Content\\flower.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreatedDate = DateTime.Today,
                    ModifiedDate = DateTime.Today
                },
                new Photo {
                    Title = "Orchard",
                    Description = "This was taken on a sunny fall day.",
                    UserName = "Julian Isla",
                    PhotoFile = getFileBytes("\\Content\\orchard.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreatedDate = DateTime.Today,
                    ModifiedDate = DateTime.Today
                },
                new Photo {
                    Title = "Blackberries",
                    Description = "This was late for blackberries so they are a little past their best.",
                    UserName = "Godwell Khosa",
                    PhotoFile = getFileBytes("\\Content\\blackberries.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreatedDate = DateTime.Today,
                    ModifiedDate = DateTime.Today
                },
                new Photo {
                    Title = "Ripples",
                    Description = "Interesting reflections and colors in this marine shot.",
                    UserName = "Izak Cohen",
                    PhotoFile = getFileBytes("\\Content\\ripples.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreatedDate = DateTime.Today,
                    ModifiedDate = DateTime.Today
                },
                new Photo {
                    Title = "View Along a Path",
                    Description = "The light was great through the trees so I had to stop and take this.",
                    UserName = "Julian Isla",
                    PhotoFile = getFileBytes("\\Content\\path.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreatedDate = DateTime.Today,
                    ModifiedDate = DateTime.Today
                }
            };
            photos.ForEach(s => context.Photos.Add(s));
            context.SaveChanges();

            //Create some comments
            var comments = new List<Comment>
            {
                new Comment {
                    PhotoID = 2,
                    UserName = "Luis Sousa",
                    Subject = "Camera Settings",
                    Body = "Nice depth-of-field. What aperture did you use?"
                },
                new Comment {
                    PhotoID = 2,
                    UserName = "Brad Sutton",
                    Subject = "Camera Settings",
                    Body = "Must have been f11 or something like that?"
                },
                new Comment {
                    PhotoID = 5,
                    UserName = "Brad Sutton",
                    Subject = "Great Shot!",
                    Body = "I know these things are easy to shoot, but I still think they're great."
                },
                new Comment {
                    PhotoID = 3,
                    UserName = "Masato Kawai",
                    Subject = "Imperfections",
                    Body = "Looks like there's a hair in the lower right. Was that in the shot?"
                }
            };
            comments.ForEach(s => context.Comments.Add(s));
            context.SaveChanges();

        }

        //This gets byte array for a file at the path specified
        //The path is relative to the route of the web site
        //It is used to seed images
        private byte[] getFileBytes(string path)
        {
            FileStream fileOnDisk = new FileStream(HttpRuntime.AppDomainAppPath + path, FileMode.Open);
            byte[] fileBytes;
            using (BinaryReader br = new BinaryReader(fileOnDisk))
            {
                fileBytes = br.ReadBytes((int)fileOnDisk.Length);
            }
            return fileBytes;
        }
    }
}