using System.Linq;
using System.Web.Mvc;
using ViVenty.Domain.Abstract;
using ViVenty.Domain.Entities;
using ViVenty.WebUI.Models;

namespace ViVenty.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IViventyRepository repository;

        public CartController(IViventyRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            { Cart = cart, ReturnUrl = returnUrl});
        }

        public RedirectToRouteResult AddToCart(Cart cart, int Id, string returnUrl)
        {
            Hsuit hsuit = repository.Hsuits.FirstOrDefault(h => h.Id == Id);

            if (hsuit != null)
                cart.AddItem(hsuit, 1);

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int Id, string returnUrl)
        {
            Hsuit hsuit = repository.Hsuits.FirstOrDefault(h => h.Id == Id);

            if (hsuit != null)
                cart.RemoveLine(hsuit);

            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

    }
}