using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using TFIDFEX;
using Accord.MachineLearning;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

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
        public async Task<ActionResult> Init()
        {
            var posts = new List<Post>{
                new Post { Title="Ferrari",
                           AuthorName="Or Gabay",
                           AuthorWebsite="#",
                           PostDate=new DateTime(2016,10,29),
                           Content="Ferrari is passion on wheels, and that passion comes alive in the articles we’ve created. Every significant Ferrari car is profiled, more than 100 models in all, from the very first machines to wear the prancing horse in 1947 to today’s thrilling lineup of V-8 and V-12 coupes and convertibles. Our journey proceeds along the three paths that make up the Ferrari cars legend: the road cars, the sports-racing cars, and the F1 cars. Clothed in graceful bodywork by Enzo Ferrari’s friend, Battista “Pinin” Farina, the early road cars were only slightly tamed versions of his racing cars. Indeed, the very first Ferrari road car, the 166 MM, took part of its name from the Mille Miglia, the famed 1,000-mile Italian road race won by a Ferrari in 1948. The theme continued through such wondrous stallions as the Ferrari 340 America and 375 MM of the early 1950s. These cars could be driven to the track, compete for the checkered flag, and carry their driver to dinner that night. This was the romance of the dual-purpose sports car, an ideal that culminated with the Ferrari 250 GT SWB coupe of 1959. Ferrari 375 MM racecar The Ferrari 375 MM racer of the 1950s wasn't a world apart from Ferrari road cars. After that, the all-out performance demanded by competition and the veneer of civility required by Ferrari’s wealthy non-racing customers sent his road cars along their own route. Certainly, each succeeding decade had its share of ferocious road going Ferraris -- the 365 GTB/4 Daytona in the 1960s followed by the midengine 512 BBi in the ‘70s, F40 in the ‘80s, F50 in the ‘90s, and Enzo in the new millennium. But each period also had its gorgeous grand touring models as well, including the 250 GT Coupe, 330 GTC, and today’s 612 Scaglietti, all of which followed Ferrari’s classic front-engine V-12 format. It’s a cannon of the Ferrari faith that Enzo sold road cars mainly to finance his first love, racing. And in the first half of the company’s 60-year existence, that mostly meant endurance racing. Ferrari’s sports-racing cars were generally recognizable as wilder versions of models customers could buy and they competed in the big glamor events, like the 24 Hours of Le Mans and the Targa Florio. Such Ferraris as the pontoon-fender 250 Testa Rossa and the voluptuous 330 P4 battled Jaguar and later, Ford, for supremacy in this particular crucible of 200-mph machine and high-risk automotive marketing. By the mid 1970s, Formula 1 had taken over as the aristocrat of motor racing, and Ferrari refocused its efforts on this form of open-wheel, single-seat competition. Immortals like Alberto Ascari and Juan Manuel Fangio had driven Ferrari Grand Prix cars in the 1950s. And the distinctive shark-nose Dino 156 F1 made Phil Hill the first American F1 world champion in 1961. But even those classic men and their machines couldn’t match the dominance of Michael Shumacher who, starting in the mid 1990s, led Ferrari to six F1 manufacturer’s titles and captured for himself five F1 world driving championships. You’re invited to learn about all these cars and more, plus the stories of the people who designed, built, and drove them. The Ferrari articles we’ve created are portals to a story of automotive magic unlike any other.",
                           Image="/content/images/Ferrari.jpg",
                           Video="/content/videos/Ferrari.mp4"},
                new Post { Title="Dogs",
                           AuthorName="Hila",
                           AuthorWebsite="#",
                           PostDate=new DateTime(2016,12,15),
                           Content="The domestic dog (Canis lupus familiaris or Canis familiaris)[4] is a member of genus Canis (canines) that forms part of the wolf-like canids,[5] and is the most widely abundant carnivore.[6][7][8] The dog and the extant gray wolf are sister taxa,[9][10][11] with modern wolves not closely related to the wolves that were first domesticated,[10][11] which implies that the direct ancestor of the dog is extinct.[12] The dog was the first domesticated species[11][13] and has been selectively bred over millennia for various behaviors, sensory capabilities, and physical attributes.[14] New research seems to show that the dog's high sociability may be affected by the same genes as in humans. Their long association with humans has led dogs to be uniquely attuned to human behavior[17] and they are able to thrive on a starch-rich diet that would be inadequate for other canid species.[18] Dogs vary widely in shape, size and colours.[19] Dogs perform many roles for people, such as hunting, herding, pulling loads, protection, assisting police and military, companionship and, more recently, aiding handicapped individuals and therapeutic roles. This influence on human society has given them the sobriquet man's best friend.",
                           Image="/Content/images/flower.png",
                           Video="/Content/videos/dogs.mp4"},
                new Post { Title="Soccer",
                           AuthorName="Daniel",
                           AuthorWebsite="#",
                           PostDate=new DateTime(2017,2,12),
                           Content="The Real Madrid star Cristiano Ronaldo was suspended for five matches on Monday by the Spanish soccer federation for shoving a referee who ejected him from a Spanish Super Cup match against Barcelona on Sunday. The ban means Ronaldo will miss a second match against Barcelona on Wednesday, as well as the first four games of the Spanish league season that opens this weekend, unless it is overturned or reduced on appeal. The incident that led to Monday’s punishment occurred in the 82nd minute of Sunday first-leg Super Cup match at Barcelona’s Camp Nou stadium. Ronaldo, running alongside Barcelona defender Samuel Umtiti at the edge of the penalty area, fell into the box after the players collided and appealed to the referee, Ricardo de Burgos Bengoetxea, for a penalty. But De Burgos Bengoetxea deemed the tumble a dive, and awarded Ronaldo a yellow card for simulation. Since it was Ronaldo’s second yellow of the match, De Burgos Bengoetxea then pulled a red card from his pocket and sent off the forward. Ronaldo also was fined 3,805 euros, about $4,500. He and Real Madrid, which was fined about $2,000, have 10 business days to appeal the sanctions. Real Madrid left little doubt about its opinion of the ejection; the match report for Sunday’s game on the club’s website describes Ronaldo in its first sentence as “unfairly sent off.” Ronaldo’s ejection capped a memorable, but brief, performance for the Portuguese star. Still working into game shape after an extended summer vacation, he entered the match as a substitute in the 58th minute, replacing the French striker Karim Benzema, and scored on a curling shot in the 80th. He received his first yellow card after that goal, for removing his shirt to celebrate the score, which gave Real Madrid a late lead. Two minutes later, he was off. Real Madrid won the match by 3-1.",
                           Image="/Content/images/Soccer.jpg",
                           Video="/Content/videos/Soccer.mp4"},
                new Post { Title="Basket Ball",
                           AuthorName="Eilon",
                           AuthorWebsite="#",
                           PostDate=new DateTime(2017,5,22),
                           Content="LeBron Raymone James (/ləˈbrɒn/; born December 30, 1984) is an American professional basketball player for the Cleveland Cavaliers of the National Basketball Association (NBA). James has won three NBA championships, four NBA Most Valuable Player Awards, three NBA Finals MVP Awards, two Olympic gold medals, an NBA scoring title, and the NBA Rookie of the Year Award. He has also been selected to 13 NBA All-Star teams, 13 All-NBA teams, and six All-Defensive teams, is the Cavaliers' all-time leading scorer, and is the NBA career playoff scoring leader. James played high school basketball at St. Vincent–St. Mary High School in his hometown of Akron, Ohio, where he was highly promoted in the national media as a future NBA superstar. After graduating, he was selected by his home team, the Cleveland Cavaliers, as the first overall pick of the 2003 NBA draft. James led Cleveland to the franchise's first Finals appearance in 2007, ultimately losing to the San Antonio Spurs. In 2010, he left the Cavaliers for the Miami Heat in a highly publicized ESPN special titled The Decision. James spent four seasons with the Heat, reaching the Finals all four years and winning back-to-back championships in 2012 and 2013. In 2013, he led Miami on a 27-game winning streak, the third longest in league history. Following his final season with the Heat, James opted out of his contract and returned to the Cavaliers. He led the Cavaliers to three consecutive Finals series between 2015 and 2017, winning his third championship in 2016 to end Cleveland's 52-year professional sports title drought. Off the court, James has accumulated considerable wealth and fame from numerous endorsement contracts. His public life has been the subject of much scrutiny, and he has been ranked as one of America's most influential and popular athletes. He has been featured in books, documentaries, and television commercials. He also hosted the ESPY Awards, Saturday Night Live, and appeared in the 2015 film Trainwreck.",
                           Image="/Content/images/Basket_Ball.jpg",
                           Video="/Content/videos/Basket_Ball.mp4"},
                new Post { Title="Cats",
                           AuthorName="Or",
                           AuthorWebsite="#",
                           PostDate=new DateTime(2017,7,30),
                           Content="The domestic cat[1][5] (Felis silvestris catus or Felis catus) is a small, typically furry, carnivorous mammal. They are often called house cats when kept as indoor pets or simply cats when there is no need to distinguish them from other felids and felines.[6] Cats are often valued by humans for companionship and for their ability to hunt vermin. There are more than 70 cat breeds, though different associations proclaim different numbers according to their standards. Cats are similar in anatomy to the other felids, with a strong flexible body, quick reflexes, sharp retractable claws, and teeth adapted to killing small prey. Cat senses fit a crepuscular and predatory ecological niche. Cats can hear sounds too faint or too high in frequency for human ears, such as those made by mice and other small animals. They can see in near darkness. Like most other mammals, cats have poorer color vision and a better sense of smell than humans. Cats, despite being solitary hunters, are a social species and cat communication includes the use of a variety of vocalizations (mewing, purring, trilling, hissing, growling, and grunting), as well as cat pheromones and types of cat-specific body language.[7] Cats have a high breeding rate.[8] Under controlled breeding, they can be bred and shown as registered pedigree pets, a hobby known as cat fancy. Failure to control the breeding of pet cats by neutering, as well as the abandonment of former household pets, has resulted in large numbers of feral cats worldwide, requiring population control.[9] In certain areas outside cats' native range, this has contributed, along with habitat destruction and other factors, to the extinction of many bird species. Cats have been known to extirpate a bird species within specific regions and may have contributed to the extinction of isolated island populations.[10] Cats are thought to be primarily responsible for the extinction of 33 species of birds, and the presence of feral and free-ranging cats makes some otherwise suitable locations unsuitable for attempted species reintroduction.[11] Since cats were venerated in ancient Egypt, they were commonly believed to have been domesticated there,[12] but there may have been instances of domestication as early as the Neolithic from around 9,500 years ago (7,500 BC).[13] A genetic study in 2007[14] concluded that domestic cats are descended from Near Eastern wildcats, having diverged around 8,000 BC in the Middle East.[12][15] A 2016 study found that leopard cats were undergoing domestication independently in China around 5,500 BC, though this line of partially domesticated cats leaves no trace in the domesticated populations of today.[16][17] A 2017 study confirmed that domestic cats are descendants of those first domesticated by farmers in the Near East around 9,000 years ago.[18][19] As of a 2007 study, cats are the second most popular pet in the US by number of pets owned, behind freshwater fish.[20] In a 2010 study they were ranked the third most popular pet in the UK, after fish and dogs, with around 8 million being owned.[21]",
                           Image="/Content/images/Cats.jpg",
                           Video="/Content/videos/Cats.mp4"},
                new Post { Title="Porsche",
                           AuthorName="Or",
                           AuthorWebsite="#",
                           PostDate=new DateTime(2017,10,26),
                           Content="When I stepped off the S-Bahn at the Neckarpark station in Stuttgart on my way to the Mercedes-Benz Museum, I ran into a gaggle of Idaho high school students who nicely summed up the two automotive museums they had just visited. “Mercedes has more details and has more history,” one of them told me. Porsche, he said, was more about Porsche and its racing heritage. There you have it. My work is done. Stuttgart is home to these two luxury brands, and as a car guy and automotive journalist, I have always wanted to make the pilgrimage there. Germany does not have the sort of geographic center for its auto industry that the United States has in Detroit. But since the companies’ founders got their 19th century starts in Stuttgart, it comes closest. I had heard that the Mercedes-Benz Museum, like the Henry Ford Museum of American Innovation in Dearborn, Mich., offered an exhaustive history of transportation. I also knew that Stuttgart was a must-see for Porschephiles. I got my excuse to visit when my daughter, Marian, moved to Düsseldorf, an easy train ride away. Continue reading the main story RELATED COVERAGE WHEELS German Luxury Car Brands Dominate and Look to Extend Their Lead JULY 2, 2015 German Automakers Step Up to Silicon Valley Challenge FEB. 8, 2017 My first stop was Mercedes, which presents a captivating walk through the birth of the automobile with the kind of historical sheet-metal eye candy that will wow an automotive fanatic. It’s a tale the company is allowed to own because a founder, Karl Benz, is credited with making the first car, and its other founder, Gottlieb Daimler, was not far behind. But before I even entered the museum, I was stunned by the building. It’s a double-helix design by the Dutch architects UNStudio and sits like a round jewel on Mercedes Street. It opened in 2006. Though Henry Ford made the expensive automobile popular with his affordable Model T in the early 20th century, it was Mr. Benz, an engineer and inventor, who got things started in 1885, when he installed the almost-one-horsepower internal combustion engine he invented into a three-wheel buggy.",
                           Image="/Content/images/Porsche.png",
                           Video="/Content/videos/Porsche.mp4"},


            };
            posts.ForEach(s => db.Posts.Add(s));
            db.SaveChanges();

            var post1 = db.Posts.Where(s => s.Title == "Ferrari").FirstOrDefault();
            var post2 = db.Posts.Where(s => s.Title == "Dogs").FirstOrDefault();
            var post3 = db.Posts.Where(s => s.Title == "Soccer").FirstOrDefault();

            var comments = new List<Comment>{
                new Comment {Title="Comment 1",
                             AuthorName="George Washington",
                             AuthorEmail="George23@gmail.com",
                             Content="Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut.",
                             PostId=post1.ID},
                new Comment {Title="Comment 2",
                             AuthorName="Benjamin Franklin",
                             AuthorEmail="example@hotmail.com",
                             Content="Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut.",
                             PostId=post1.ID,},
                new Comment {Title="Comment 3",
                             AuthorName="Barack Obama",
                             AuthorEmail="sendhere@wall.com",
                             Content="Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut.",
                             PostId=post1.ID,},
                new Comment {Title="Comment 4",
                             AuthorName="George Washington",
                             AuthorEmail="hansom22@gmail.com",
                             Content="Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut.",
                             PostId=post2.ID},
                new Comment {Title="Comment 5",
                             AuthorName="Benjamin Franklin",
                             AuthorEmail="Benjamin@yahoo.com",
                             Content="Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut.",
                             PostId=post2.ID,},
                new Comment {Title="Comment 6",
                             AuthorName="Barack Obama",
                             AuthorEmail="harbesh@gmail.com",
                             Content="Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut.",
                             PostId=post2.ID,},

            };
            comments.ForEach(s => db.Comments.Add(s));
            db.SaveChanges();


            var fans = new List<Fan>{
                new Fan{FirstName="Or",
                        LastName="Gabay",
                        Gender="Male",
                        BirthDay=new DateTime(1992,7,26),
                        Country="Italy"},
                new Fan{FirstName="Tal",
                        LastName="Gabay",
                        Gender="Female",
                        BirthDay=new DateTime(1995,6,1),
                        Country="Germany"},
                new Fan{FirstName="Matan",
                        LastName="Gabay",
                        Gender="Male",
                        BirthDay=new DateTime(2000,9,14),
                        Country="Israel"},
            };
            fans.ForEach(s => db.Fans.Add(s));
            db.SaveChanges();

            var centers = new List<Center>{
                new Center{
                    Country="Israel",
                    City="Tel Aviv",
                    Latitude=32.079980,
                    Longitude=34.799967,
                },
                new Center{
                    Country="Italy",
                    City="Roma",
                    Latitude=41.902473,
                    Longitude=12.496355,
                },
                new Center{
                    Country="Spain",
                    City="Madrid",
                    Latitude=40.414504,
                    Longitude=-3.700706,
                },
                new Center{
                    Country="Germany",
                    City="Berlin",
                    Latitude=52.521597,
                    Longitude=13.415151,
                },

            };
            centers.ForEach(s => db.Centers.Add(s));
            db.SaveChanges();

            // Create admin user
            var result = await new AccountController().AdminUser();

            if (!result.Succeeded)
            {
                return new ContentResult { Content = "Error in initialize" };
            }

            return new ContentResult { Content = "OK" };

        }
        public ActionResult Index(string author, string content, string date, string title)
        {
            if (User.Identity.IsAuthenticated)
            {

            }
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

            if (!String.IsNullOrEmpty(title))
            {
                posts = posts.Where(f => f.Title.Contains(title));
            }

            return View(posts.ToList());
        }

        public ActionResult Statistics(bool? publisher, bool? dates)
        {
            /* Get post per user count */
            var postsPerUser = from p in db.Posts
                               group p by p.AuthorName into author
                               select new PostPerUser{ Name = author.Key, Posts = author.Count()};

            /* Get post per month count */
            var postsPerDates = (from p in db.Posts
                                 group p by new { month = p.PostDate.Month, year = p.PostDate.Year } into d
                                 select new { dt = d.Key.month.ToString() +"/" + d.Key.year.ToString(), count = d.Count() }).OrderByDescending(g => g.dt);

            /* Create list of selected dates, 12 month back */
            var selectedDates = new List<String>();
            for (var date = DateTime.Now.AddYears(-1); date <= DateTime.Now; date = date.AddMonths(1))
            {
                selectedDates.Add(date.Month.ToString() + "/" + date.Year.ToString());
            }
            /* Create final json for post per month graph including months with zero posts */
            var DatesCount = from d in selectedDates
                        join p in postsPerDates on d equals p.dt into dd
                        from count in dd.DefaultIfEmpty()
                        select new{Date = d, Count= count == null? 0 : count.count };

            /* For ajax requests */
            if (Request.AcceptTypes.Contains("application/json"))
            {
                if(publisher.GetValueOrDefault())
                    return Json(postsPerUser, JsonRequestBehavior.AllowGet);

                if (dates.GetValueOrDefault())
                    return Json(DatesCount, JsonRequestBehavior.AllowGet);
            }

            ViewBag.postsPerUser = postsPerUser.ToList<PostPerUser>();
            return View();
        }

        public ActionResult About()
        {
            var centers = from c in db.Centers
                        select c;
            // Ajax get request, data for google maps api
            if (Request.AcceptTypes.Contains("application/json"))
            {
                // Get number of Fnas in centers' countries
                var fans = (from c in db.Centers
                            join f in db.Fans on c.Country equals f.Country
                            select (new { f.FirstName, c.City }) into x
                            group x by x.City into city
                            select new { City = city.Key, Fans = city.Count() });
                // Attach the fans number to the object
                foreach(var f in fans)
                {
                    var center = centers.Where(s => s.City == f.City).FirstOrDefault();
                    center.Description = "Fans In Country: " + f.Fans.ToString();
                }
                // Create json 
                var jsoncenters = new List<object>();
                foreach (var center in centers) {
                    jsoncenters.Add(new {lat=center.Latitude, lng=center.Longitude, city=center.City, description=center.Description});
                }
                return Json(jsoncenters, JsonRequestBehavior.AllowGet);
            }
            // For About view (Razor)
            return View(centers.ToList());
        }
    }
}