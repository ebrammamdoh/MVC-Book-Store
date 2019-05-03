using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.UI.Models
{
    public class CartIndexViewModel
    {
        public Cart cart { get; set; }
        public string returnUrl { get; set; }

    }
}