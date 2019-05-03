using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Book book, int quantity = 1)
        {
            CartLine cLine = lineCollection.Where(b => b.Book.BookId == book.BookId).FirstOrDefault();
            if (cLine == null)
            {
                lineCollection.Add(new CartLine
                {
                    Book = book,
                    Quantity = quantity
                });
            }
            else
                cLine.Quantity += quantity;

        }
        public void RemoveItem(Book book)
        {
            lineCollection.RemoveAll(b => b.Book.BookId == book.BookId);
        }
        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(b => b.Book.Price * b.Quantity);
        }
        public void Clear()
        {
            lineCollection.Clear();
        }
        public IEnumerable<CartLine> CartLines
        {
            get { return lineCollection; }
        }
    }
}
