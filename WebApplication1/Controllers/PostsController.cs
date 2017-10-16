using Accord.MachineLearning;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private BlogContext db = new BlogContext();
        /**
         * Get related post for each post
         * Use k-means cluster algorithm
         */
        public void PostetsRelatedAi()
        {
            // Get all posts from db
            var posts = (from p in db.Posts
                         where p.Content.Length > 50 // Avoid uneccesary outliers
                         select p).ToList();

            // Create Array of all posts content
            string[] documents = (from p in db.Posts
                                  where p.Content.Length > 50 // Avoid uneccesary outliers
                                  select p.Content).ToArray();

            ///Apply TF*IDF on the documents and get the resulting vectors.
            double[][] inputs = TFIDFEX.TFIDF.Transform(documents);
            inputs = TFIDFEX.TFIDF.Normalize(inputs);

            // Create a new K-Means algorithm with Posts/2 clusters (create couples) 
            KMeans kmeans = new KMeans(Convert.ToInt32(posts.Count() / 2));

            // Compute the algorithm, retrieving an integer array
            //  containing the labels for each of the observations
            KMeansClusterCollection clusters = kmeans.Learn(inputs);
            int[] labels = clusters.Decide(inputs);


            // Create more handy list of clusters and their vectors
            var clustersList = new List<List<int>>();
            for (int j = 0; j < Convert.ToInt32(posts.Count() / 2); j++)
            {
                clustersList.Add(labels.Select((s, i) => new { i, s })
                                       .Where(t => t.s == j)
                                       .Select(t => t.i)
                                       .ToList());
            }

            // Adjust all posts and thier related according to clustering results
            foreach (var clusetr in clustersList)
            {
                // Handle clusters with 2 and more vectors
                if (clusetr.Count() >= 2)
                {
                    for (int i = 1; i < clusetr.Count(); i++)
                    {
                        // Attach each post in the cluster to it's neighbor
                        posts[clusetr[i-1]].relatedPost = posts[clusetr[i]].Title;
                    }
                    // Attach the last post to the first one in the list
                    posts[clusetr.Last()].relatedPost = posts[clusetr.First()].Title;

                }
                // Handle clusters with only one vector
                else if(clusetr.Count() > 0)
                {
                    // In case matching not found
                    posts[clusetr.First()].relatedPost = null;
                }

            }

            // Update Changes in DB
            foreach(var p in posts){
                db.Entry(p).State = EntityState.Modified;
            }
            db.SaveChanges();
        }

        // GET: Posts
        public ActionResult Index(string title)
        {
            var posts = from p in db.Posts
                        select p;

            if (!String.IsNullOrEmpty(title))
            {
                posts = posts.Where(f => f.Title.Contains(title));
            }

            return View(posts.ToList());
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
        // Get also stream of image file and video file, IsImage and IsVideo are the frontend checkboxes values
        public ActionResult Create([Bind(Include = "ID,Title,AuthorName,AuthorWebsite,Content")] Post post, HttpPostedFileBase Image, bool IsImage, HttpPostedFileBase Video, bool IsVideo)
        {
            if (ModelState.IsValid)
            {
                // In case image is attached
                if (Image != null && IsImage)
                {
                    // Generate unique name
                    var imgName = DateTime.Now.ToString("yyyyMMddHHmmssfff_") + Image.FileName.Replace(" ", "_");
                    // Save the image file in the server
                    Image.SaveAs(HttpContext.Server.MapPath("/Content/images/") + imgName);
                    // Attach image path to the post
                    post.Image = String.Format("/Content/images/{0}", imgName);
                }

                // In case video is attached
                if (Video != null && IsVideo)
                {
                    // Generate unique name
                    var vidname = DateTime.Now.ToString("yyyyMMddHHmmssfff_") + Video.FileName.Replace(" ", "_");
                    // Save the video file in the server
                    Video.SaveAs(HttpContext.Server.MapPath("/Content/videos/") + vidname);
                    // Attach video path to the post
                    post.Video = String.Format("/Content/videos/{0}", vidname);
                }

                post.PostDate = DateTime.Now;
                db.Posts.Add(post);
                try
                {
                    db.SaveChanges();
                }
                // Handle post title duplication error
                catch (DbUpdateException e)
                {
                    // Duplicated podt title error will include the word unique in it's error massage.
                    if (e.InnerException.InnerException.Message.Contains("unique")){
                        // Add Error the to model state, this will be presented in the view
                        ModelState.AddModelError("", "Operation Failed, Make sure post title in unique.");
                        return View(post);
                    }
                    throw e;
                }
                // Reattach related posts
                PostetsRelatedAi();
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
                // Use old post data values in order to delete image and video files
                db.Posts.Attach(post);
                var OldPost = db.Posts.AsNoTracking().FirstOrDefault(me => me.ID == post.ID);

                if (IsImage) //IsImage Checkbox is checked
                {
                    // Verify image file exist
                    if (System.IO.File.Exists(HttpContext.Server.MapPath(OldPost.Image)))
                    {
                        // Delete image file from server
                        System.IO.File.Delete(HttpContext.Server.MapPath(OldPost.Image));
                    }
                    if (Image != null)
                    {
                        // Generate unique name
                        var imgName = DateTime.Now.ToString("yyyyMMddHHmmssfff_") + Image.FileName.Replace(" ", "_");
                        // Save the image file in the server
                        Image.SaveAs(HttpContext.Server.MapPath("/Content/images/") + imgName);
                        // Attach image path to the post
                        post.Image = String.Format("/Content/images/{0}", imgName);
                    }
                }
                // Stay with old image
                else
                {
                    post.Image = OldPost.Image;
                } 

                if (IsVideo) //IsVideo Checkbox is checked
                {
                    // Verify video file exist
                    if (System.IO.File.Exists(HttpContext.Server.MapPath(OldPost.Video)))
                    {
                        // Delete video file from server
                        System.IO.File.Delete(HttpContext.Server.MapPath(OldPost.Video));
                    }
                    if (Video != null)
                    {
                        // Generate unique name
                        var vidname = DateTime.Now.ToString("yyyyMMddHHmmssfff_") + Video.FileName.Replace(" ", "_");
                        // Save the image file in the server
                        Video.SaveAs(HttpContext.Server.MapPath("/Content/videos/") + vidname);
                        // Attach image path to the post
                        post.Video = String.Format("/Content/videos/{0}", vidname);
                    }
                }
                // Stay with old video
                else
                {
                    post.Video = OldPost.Video;
                }

                post.PostDate = OldPost.PostDate;
                db.Entry(post).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                // Handle post title duplication error
                catch (DbUpdateException e)
                {
                    // Duplicated title error will include the word unique in it's error massage.
                    if (e.InnerException.InnerException.Message.Contains("unique"))
                    {
                        ModelState.AddModelError("", "Operation Failed, Make sure post title in unique.");
                        return View(post);
                    }
                    throw e;
                }

                // Reattach related posts
                PostetsRelatedAi();
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

            // Reattach related posts
            PostetsRelatedAi();
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
