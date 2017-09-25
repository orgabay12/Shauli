using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public String Title { get; set; }
        public String AuthorName { get; set; }
        public String AuthorWebsite { get; set; }
        [DataType(DataType.MultilineText)]
        public String Content { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set;}
    
    }
}

