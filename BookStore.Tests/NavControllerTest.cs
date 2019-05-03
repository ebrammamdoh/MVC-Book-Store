using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BookStore.Domain.Entities;
using BookStore.Domain.Abstract;
using BookStore.UI.Controllers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BookStore.UI.Models;

namespace BookStore.Tests
{
    [TestClass]
    public class NavControllerTest
    {
        [TestMethod]
        public void Can_Retrive_Category()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new Book[]
            {
                new Book {BookId = 1, Author = "Auth1", Category = "Drama", Description = "Desc", Price = 23.3m, Title = "Book1" },
                new Book {BookId = 2, Author = "Auth2", Category = "Comedy", Description = "Desc", Price = 10.2m, Title = "Book2" },
                new Book {BookId = 3, Author = "Auth3", Category = "Drama", Description = "Desc", Price = 11.8m, Title = "Book3" },
                new Book {BookId = 4, Author = "Auth4", Category = "Comedy", Description = "Desc", Price = 78.2m, Title = "Book4" },
                new Book {BookId = 5, Author = "Auth5", Category = "Action", Description = "Desc", Price = 15.0m, Title = "Book5" },
            });

            NavController controller = new NavController(mock.Object);

            string[] result = ((IEnumerable<string>)controller.Menu().Model).ToArray();

            Assert.AreEqual(result.Length, 3);
            Assert.IsTrue(result[0] == "Drama");
        }
       
        [TestMethod]
        public void Generate_category_specific_book_count()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new Book[]
            {
                new Book {BookId = 1, Author = "Auth1", Category = "Drama", Description = "Desc", Price = 23.3m, Title = "Book1" },
                new Book {BookId = 2, Author = "Auth2", Category = "Comedy", Description = "Desc", Price = 10.2m, Title = "Book2" },
                new Book {BookId = 3, Author = "Auth3", Category = "Drama", Description = "Desc", Price = 11.8m, Title = "Book3" },
                new Book {BookId = 4, Author = "Auth4", Category = "Comedy", Description = "Desc", Price = 78.2m, Title = "Book4" },
                new Book {BookId = 5, Author = "Auth5", Category = "Action", Description = "Desc", Price = 15.0m, Title = "Book5" },
            });

            BookController controller = new BookController(mock.Object);
            controller.pageSize = 3;

            int result1 = ((BookListViewModel)controller.List("Drama").Model).pagingInfo.TotalItems;
            int result2 = ((BookListViewModel)controller.List("Comedy").Model).pagingInfo.TotalItems;

            Assert.AreEqual(result1, 2);
            Assert.AreEqual(result2, 1);
        }

        [TestMethod]
        public void Indicates_selected_category()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new Book[]
            {
                new Book {BookId = 1, Author = "Auth1", Category = "Drama", Description = "Desc", Price = 23.3m, Title = "Book1" },
                new Book {BookId = 2, Author = "Auth2", Category = "Comedy", Description = "Desc", Price = 10.2m, Title = "Book2" },
                new Book {BookId = 3, Author = "Auth3", Category = "Drama", Description = "Desc", Price = 11.8m, Title = "Book3" },
                new Book {BookId = 4, Author = "Auth4", Category = "Comedy", Description = "Desc", Price = 78.2m, Title = "Book4" },
                new Book {BookId = 5, Author = "Auth5", Category = "Action", Description = "Desc", Price = 15.0m, Title = "Book5" },
            });

            NavController controller = new NavController(mock.Object);

            string result = controller.Menu("Drama").ViewBag.category;
            Assert.AreEqual("Drama", result);

        }
    }
}
