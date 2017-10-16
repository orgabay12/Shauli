using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private BlogContext db = new BlogContext();

        // GET: Comments/1
        [AllowAnonymous]
        // The id parameter is PostID related to the comment
        public ActionResult Index(int? id, string author)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Posts");
            }
            var comments = db.Comments.Where(c => c.PostId == id);

            // Filter comments
            if (!String.IsNullOrEmpty(author))
            {
                comments = comments.Where(c => c.AuthorName.Contains(author));
            }

            return View(comments.ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // Create comments is only from the blog itself
        // GET: Comments/Create
        //public ActionResult Create()
        //{
        //    return RedirectToAction("index", "Blog", null);
        //    ViewBag.PostId = new SelectList(db.Posts, "ID", "Title");
        //    return View();
        //}

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        // The PostID parameter is the PostID related to the comment
        public ActionResult Create([Bind(Include = "ID,Title,AuthorName,AuthorEmail,Content")] Comment comment, int PostId)
        {
            if (ModelState.IsValid)
            {
                // Attach comment to it's post
                comment.PostId = PostId;
                // For redirection
                var postTitle = db.Posts.Find(PostId).Title;
                db.Comments.Add(comment);
                db.SaveChanges();
                // Redirect to the post
                return RedirectToAction("Index", "Blog", new {title= postTitle});
            }

            ViewBag.PostId = new SelectList(db.Posts, "ID", "Title", comment.PostId);
            return View(comment);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.PostId = new SelectList(db.Posts, "ID", "Title", comment.PostId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // The PostID parameter is the PostID related to the comment
        public ActionResult Edit([Bind(Include = "ID,Title,AuthorName,AuthorEmail,Content,PostId")] Comment comment, int PostId)
        {
            if (ModelState.IsValid)
            {
                // Attach comment to it's post
                comment.PostId = PostId;
                comment.Post = db.Posts.Find(PostId);
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                // Redirect to post's comments index page
                return RedirectToAction("Index", new {id=comment.PostId });
            }
            ViewBag.PostId = new SelectList(db.Posts, "ID", "Title", comment.PostId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            // Redirect to post's comments index page
            return RedirectToAction("Index", new { id = comment.PostId });
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
