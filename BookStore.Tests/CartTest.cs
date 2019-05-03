using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookStore.Domain.Entities;
using System.Linq;

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
    }
}
