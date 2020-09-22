using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViVenty.Domain.Abstract;
using ViVenty.Domain.Concrete;
using ViVenty.Domain.Entities;

namespace ViVenty.WebUI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }        
    }
 }
