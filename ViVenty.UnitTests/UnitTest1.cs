using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Mvc;
using ViVenty.Domain.Entities;
using ViVenty.Domain.Abstract;
using ViVenty.WebUI.Controllers;
using ViVenty.WebUI.HtmlHelpers;
using ViVenty.WebUI.Models;
using System.Collections.Generic;
using System.Linq;

namespace ViVenty.UnitTests
{
    [TestClass]
    public class UnitTest1
    {

        Mock<IViventyRepository> mock = new Mock<IViventyRepository>();
        [TestMethod]
        public void Can_Paginate()
        {
            //Setup
            mock.Setup(m => m.Hsuits).Returns(new List<Hsuit>
            {
                new Hsuit { Id = 1, Name = "Тестовый объект1"},
                new Hsuit { Id = 2, Name = "Тестовый объект2"},
                new Hsuit { Id = 3, Name = "Тестовый объект3"},
                new Hsuit { Id = 4, Name = "Тестовый объект4"},
                new Hsuit { Id = 5, Name = "Тестовый объект5"},
                new Hsuit { Id = 6, Name = "Тестовый объект6"},
                new Hsuit { Id = 7, Name = "Тестовый объект7"},
            });

            HsuitController controller = new HsuitController(mock.Object);
            controller.pageSize = 3;

            //Action

            HsuitListViewModel result = (HsuitListViewModel)controller.List(null, 2).Model;

            //Assert
            List<Hsuit> hsuits = result.Hsuits.ToList();
            Assert.IsTrue(hsuits.Count == 3);
            Assert.AreEqual(hsuits[0].Name, "Тестовый объект4");
            Assert.AreEqual(hsuits[1].Name, "Тестовый объект5");
            Assert.AreEqual(hsuits[2].Name, "Тестовый объект6");
        }

        [TestMethod]
        public void Can_Generate_PageLinks()
        {
            // Setup - definition of secondary method HTML - it need to use extension method then
            HtmlHelper myHelper = null;

            // Setup - create a PagingInfo obect
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            //Setup - make a delegate with lambda expression
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Action
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Statement
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());

        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            //Arrange
            mock.Setup(m => m.Hsuits).Returns(new List<Hsuit>
            {
                new Hsuit { Id = 1, Name = "Тестовый объект1"},
                new Hsuit { Id = 2, Name = "Тестовый объект2"},
                new Hsuit { Id = 3, Name = "Тестовый объект3"},
                new Hsuit { Id = 4, Name = "Тестовый объект4"},
                new Hsuit { Id = 5, Name = "Тестовый объект5"},
                new Hsuit { Id = 6, Name = "Тестовый объект6"},
                new Hsuit { Id = 7, Name = "Тестовый объект7"},

            });

            HsuitController controller = new HsuitController(mock.Object);
            controller.pageSize = 3;

            //Act
            HsuitListViewModel result = (HsuitListViewModel)controller.List(null,2).Model;

            //Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 7);
            Assert.AreEqual(pageInfo.TotalPages, 3);

        }

        [TestMethod]
        public void Can_Filter_Hsuits()
        {
            //Arrange
            mock.Setup(m => m.Hsuits).Returns(new List<Hsuit>
            {
                new Hsuit { Id = 1, Name = "Тестовый объект1", Category = "Cat1"},
                new Hsuit { Id = 2, Name = "Тестовый объект2", Category = "Cat1"},
                new Hsuit { Id = 3, Name = "Тестовый объект3", Category = "Cat2"},
                new Hsuit { Id = 4, Name = "Тестовый объект4", Category = "Cat1"},
                new Hsuit { Id = 5, Name = "Тестовый объект5", Category = "Cat2"}
            });

            HsuitController controller = new HsuitController(mock.Object);
            controller.pageSize = 3;

            //Action
            List<Hsuit> result = ((HsuitListViewModel)controller.List("Cat2", 1).Model).
                Hsuits.ToList();

            //Assert
            Assert.AreEqual(result.Count, 2);
            Assert.IsTrue(result[0].Name == "Тестовый объект3" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].Name == "Тестовый объект5" && result[1].Category == "Cat2");
        }

        [TestMethod]
        public void Can_Create_Categories()
        {
            mock.Setup(m => m.Hsuits).Returns(new List<Hsuit>
            {
                new Hsuit { Id = 1, Name = "Тестовый объект1", Category = "Cat1"},
                new Hsuit { Id = 2, Name = "Тестовый объект2", Category = "Cat1"},
                new Hsuit { Id = 3, Name = "Тестовый объект3", Category = "Cat2"},
                new Hsuit { Id = 4, Name = "Тестовый объект4", Category = "Cat1"},
                new Hsuit { Id = 5, Name = "Тестовый объект5", Category = "Cat3"}

            });

            NavController controller = new NavController(mock.Object);
            List<string> result = ((IEnumerable<string>)controller.Menu().Model).ToList();

            Assert.AreEqual(result.Count, 3);
            Assert.AreEqual(result[0], "Cat1");
            Assert.AreEqual(result[1], "Cat2");
            Assert.AreEqual(result[2], "Cat3");
        }
        [TestMethod]
        public void Indicate_Selected_Category()
        {
            mock.Setup(h => h.Hsuits).Returns(new Hsuit[]
            {
                new Hsuit { Id = 1, Name = "Тестовый объект1", Category = "Cat1"},
                new Hsuit { Id = 2, Name = "Тестовый объект2", Category = "Cat2"},
            });

            NavController controller = new NavController(mock.Object);

            string categoryToSelect = "Cat2";

            string result = controller.Menu(categoryToSelect).ViewBag.SelectedCategory;

            Assert.AreEqual(categoryToSelect, result);
        }

        [TestMethod]
        public void Generate_Category_Specific_Hsuit_Count()
        {
            mock.Setup(m => m.Hsuits).Returns(new List<Hsuit>
            {
                new Hsuit { Id = 1, Name = "Тестовый объект1", Category = "Cat1"},
                new Hsuit { Id = 2, Name = "Тестовый объект2", Category = "Cat1"},
                new Hsuit { Id = 3, Name = "Тестовый объект3", Category = "Cat2"},
                new Hsuit { Id = 4, Name = "Тестовый объект4", Category = "Cat1"},
                new Hsuit { Id = 5, Name = "Тестовый объект5", Category = "Cat3"},
                new Hsuit { Id = 6, Name = "Тестовый объект6", Category = "Cat2"}

            });

            HsuitController controller = new HsuitController(mock.Object);
            controller.pageSize = 3;

            int result1 = ((HsuitListViewModel)(controller.List("Cat1")).Model).PagingInfo.TotalItems;
            int result2 = ((HsuitListViewModel)(controller.List("Cat2")).Model).PagingInfo.TotalItems;
            int result3 = ((HsuitListViewModel)(controller.List("Cat3")).Model).PagingInfo.TotalItems;
            int resultAll = ((HsuitListViewModel)(controller.List(null)).Model).PagingInfo.TotalItems;

            Assert.AreEqual(result1, 3);
            Assert.AreEqual(result2, 2);
            Assert.AreEqual(result3, 1);
            Assert.AreEqual(resultAll, 6);
        }
    }
}
