using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViVenty.Domain.Entities
{
    public class Hsuit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Size { get; set; }
        public int Price { get; set; }
        public string DefaultPhoto { get; set; }
    }
}