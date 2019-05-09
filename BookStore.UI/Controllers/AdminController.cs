using BookStore.Domain.Abstract;
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
        public ActionResult Index()
        {
            return View(repo.Books);
        }
    }
}