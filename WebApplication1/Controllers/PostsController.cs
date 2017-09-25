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
    public class PostsController : Controller
    {
        private BlogContext db = new BlogContext();

        // GET: Posts
        public ActionResult Index()
        {
            return View(db.Posts.ToList());
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,AuthorName,AuthorWebsite,Content")] Post post, HttpPostedFileBase Image, bool IsImage, HttpPostedFileBase Video, bool IsVideo)
        {
            if (ModelState.IsValid)
            {
                if (Image != null && IsImage)
                {
                    var imgName = DateTime.Now.ToString("yyyyMMddHHmmssfff_") + Image.FileName.Replace(" ", "_");
                    Image.SaveAs(HttpContext.Server.MapPath("/Content/images/") + imgName);
                    post.Image = String.Format("/Content/images/{0}", imgName);
                }

                if (Video != null && IsVideo)
                {
                    var vidname = DateTime.Now.ToString("yyyyMMddHHmmssfff_") + Video.FileName.Replace(" ", "_");
                    Video.SaveAs(HttpContext.Server.MapPath("/Content/videos/") + vidname);
                    post.Video = String.Format("/Content/videos/{0}", vidname);
                }

                post.PostDate = DateTime.Now;
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,AuthorName,AuthorWebsite,Content")] Post post, HttpPostedFileBase Image, bool IsImage, HttpPostedFileBase Video, bool IsVideo)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Attach(post);
                var OldPost = db.Posts.AsNoTracking().FirstOrDefault(me => me.ID == post.ID);

                if (IsImage) //IsImage Checkbox is checked
                {
                    if (System.IO.File.Exists(HttpContext.Server.MapPath(OldPost.Image)))
                    {
                        System.IO.File.Delete(HttpContext.Server.MapPath(OldPost.Image));
                    }
                    if (Image != null)
                    {
                        var imgName = DateTime.Now.ToString("yyyyMMddHHmmssfff_") + Image.FileName.Replace(" ", "_");
                        Image.SaveAs(HttpContext.Server.MapPath("/Content/images/") + imgName);
                        post.Image = String.Format("/Content/images/{0}", imgName);
                    }
                }
                else{ post.Image = OldPost.Image; } //Stay with old image

                if (IsVideo)
                {
                    if (System.IO.File.Exists(HttpContext.Server.MapPath(OldPost.Video)))
                    {
                        System.IO.File.Delete(HttpContext.Server.MapPath(OldPost.Video));
                    }
                    if (Video != null)
                    {
                        var vidname = DateTime.Now.ToString("yyyyMMddHHmmssfff_") + Video.FileName.Replace(" ", "_");
                        Video.SaveAs(HttpContext.Server.MapPath("/Content/videos/") + vidname);
                        post.Video = String.Format("/Content/videos/{0}", vidname);
                    }
                }
                else { post.Video = OldPost.Video; }

                post.PostDate = OldPost.PostDate;
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            /* Delete post releated media*/
            if (System.IO.File.Exists(HttpContext.Server.MapPath(post.Image)))
            {
                System.IO.File.Delete(HttpContext.Server.MapPath(post.Image));
            }
            if (System.IO.File.Exists(HttpContext.Server.MapPath(post.Video)))
            {
                System.IO.File.Delete(HttpContext.Server.MapPath(post.Video));
            }
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
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
