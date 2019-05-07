using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookStore.Domain.Entities;
using System.Linq;
using Moq;
using BookStore.Domain.Abstract;
using BookStore.UI.Controllers;
using System.Web.Mvc;
using BookStore.UI.Models;

namespace BookStore.Tests
{
    [TestClass]
    public class CartTest
    {
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            Book book1 = new Book { BookId = 1, Author = "Auth1", Category = "Drama", Description = "Desc", Price = 23.3m, Title = "Book1" };
            Book book2 = new Book { BookId = 2, Author = "Auth2", Category = "Drama", Description = "Desc", Price = 10.2m, Title = "Book2" };

            Cart target = new Cart();
            target.AddItem(book1);
            target.AddItem(book2, 3);

            CartLine[] result = target.CartLines.ToArray();

            Assert.AreEqual(result[0].Book, book1);
            Assert.AreEqual(result[1].Book, book2);
        }
        [TestMethod]
        public void Can_Add_Quentity_for_Exesting()
        {
            Book book1 = new Book { BookId = 1, Author = "Auth1", Category = "Drama", Description = "Desc", Price = 23.3m, Title = "Book1" };
            Book book2 = new Book { BookId = 2, Author = "Auth2", Category = "Drama", Description = "Desc", Price = 10.2m, Title = "Book2" };

            Cart target = new Cart();
            target.AddItem(book1);
            target.AddItem(book2, 3);
            target.AddItem(book1, 5);

            CartLine[] result = target.CartLines.ToArray();

            Assert.AreEqual(result.Length, 2);
            Assert.AreEqual(result[0].Quantity, 6);
            Assert.AreEqual(result[1].Quantity, 3);

        }
        [TestMethod]
        public void Can_Remove_Line()
        {
            Book book1 = new Book { BookId = 1, Author = "Auth1", Category = "Drama", Description = "Desc", Price = 23.3m, Title = "Book1" };
            Book book2 = new Book { BookId = 2, Author = "Auth2", Category = "Drama", Description = "Desc", Price = 10.2m, Title = "Book2" };
            Book book3 = new Book { BookId = 3, Author = "Auth3", Category = "Drama", Description = "Desc", Price = 11.8m, Title = "Book3" };


            Cart target = new Cart();
            target.AddItem(book1);
            target.AddItem(book2, 3);
            target.AddItem(book3, 5);
            target.AddItem(book2, 1);
            target.RemoveItem(book2);


            Assert.AreEqual(target.CartLines.Where(b => b.Book == book2).Count(), 0);
            Assert.AreEqual(target.CartLines.Count(), 2);

        }

        [TestMethod]
        public void Calculate_Cart_total()
        {
            Book book1 = new Book { BookId = 1, Author = "Auth1", Category = "Drama", Description = "Desc", Price = 23.1m, Title = "Book1" };
            Book book2 = new Book { BookId = 2, Author = "Auth2", Category = "Drama", Description = "Desc", Price = 10.2m, Title = "Book2" };
            Book book3 = new Book { BookId = 3, Author = "Auth3", Category = "Drama", Description = "Desc", Price = 11.8m, Title = "Book3" };


            Cart target = new Cart();
            target.AddItem(book1);
            target.AddItem(book2, 3);
            decimal result = target.ComputeTotalValue();


            Assert.AreEqual(result, 54m);
        }
        [TestMethod]
        public void Calculate_Clear_Content()
        {
            Book book1 = new Book { BookId = 1, Author = "Auth1", Category = "Drama", Description = "Desc", Price = 23.1m, Title = "Book1" };
            Book book2 = new Book { BookId = 2, Author = "Auth2", Category = "Drama", Description = "Desc", Price = 10.2m, Title = "Book2" };
            Book book3 = new Book { BookId = 3, Author = "Auth3", Category = "Drama", Description = "Desc", Price = 11.8m, Title = "Book3" };


            Cart target = new Cart();
            target.AddItem(book1);
            target.AddItem(book2, 3);
            target.Clear();

            Assert.AreEqual(target.CartLines.Count(), 0);
        }
        [TestMethod]
        public void Can_Add_toCart()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(b => b.Books).Returns(
                new Book[]
                {
                    new Book { BookId = 1, Author = "Auth1", Category = "Drama", Description = "Desc", Price = 23.3m, Title = "Book1" },
                    new Book { BookId = 2, Author = "Auth2", Category = "Drama", Description = "Desc", Price = 10.2m, Title = "Book2" },
                    new Book { BookId = 3, Author = "Auth3", Category = "Drama", Description = "Desc", Price = 11.8m, Title = "Book3" },
                    new Book { BookId = 4, Author = "Auth4", Category = "Drama", Description = "Desc", Price = 78.2m, Title = "Book4" },
                    new Book { BookId = 5, Author = "Auth5", Category = "Drama", Description = "Desc", Price = 15.0m, Title = "Book5" },
                }.AsQueryable()
                );
            Cart cart = new Cart();
            CartController controller = new CartController(mock.Object, null);

