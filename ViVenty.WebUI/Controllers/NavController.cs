using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViVenty.Domain.Abstract;

namespace ViVenty.WebUI.Controllers
{
    public class NavController : Controller
    {
        // GET: Nav
        
        private IViventyRepository repository;

        public NavController (IViventyRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult Menu (string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = repository.Hsuits.
                Select(h => h.Category).
                Distinct().
                OrderBy(x => x);
            return PartialView(categories);
        }
    }
}