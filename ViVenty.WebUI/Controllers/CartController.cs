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
    public class CartController : Controller
    {
        private IViventyRepository repository;

        public CartController(IViventyRepository repo)
        {
            repository = repo;
        }

        public RedirectToRouteResult AddToCart(int HsuitId, string returnUrl)
        {
            Hsuit hsuit = repository.Hsuits.FirstOrDefault(h => h.Id == HsuitId);

            if (hsuit != null)
                GetCart().AddItem(hsuit, 1);

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int HsuitId, string returnUrl)
        {
            Hsuit hsuit = repository.Hsuits.FirstOrDefault(h => h.Id == HsuitId);

            if (hsuit != null)
                GetCart().RemoveLine(hsuit);

            return RedirectToAction("Index", new { returnUrl });
        }

        public Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
    }
}