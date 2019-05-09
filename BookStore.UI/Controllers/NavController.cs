using BookStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.UI.Controllers
{
    public class NavController : Controller
    {
        private IBookRepository repo;
        public NavController(IBookRepository _repo)
        {
            repo = _repo;
        }
        // GET: Nav
        public PartialViewResult Menu(string category = null)
        {
            ViewBag.category = category;
            IEnumerable<string> categories = repo.Books
                                            .Select(b => b.Category)
                                            .Distinct();
            //string viewName = mobileLayout ? "MenuHorizontal" : "Menu";
            return PartialView("MenuFlex", categories);
        }

    }
}