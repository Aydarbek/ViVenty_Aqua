using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ViVenty.Domain.Entities;

namespace ViVenty.WebUI.Models
{
    public class HsuitDetailsModel
    {
        public Hsuit Hsuit { get; set; }
        public IEnumerable<Photo> Photos { get; set; }
        public Photo p_0 { get; set; }
        public Photo p_1 { get; set; }
        public Photo MainPhoto { get; set; }

    }
}