using BookStore.UI.Infrastructure.Abstract;
using BookStore.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.UI.Controllers
{
    public class AccountController : Controller
    {
        private IAuthProvider authProvider;

        public AccountController(IAuthProvider _authProvider)
        {
            authProvider = _authProvider;
        }
        [HttpGet]
        public ViewResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (authProvider.Authenticate(model.username, model.password))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password");
                    return View();
                }
            }
            else
                return View();
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}