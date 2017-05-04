using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OperasWebSite.Models;

namespace OperasWebSite.Controllers
{
    public class ReviewController : Controller
    {
        OperasDB context = new OperasDB();

        //
        // GET: /Review/
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly] //This attribute means the action cannot be accessed from the browser's address bar
        public PartialViewResult _ReviewsForOpera(int OperaId)
        {
            IEnumerable<Review> reviews = from r in context.Reviews
                           where r.OperaID == OperaId
                           select r;
            ViewBag.OperaId = OperaId;
            return PartialView(reviews.ToList());
        }

        [HttpPost]
        public PartialViewResult _ReviewsForOpera(Review review, int OperaId)
        {

            context.Reviews.Add(review);
            context.SaveChanges();
            var reviews = from r in context.Reviews
                           where r.OperaID == OperaId
                           select r;
            ViewBag.OperaId = OperaId;
            return PartialView("_ReviewsForOpera", reviews.ToList());
        }

        public PartialViewResult _Create(int OperaId)
        {
            Review newReview = new Review();
            newReview.OperaID = OperaId;
            ViewBag.OperaID = OperaId;
            return PartialView("_CreateAReview");
        }


    }
}
