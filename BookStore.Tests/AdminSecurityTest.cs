using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookStore.UI.Infrastructure.Abstract;
using Moq;
using BookStore.UI.Models;
using BookStore.UI.Controllers;
using System.Web.Mvc;

namespace BookStore.Tests
{
    [TestClass]
    public class AdminSecurityTest
    {
        [TestMethod]
        public void Can_Login_with_Valid_Credentials()
        {
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "123")).Returns(true);

            LoginViewModel model = new LoginViewModel
            {
                username = "admin",
                password = "123"
            };

            AccountController controller = new AccountController(mock.Object);
            ActionResult result = controller.Login(model, "myUrl");

            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual(((RedirectResult)result).Url, "myUrl");

        }
        [TestMethod]
        public void Can_Login_with_InValid_Credentials()
        {
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("abcde", "456")).Returns(true);

            LoginViewModel model = new LoginViewModel
            {
                username = "abcde",
                password = "456"
            };

            AccountController controller = new AccountController(mock.Object);
            ActionResult result = controller.Login(model, "myUrl");
             
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);

        }
    }
}
