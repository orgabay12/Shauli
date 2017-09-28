using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PostPerUser
    {
        public string Name;
        public int Posts;
    }
    public class BlogController : Controller
    {
        private BlogContext db = new BlogContext();

        public string init()
        {
            var posts = new List<Post>
            {
                new Post { Title="this is the title of a blog post",
                           AuthorName="mads kjaer",
                           AuthorWebsite="#",
                           PostDate=new DateTime(2009,6,29),
                           Content="lorem ipsum dolor sit amet, tellus eu orci lacus blandit. cras enim nibh, sodales ultricies elementum vel, fermentum id tellus. proin metus odio, ultricies eu pharetra dictum, laoreet id odio. curabitur in odio augue. morbi congue auctor interdum. phasellus sit amet metus justo. phasellus vitae tellus orci, at elementum ipsum. in in quam eget diam adipiscing condimentum. maecenas gravida diam vitae nisi convallis vulputate quis sit amet nibh. nullam ut velit tortor. curabitur ut elit id nisl volutpat consectetur ac ac lorem. quisque non elit et elit lacinia lobortis nec a velit. sed ac nisl sed enim consequat porttitor. pellentesque ut sapien arcu, egestas mollis massa. fusce lectus leo, fringilla at aliquet sit amet, volutpat non erat. aenean molestie nibh vitae turpis venenatis vel accumsan nunc tincidunt. suspendisse id purus vel felis auctor mattis non ac erat. pellentesque sodales venenatis condimentum. aliquam sit amet nibh nisi, ut pulvinar est. sed ullamcorper nisi vel tortor volutpat eleifend. vestibulum iaculis ullamcorper diam consectetur sagittis. quisque vitae mauris ut elit semper condimentum varius nec nisl. nulla tempus porttitor dolor ac eleifend. proin vitae purus lectus, a hendrerit ipsum. aliquam mattis lacinia risus in blandit. vivamus vitae nulla dolor. suspendisse eu lacinia justo. vestibulum a felis ante, non aliquam leo. aliquam eleifend, est viverra semper luctus, metus purus commodo elit, a elementum nisi lectus vel magna. praesent faucibus leo sit amet arcu vehicula sed facilisis eros aliquet. etiam sodales, enim sit amet mollis vestibulum, ipsum sapien accumsan lectus, eget ultricies arcu velit ut diam. aenean fermentum luctus nulla, eu malesuada urna consequat in. pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. pellentesque massa lacus, sodales id facilisis ac, lobortis sed arcu. donec hendrerit fringilla ligula, ac rutrum nulla bibendum id. cras sapien ligula, tincidunt eget euismod nec, ultricies eu augue. nulla vitae velit sollicitudin magna mattis eleifend. nam enim justo, vulputate vitae pretium ac, rutrum at turpis. aenean id magna neque. sed rhoncus aliquet viverra",
                           Image="/content/images/flower.png",
                           Video="/content/images/shauli.mp4"},
                new Post { Title="My Post",
                           AuthorName="Or Gabay",
                           AuthorWebsite="#",
                           PostDate=new DateTime(2010,11,29),
                           Content="Lorem ipsum dolor sit amet, tellus eu orci lacus blandit. Cras enim nibh, sodales ultricies elementum vel, fermentum id tellus. Proin metus odio, ultricies eu pharetra dictum, laoreet id odio. Curabitur in odio augue. Morbi congue auctor interdum. Phasellus sit amet metus justo. Phasellus vitae tellus orci, at elementum ipsum. In in quam eget diam adipiscing condimentum. Maecenas gravida diam vitae nisi convallis vulputate quis sit amet nibh. Nullam ut velit tortor. Curabitur ut elit id nisl volutpat consectetur ac ac lorem. Quisque non elit et elit lacinia lobortis nec a velit. Sed ac nisl sed enim consequat porttitor. Pellentesque ut sapien arcu, egestas mollis massa. Fusce lectus leo, fringilla at aliquet sit amet, volutpat non erat. Aenean molestie nibh vitae turpis venenatis vel accumsan nunc tincidunt. Suspendisse id purus vel felis auctor mattis non ac erat. Pellentesque sodales venenatis condimentum. Aliquam sit amet nibh nisi, ut pulvinar est. Sed ullamcorper nisi vel tortor volutpat eleifend. Vestibulum iaculis ullamcorper diam consectetur sagittis. Quisque vitae mauris ut elit semper condimentum varius nec nisl. Nulla tempus porttitor dolor ac eleifend. Proin vitae purus lectus, a hendrerit ipsum. Aliquam mattis lacinia risus in blandit. Vivamus vitae nulla dolor. Suspendisse eu lacinia justo. Vestibulum a felis ante, non aliquam leo. Aliquam eleifend, est viverra semper luctus, metus purus commodo elit, a elementum nisi lectus vel magna. Praesent faucibus leo sit amet arcu vehicula sed facilisis eros aliquet. Etiam sodales, enim sit amet mollis vestibulum, ipsum sapien accumsan lectus, eget ultricies arcu velit ut diam. Aenean fermentum luctus nulla, eu malesuada urna consequat in. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Pellentesque massa lacus, sodales id facilisis ac, lobortis sed arcu. Donec hendrerit fringilla ligula, ac rutrum nulla bibendum id. Cras sapien ligula, tincidunt eget euismod nec, ultricies eu augue. Nulla vitae velit sollicitudin magna mattis eleifend. Nam enim justo, vulputate vitae pretium ac, rutrum at turpis. Aenean id magna neque. Sed rhoncus aliquet viverra",
                           Image="/Content/images/flower.png",
                           Video="/Content/images/shauli.mp4"},


            };
            posts.ForEach(s => db.Posts.Add(s));
            db.SaveChanges();

            var post1 = db.Posts.Where(s => s.Title == "this is the title of a blog post").FirstOrDefault();
            var post2 = db.Posts.Where(s => s.Title == "My Post").FirstOrDefault();

            var comments = new List<Comment>
            {
                new Comment {Title="Comment 1",
                             AuthorName="George Washington",
                             AuthorEmail="#",
                             Content="Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut.",
                             PostId=post1.ID},
                new Comment {Title="Comment 2",
                             AuthorName="Benjamin Franklin",
                             AuthorEmail="#",
                             Content="Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut.",
                             PostId=post1.ID,},
                new Comment {Title="Comment 3",
                             AuthorName="Barack Obama",
                             AuthorEmail="#",
                             Content="Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut.",
                             PostId=post1.ID,},
                new Comment {Title="Comment 4",
                             AuthorName="George Washington",
                             AuthorEmail="#",
                             Content="Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut.",
                             PostId=post2.ID},
                new Comment {Title="Comment 5",
                             AuthorName="Benjamin Franklin",
                             AuthorEmail="#",
                             Content="Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut.",
                             PostId=post2.ID,},
                new Comment {Title="Comment 6",
                             AuthorName="Barack Obama",
                             AuthorEmail="#",
                             Content="Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut.",
                             PostId=post2.ID,},

            };
            comments.ForEach(s => db.Comments.Add(s));
            db.SaveChanges();


            var fans = new List<Fan>
            {
                new Fan{FirstName="Or",
                        LastName="Gabay",
                        Gender="Male",
                        BirthDay=new DateTime(1992,7,26),
                        Seniority=4},
                new Fan{FirstName="Tal",
                        LastName="Gabay",
                        Gender="Female",
                        BirthDay=new DateTime(1995,6,1),
                        Seniority=4},
                new Fan{FirstName="Matan",
                        LastName="Gabay",
                        Gender="Male",
                        BirthDay=new DateTime(2000,9,14),
                        Seniority=4},
            };
            fans.ForEach(s => db.Fans.Add(s));
            db.SaveChanges();
            return "OK!";

        }
        public ActionResult Index(string author, string content, string date)
        {
            var posts = from p in db.Posts
                        select p;

            if (!String.IsNullOrEmpty(author))
            {
                posts = posts.Where(f => f.AuthorName.Contains(author));
            }

            if (!String.IsNullOrEmpty(content))
            {
                posts = posts.Where(f => f.Content.Contains(content));
            }

            if (!String.IsNullOrEmpty(date))
            {
                var dt = Convert.ToDateTime(date);
                posts = posts.Where(f => f.PostDate == dt);

            }

            return View(posts.ToList());
        }

        public ActionResult Statistics()
        {
            var postsPerUser = from p in db.Posts
                               group p by p.AuthorName into author
                               select new PostPerUser{ Name = author.Key, Posts = author.Count()};
            ViewBag.postsPerUser = postsPerUser.ToList<PostPerUser>();
            foreach(var person in ViewBag.postsPerUser)
            {
                Console.WriteLine(person.Name);
            }
            return View();
        }

        public ActionResult About()
        {

            return View();
        }
    }
}