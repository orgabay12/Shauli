using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Post
    {
        public int ID { get; set; }
        [Index(IsUnique = true)]
        [MaxLength(255)]
        public String Title { get; set; }
        public String AuthorName { get; set; }
        public String AuthorWebsite { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PostDate { get; set; }
        [DataType(DataType.MultilineText)]
        public String Content { get; set; }
        public String Image { get; set; }
        public String Video { get; set; }
        public String relatedPost { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }

}