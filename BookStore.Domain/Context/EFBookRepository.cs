using BookStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Entities;

namespace BookStore.Domain.Context
{
    public class EFBookRepository : IBookRepository
    {
        private DataContext context;
        public EFBookRepository()
        {
            context = new DataContext();
        }
        public IEnumerable<Book> Books
        {
            get
            {
                return context.Books;
            }
        }

        public void SaveBook(Book book)
        {
            Book oldBook = context.Books.Where(b => b.BookId == book.BookId).FirstOrDefault();
            if (oldBook == null)
            {
                context.Books.Add(book);
            }
            else
            {
                oldBook.Author = book.Author;
                oldBook.Category = book.Author;
                oldBook.Description = book.Description;
                oldBook.Price = book.Price;
                oldBook.Title = book.Title;
            }
            context.SaveChanges();
        }
    }
}
