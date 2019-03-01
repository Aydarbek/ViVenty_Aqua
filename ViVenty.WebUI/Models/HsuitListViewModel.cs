using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ViVenty.Domain.Entities;

namespace ViVenty.WebUI.Models
{
    public class HsuitListViewModel
    {
        public IEnumerable<Hsuit> Hsuits { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}