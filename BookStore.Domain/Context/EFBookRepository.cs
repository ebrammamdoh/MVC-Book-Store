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
    }
}
