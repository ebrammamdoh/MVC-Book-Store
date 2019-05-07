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
        private IOrderProcessor orderProcessor;
        public CartController(IBookRepository _repo, IOrderProcessor _orderProcessor)
        {
            repo = _repo;
            orderProcessor = _orderProcessor;
        }
        public RedirectToRouteResult AddToCart(Cart cart, int BookId, string returnUrl)
        {
            Book book = repo.Books.Where(b => b.BookId == BookId).FirstOrDefault();
            if(book != null)
            {
                cart.AddItem(book);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToRouteResult RemoveFromCart(Cart cart, int bookId, string returnUrl)
        {
            Book book = repo.Books.Where(b => b.BookId == bookId).FirstOrDefault();
            if (book != null)
            {
                cart.RemoveItem(book);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        //private Cart GetCart()
        //{
        //    Cart cart = Session["Cart"] as Cart;
        //    if(cart == null)
        //    {
        //        cart = new Cart();
        //        Session["Cart"] = cart;
        //    }
        //    return cart;
        //}

        public ActionResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                cart = cart,
                returnUrl = returnUrl
            });
        }
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }
        [HttpPost ]
        public ViewResult Checkout(Cart cart, ShippingDetails shipDetails)
        {
            if(cart.CartLines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry your cart is empty");
            }
            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shipDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shipDetails);
            }
            
        }
    }
}