using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ViVenty.Domain.Abstract;
using ViVenty.Domain.Entities;
using ViVenty.WebUI.Controllers;
using ViVenty.WebUI.Models;

namespace ViVenty.UnitTests
{
    [TestClass]
    public class CartTests
    {
        Hsuit hsuit1 = new Hsuit { Id = 1, Name = "Тестовый объект1", Price = 5000 };
        Hsuit hsuit2 = new Hsuit { Id = 2, Name = "Тестовый объект2", Price = 6000 };
        Hsuit hsuit3 = new Hsuit { Id = 3, Name = "Тестовый объект3", Price = 5500 };
        Hsuit hsuit4 = new Hsuit { Id = 4, Name = "Тестовый объект4", Price = 5700 };

        [TestMethod]
        public void Can_Add_New_Lines()
        {

            Cart cart = new Cart();

            cart.AddItem(hsuit1, 1);
            cart.AddItem(hsuit2, 1);

            List<CartLine> results = cart.Lines.ToList();

            Assert.AreEqual(results.Count, 2);
            Assert.AreEqual(results[0].Hsuit, hsuit1);
            Assert.AreEqual(results[1].Hsuit, hsuit2);
        }

        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            Cart cart = new Cart();
            
            cart.AddItem(hsuit1, 1);
            cart.AddItem(hsuit2, 1);
            cart.AddItem(hsuit1, 7);


            List<CartLine> results = cart.Lines.ToList();

            Assert.AreEqual(results.Count, 2);
            Assert.AreEqual(results[0].Quantity, 8);

        }
        [TestMethod]
        public void Can_Remove_Line()
        {
            Cart cart = new Cart();

            cart.AddItem(hsuit1, 1);
            cart.AddItem(hsuit2, 2);
            cart.AddItem(hsuit3, 3);
            cart.AddItem(hsuit4, 4);
            
            cart.RemoveLine(hsuit3);

            List<CartLine> results = cart.Lines.ToList();


            Assert.AreEqual(results.Count, 3);
            Assert.AreEqual(results.Where(h => h.Hsuit == hsuit3).Count(), 0);
            Assert.AreEqual(results[0].Hsuit, hsuit1);
            Assert.AreEqual(results[1].Hsuit, hsuit2);
            Assert.AreEqual(results[2].Hsuit, hsuit4);
        }
        [TestMethod]
        public void Calculate_Total_Price()
        {
            Cart cart = new Cart();

            cart.AddItem(hsuit1, 1);
            cart.AddItem(hsuit2, 2);
            cart.AddItem(hsuit3, 2);
            cart.AddItem(hsuit4, 1);

            int result = cart.ComputeTotalValue();

            Assert.AreEqual(result, 33700);
        }

        [TestMethod]
        public void Can_Clear_Cart()
        {
            Cart cart = new Cart();

            cart.AddItem(hsuit1, 1);
            cart.AddItem(hsuit2, 2);
            cart.AddItem(hsuit3, 3);
            cart.AddItem(hsuit4, 4);

            cart.Clear();

            Assert.AreEqual(cart.Lines.Count(), 0);

        }

        [TestMethod]
        public void Can_Add_To_Cart()
        {
            //Setup - creation of repository imitation (Mock)

            Mock<IViventyRepository> mock = new Mock<IViventyRepository>();
            mock.Setup(h => h.Hsuits).Returns(new List<Hsuit>
            {
                new Hsuit {Id = 1, Name = "Hsuit1", Category = "Cat1" }
            }.AsQueryable);

            //Setup - create of Cart and CartController

            Cart cart = new Cart();
            CartController controller = new CartController(mock.Object);

            //Action - add element to cart

            controller.AddToCart(cart, 1, null);

            //Assert

            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToList()[0].Hsuit.Id, 1);

        }

        [TestMethod]
        public void Adding_Hsuit_To_Cart_Goes_To_Cart_Screen()
        {
            //Setup - Create of repository imitation, Cart, CartController

            Mock<IViventyRepository> repo = new Mock<IViventyRepository>();
            repo.Setup(h => h.Hsuits).Returns(new List<Hsuit>
            {
                new Hsuit {Id = 1, Name = "Hsuit1", Category = "cat1" }
            }.AsQueryable());

            Cart cart = new Cart();

            CartController controller = new CartController(repo.Object);

            // Action - Add object to cart
            RedirectToRouteResult result = controller.AddToCart(cart, 1, "backToShop");

            //Assert
            Assert.AreEqual(result.RouteValues["Action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "backToShop");
        }

        [TestMethod]
        public void Can_View_Cart_Contents()
        {
            //Setup - create Cart and CartController
            Cart cart = new Cart();
            CartController controller = new CartController(null);

            //Action - call of Index method

            CartIndexViewModel result =
                (CartIndexViewModel)controller.Index(cart, "backToShop").ViewData.Model;

            //Assert
            Assert.AreSame(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "backToShop");
        }

    }
}
