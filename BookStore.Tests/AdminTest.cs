using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BookStore.Domain.Abstract;
using BookStore.Domain.Entities;
using BookStore.UI.Controllers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BookStore.Tests
{
    [TestClass]
    public class AdminTest
    {
        [TestMethod]
        public void Index_Contains_All_Books()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new Book[]
            {
                new Book {BookId = 1, Author = "Auth1", Category = "Drama", Description = "Desc", Price = 23.3m, Title = "Book1" },
                new Book {BookId = 2, Author = "Auth2", Category = "Drama", Description = "Desc", Price = 10.2m, Title = "Book2" },
                new Book {BookId = 3, Author = "Auth3", Category = "Drama", Description = "Desc", Price = 11.8m, Title = "Book3" },
                new Book {BookId = 4, Author = "Auth4", Category = "Drama", Description = "Desc", Price = 78.2m, Title = "Book4" },
                new Book {BookId = 5, Author = "Auth5", Category = "Drama", Description = "Desc", Price = 15.0m, Title = "Book5" },
                new Book {BookId = 6, Author = "Auth6", Category = "Drama", Description = "Desc", Price = 45.1m, Title = "Book6" },
                new Book {BookId = 7, Author = "Auth7", Category = "Drama", Description = "Desc", Price = 40.4m, Title = "Book7" },
                new Book {BookId = 8, Author = "Auth8", Category = "Drama", Description = "Desc", Price = 80.5m, Title = "Book8" },
                new Book {BookId = 9, Author = "Auth9", Category = "Drama", Description = "Desc", Price = 100.8m, Title = "Book9" },
            });

            AdminController controller = new AdminController(mock.Object);
            Book[] result = ((IEnumerable<Book>)controller.Index().ViewData.Model).ToArray();

            Assert.AreEqual(result.Count(), 9);
            Assert.AreEqual(result[0].BookId, 1);
        }
        [TestMethod]
        public void Can_Edit_Book()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new Book[]
            {
                new Book {BookId = 1, Author = "Auth1", Category = "Drama", Description = "Desc", Price = 23.3m, Title = "Book1" },
                new Book {BookId = 2, Author = "Auth2", Category = "Drama", Description = "Desc", Price = 10.2m, Title = "Book2" },
                new Book {BookId = 3, Author = "Auth3", Category = "Drama", Description = "Desc", Price = 11.8m, Title = "Book3" },
                new Book {BookId = 4, Author = "Auth4", Category = "Drama", Description = "Desc", Price = 78.2m, Title = "Book4" },
            });

            AdminController controller = new AdminController(mock.Object);
            Book result = controller.Edit(1).ViewData.Model as Book;
            Book result2 = controller.Edit(3).ViewData.Model as Book;

            Assert.AreEqual(result.Title, "Book1");
            Assert.AreEqual(result2.Title, "Book3");
        }

        [TestMethod]
        public void Cannot_Edit_NotExist_Book()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new Book[]
            {
                new Book {BookId = 1, Author = "Auth1", Category = "Drama", Description = "Desc", Price = 23.3m, Title = "Book1" },
                new Book {BookId = 2, Author = "Auth2", Category = "Drama", Description = "Desc", Price = 10.2m, Title = "Book2" },
                new Book {BookId = 3, Author = "Auth3", Category = "Drama", Description = "Desc", Price = 11.8m, Title = "Book3" },
                new Book {BookId = 4, Author = "Auth4", Category = "Drama", Description = "Desc", Price = 78.2m, Title = "Book4" },
            });

            AdminController controller = new AdminController(mock.Object);
            Book result = controller.Edit(5).ViewData.Model as Book;

            Assert.IsNull(result);
        }
        [TestMethod]
        public void Can_save_Valid_changes()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            Book book = new Book { BookId = 1, Title = "Book1" };

            AdminController controller = new AdminController(mock.Object);
            ActionResult result = controller.Edit(book);

            mock.Verify(b => b.SaveBook(book));
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void Cannot_save_InValid_changes()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            Book book = new Book { BookId = 1, Title = "Book1" };

            AdminController controller = new AdminController(mock.Object);
            controller.ModelState.AddModelError("error", "error happen in model");
            ActionResult result = controller.Edit(book);

            mock.Verify(b => b.SaveBook(It.IsAny<Book>()), Times.Never);
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void Can_Delete_Valid_Book()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(p => p.Books).Returns(new Book[]
            {
                new Book { BookId = 1, Title = "Book1" },
                new Book { BookId = 2, Title = "Book2" },
                new Book { BookId = 3, Title = "Book3" },
            });

            AdminController controller = new AdminController(mock.Object);
            ActionResult result = controller.Delete(1);

            mock.Verify(b => b.DeleteBook(1));
        }
    }
}
