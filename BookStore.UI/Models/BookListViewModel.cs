using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.UI.Models
{
    public class BookListViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        public PagingInfo pagingInfo { get; set; }
        public string CurrentCategory { get; set; }

    }
}