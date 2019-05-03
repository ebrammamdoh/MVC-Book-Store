using BookStore.Domain.Abstract;
using BookStore.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.UI.Controllers
{
    public class BookController : Controller
    {
        private IBookRepository repo;
        public int pageSize = 2;
        public BookController(IBookRepository _repo)
        {
            repo = _repo;
        }
        public ViewResult List(string category, int page = 1)
        {
            BookListViewModel model = new BookListViewModel
            {
                Books = repo.Books
                        .Where(b=> b.Category == null || b.Category == category)
                        .OrderBy(b => b.BookId)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize),
                pagingInfo = new PagingInfo
                {
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ? repo.Books.Count() : 
                                    repo.Books.Where(b => b.Category == category).Count(),
                    CurrentPage = page
                },
                CurrentCategory = category
            };


            return View(model);
        }
        public ViewResult ListAll()
        {
            return View(repo.Books);
        }
    }
}