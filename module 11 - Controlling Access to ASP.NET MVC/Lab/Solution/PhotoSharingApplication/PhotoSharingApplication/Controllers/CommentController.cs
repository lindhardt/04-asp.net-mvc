using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoSharingApplication.Models;

namespace PhotoSharingApplication.Controllers
{
    public class CommentController : Controller
    {
        private IPhotoSharingContext context;

        //Constructors
        public CommentController()
        {
            context = new PhotoSharingContext();
        }

        public CommentController(IPhotoSharingContext Context)
        {
            context = Context;
        }

        //
        // GET: A Partial View for displaying in the Photo Details view
        [ChildActionOnly] //This attribute means the action cannot be accessed from the browser's address bar
        public PartialViewResult _CommentsForPhoto(int PhotoId)
        {
            //The comments for a particular photo have been requested. Get those comments.
            var comments = from c in context.Comments
                           where c.PhotoID == PhotoId
                           select c;
            //Save the PhotoID in the ViewBag because we'll need it in the view
            ViewBag.PhotoId = PhotoId;
            return PartialView(comments.ToList());
        }

        //
        //POST: This action creates the comment when the AJAX comment create tool is used
        [HttpPost]
        public PartialViewResult _CommentsForPhoto(Comment comment, int PhotoId)
        {

            //Save the new comment
            context.Add<Comment>(comment);
            context.SaveChanges();

            //Get the updated list of comments
            var comments = from c in context.Comments
                           where c.PhotoID == PhotoId
                           select c;
            //Save the PhotoID in the ViewBag because we'll need it in the view
            ViewBag.PhotoId = PhotoId;
            //Return the view with the new list of comments
            return PartialView("_CommentsForPhoto", comments.ToList());
        }

        //
        // GET: /Comment/_Create. A Partial View for displaying the create comment tool as a AJAX partial page update
        [Authorize]
        public PartialViewResult _Create(int PhotoId)
        {
            //Create the new comment
            Comment newComment = new Comment();
            newComment.PhotoID = PhotoId;

            ViewBag.PhotoID = PhotoId;
            return PartialView("_CreateAComment");
        }



        //
        // GET: /Comment/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            Comment comment = context.FindCommentById(id);
            ViewBag.PhotoID = comment.PhotoID;
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        //
        // POST: /Comment/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = context.FindCommentById(id);
            context.Delete<Comment>(comment);
            context.SaveChanges();
            return RedirectToAction("Display", "Photo", new { id = comment.PhotoID });
        }

    }
}
