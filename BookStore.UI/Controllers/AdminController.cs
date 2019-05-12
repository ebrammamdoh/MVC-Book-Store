using BookStore.Domain.Abstract;
<<<<<<< HEAD
=======
using BookStore.Domain.Entities;
>>>>>>> 58c2c37351e018a12731e1082330490bdfa9bf6f
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.UI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IBookRepository repo;
        public AdminController(IBookRepository _repo)
        {
            repo = _repo;
        }
        // GET: Admin
<<<<<<< HEAD
        public ActionResult Index()
        {
            return View(repo.Books);
        }
=======
        public ViewResult Index()
        {
            return View(repo.Books);
        }
        public ViewResult Create()
        {
            return View(repo.Books);
        }
        [HttpGet]
        public ViewResult Edit(int bookId)
        {
            Book book = repo.Books.Where(b => b.BookId == bookId).FirstOrDefault();
            return View(book);
        }
        [HttpPost]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                repo.SaveBook(book);
                TempData.Add("message", book.Title + " has been saved");
                return RedirectToAction("Index");
            }
            else
            {
                return View(book);
            }

        }
>>>>>>> 58c2c37351e018a12731e1082330490bdfa9bf6f
    }
}