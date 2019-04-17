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
        private IOrderProcessor orderProcessor;
        private IEmailService emailService;

        public CartController(IViventyRepository repo, IOrderProcessor processor, IEmailService email)
        {
            repository = repo;
            orderProcessor = processor;
            emailService = email;
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

        public ViewResult Checkout ()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Извините, ваша корзина пуста.");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                ViewBag.Id = orderProcessor.order.Id;

                emailService.SendOrderDetailsToAdmin(cart, orderProcessor.order);
                emailService.SendOrderDetailsToClient(cart, orderProcessor.order);
                cart.Clear();                
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }            
        }

    }
}