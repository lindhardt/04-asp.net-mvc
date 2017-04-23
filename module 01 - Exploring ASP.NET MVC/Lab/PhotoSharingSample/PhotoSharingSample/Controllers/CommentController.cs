using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoSharingApplication.Models;

namespace PhotoSharingApplication.Controllers
{

    public class CommentController : Controller
    {
        private PhotoSharingDB db = new PhotoSharingDB();

        //
        // GET: A Partial View for displaying in the Photo Details view
        [ChildActionOnly] //This attribute means the action cannot be accessed from the brower's address bar
        public PartialViewResult _CommentsForPhoto(int PhotoId)
        {
            //The comments for a particular photo have been requested. Get those comments.
            var comments = from c in db.Comments
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
            //The comment comes from the currently authenticated user
            comment.UserName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(User.Identity.Name);

            //Save the new comment
            db.Comments.Add(comment);
            db.SaveChanges();

            //Get the updated list of comments
            var comments = from c in db.Comments
                            where c.PhotoID == PhotoId
                            select c;
            //Save the PhotoID in the ViewBag because we'll need it in the view
            ViewBag.PhotoId = PhotoId;
            //Return the view with the new list of comments
            return PartialView("_CommentsForPhoto", comments.ToList());
        }

        //
        // GET: /Comment/CreateInline. A Partial View for displaying the create comment tool as a AJAX partial page update
        [Authorize]
        public PartialViewResult _Create(int PhotoId)
        {
            //Create the new comment
            Comment newComment = new Comment();
            newComment.PhotoID = PhotoId;
            newComment.UserName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(User.Identity.Name);

            ViewBag.PhotoID = PhotoId;
            return PartialView("_AddAComment");
        }

        //
        // GET: /Comment/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            Comment comment = db.Comments.Find(id);
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
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Details", "Photo", new { id = comment.PhotoID });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}