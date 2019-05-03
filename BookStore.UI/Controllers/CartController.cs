using BookStore.Domain.Abstract;
using BookStore.Domain.Entities;
using BookStore.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.UI.Controllers
{
    public class CartController : Controller
    {
        private IBookRepository repo;
        public CartController(IBookRepository _repo)
        {
            repo = _repo;
        }
        public RedirectToRouteResult AddToCart(int BookId, string returnUrl)
        {
            Book book = repo.Books.Where(b => b.BookId == BookId).FirstOrDefault();
            if(book != null)
            {
                GetCart().AddItem(book);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToRouteResult RemoveFromCart(int bookId, string returnUrl)
        {
            Book book = repo.Books.Where(b => b.BookId == bookId).FirstOrDefault();
            if (book != null)
            {
                GetCart().RemoveItem(book);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        private Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if(cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public ActionResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                cart = GetCart(),
                returnUrl = returnUrl
            });
        }
    }
}