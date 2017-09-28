using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Center
    {
        public int ID { get; set; }
        public String Country { get; set; }
        public String City { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public String Description { get; set; }
        public String Address { get; set; }
        public String Phone { get; set; }
    }
}