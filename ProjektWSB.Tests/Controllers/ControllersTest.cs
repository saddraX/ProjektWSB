using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjektWSB.Controllers;
using ProjektWSB.Models;
using System.Web.Mvc;
using Microsoft.SqlServer.Server;

namespace ProjektWSB.Tests.Controllers
{
    [TestClass]
    public class ControllersTest : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        [TestMethod]
        public void DishesIndexTest() // wyświetlanie listy wszystkich dań
        {
            var controller = new DishesController();
            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result.ViewData);
        }

        [TestMethod]
        public void DishesDetailsTest() // zwracanie null dla /Dishes/Details
        {
            var controller = new DishesController();
            var result = controller.Details(5) as ViewResult;
            Assert.IsNotNull(result.ViewData);
        }

        [TestMethod]
        public void LoginIndexTest() // wyświetlanie ekranu logowania
        {
            var controller = new UsersController();
            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result.ViewData);
        }

        [TestMethod]
        public void AuthorizeLoginTest() // testowanie poprawności autentyfikacji użytkownika
        {
            var controller = new UsersController();

            User tempUser = new User();
            tempUser.Login = "testtest";
            tempUser.Password = "testtest";
            var result = controller.Create(tempUser) as ViewResult;

            User tempUser2 = new User();
            tempUser2.Login = "testtest";
            tempUser2.Password = "testtest";
            var result2 = controller.Create(tempUser2) as ViewResult;

            Assert.AreEqual(result, result2);
        }
    }
}