            controller.AddToCart(cart, 1, null);
            //RedirectToRouteResult result = controller.AddToCart(cart, 2, "myUrl");

            Assert.AreEqual(cart.CartLines.Count(), 1);
            Assert.AreEqual(cart.CartLines.ToArray()[0].Book.Author, "Auth1");
            //Assert.AreEqual(result.RouteValues["action"], "Index");
            //Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        }

        [TestMethod]
        public void Redirect_From_Add_toCart()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(b => b.Books).Returns(
                new Book[]
                {
                    new Book { BookId = 1, Author = "Auth1", Category = "Drama", Description = "Desc", Price = 23.3m, Title = "Book1" },
                    new Book { BookId = 2, Author = "Auth2", Category = "Drama", Description = "Desc", Price = 10.2m, Title = "Book2" },
                    new Book { BookId = 3, Author = "Auth3", Category = "Drama", Description = "Desc", Price = 11.8m, Title = "Book3" },
                    new Book { BookId = 4, Author = "Auth4", Category = "Drama", Description = "Desc", Price = 78.2m, Title = "Book4" },
                    new Book { BookId = 5, Author = "Auth5", Category = "Drama", Description = "Desc", Price = 15.0m, Title = "Book5" },
                }.AsQueryable()
                );
            Cart cart = new Cart();
            CartController controller = new CartController(mock.Object, null);

            RedirectToRouteResult result = controller.AddToCart(cart, 2, "myUrl");

            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        }
        [TestMethod]
        public void Can_View_Cart_content()
        {
            Cart cart = new Cart();
            CartController controller = new CartController(null, null);

            //CartIndexViewModel result = (CartIndexViewModel) controller.Index(cart, "myUrl").ViewData.Model;

            //Assert.AreEqual(result.cart, cart);
            //Assert.AreEqual(result.returnUrl, "myUrl");


        }
        [TestMethod]
        public void Cannot_checkout_empty_cart()
        {
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            ShippingDetails shippingDetails = new ShippingDetails();
            CartController controller = new CartController(null, mock.Object);

            ViewResult result = controller.Checkout(cart, shippingDetails);

            //mock.Verify(m => m.ProcessOrder(cart, shippingDetails));
            Assert.AreEqual(result.ViewName, "");
            Assert.AreEqual(result.ViewData.ModelState.IsValid, false);

        }
        [TestMethod]
        public void Cannot_checkout_invalid_shippingDetails()
        {
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            cart.AddItem(new Book(), 1);
             
            ShippingDetails shippingDetails = new ShippingDetails();
            CartController controller = new CartController(null, mock.Object);
            controller.ModelState.AddModelError("error", "error");
            ViewResult result = controller.Checkout(cart, shippingDetails);

            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), 
                Times.Never());

            Assert.AreEqual(result.ViewName, "");
            Assert.AreEqual(result.ViewData.ModelState.IsValid, false);

        }
        [TestMethod]
        public void Cannot_checkout_submit_order()
        {
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            cart.AddItem(new Book(), 1);

            ShippingDetails shippingDetails = new ShippingDetails();
            CartController controller = new CartController(null, mock.Object);

            ViewResult result = controller.Checkout(cart, shippingDetails);

            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),
                Times.Once());

            Assert.AreEqual(result.ViewName, "Completed");
            Assert.AreEqual(result.ViewData.ModelState.IsValid, true);

        }
    }
}
