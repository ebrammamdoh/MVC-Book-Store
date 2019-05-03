using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BookStore.Domain.Abstract;
using BookStore.Domain.Entities;
using BookStore.UI.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BookStore.UI.Models;
using BookStore.UI.HtmlHelper;

namespace BookStore.Tests
{
    [TestClass]
    public class BookControllerTest
    {
        [TestMethod]
        public void Can_Paginate()
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

            BookController bookController = new BookController(mock.Object);
            bookController.pageSize = 4;
            BookListViewModel result = bookController.List(null, 3).Model as BookListViewModel;

            Book[] bookArray = result.Books.ToArray();
            Assert.IsTrue(bookArray.Length == 1);
            Assert.AreEqual(bookArray[0].BookId, 9);
        }
        [TestMethod]
        public void Can_generate_Page_Links()
        {
            HtmlHelper myHelper = null;
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 14,
                ItemsPerPage = 5
            };
            Func<int, string> pageUrlDelegate = delFunction;

            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            var expResult = "<a class=\"btn btn-default\" href=\"page1\">1</a>"
                            + "<a class=\"btn btn-default btn-primary selected\" href=\"page2\">2</a>"
                            + "<a class=\"btn btn-default\" href=\"page3\">3</a>"
                            ;
            Assert.AreEqual(expResult, result.ToString());
        }
        private string delFunction(int i)
        {
            return "page" + i;
        }
        [TestMethod]
        public void Can_Send_Pagination_View_Model()
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
                new Book { BookId = 6, Author = "Auth6", Category = "Drama", Description = "Desc", Price = 45.1m, Title = "Book6" },
                new Book { BookId = 7, Author = "Auth7", Category = "Drama", Description = "Desc", Price = 40.4m, Title = "Book7" },
                new Book { BookId = 8, Author = "Auth8", Category = "Drama", Description = "Desc", Price = 80.5m, Title = "Book8" }
                }

            );
            BookController controller = new BookController(mock.Object);
            controller.pageSize = 3;

            BookListViewModel model = controller.List(null, 2).Model as BookListViewModel;

            PagingInfo pageInfo = model.pagingInfo;

            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 8);
            Assert.AreEqual(pageInfo.TotalPages, 3);

        }
        [TestMethod]
        public void Can_Filter()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new Book[]
            {
                new Book {BookId = 1, Author = "Auth1", Category = "Drama", Description = "Desc", Price = 23.3m, Title = "Book1" },
                new Book {BookId = 2, Author = "Auth2", Category = "Comedy", Description = "Desc", Price = 10.2m, Title = "Book2" },
                new Book {BookId = 3, Author = "Auth3", Category = "Comedy", Description = "Desc", Price = 11.8m, Title = "Book3" },
                new Book {BookId = 4, Author = "Auth4", Category = "Drama", Description = "Desc", Price = 78.2m, Title = "Book4" },
                new Book {BookId = 5, Author = "Auth5", Category = "Action", Description = "Desc", Price = 15.0m, Title = "Book5" },
                new Book {BookId = 6, Author = "Auth6", Category = "Drama", Description = "Desc", Price = 45.1m, Title = "Book6" },
                new Book {BookId = 7, Author = "Auth7", Category = "Action", Description = "Desc", Price = 40.4m, Title = "Book7" },
                new Book {BookId = 8, Author = "Auth8", Category = "Comedy", Description = "Desc", Price = 80.5m, Title = "Book8" },
                new Book {BookId = 9, Author = "Auth9", Category = "Drama", Description = "Desc", Price = 100.8m, Title = "Book9" },
            });

            BookController bookController = new BookController(mock.Object);
            bookController.pageSize = 3;

            Book[] result = ((BookListViewModel)bookController.List("Drama", 2).Model).Books.ToArray();

            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].BookId == 4);
        }
    }
}
