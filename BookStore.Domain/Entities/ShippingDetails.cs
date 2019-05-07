using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string name { get; set; }
        [Required(ErrorMessage = "Address line1 is required")]
        [Display(Name = "Address Line 1")]
        public string Line1 { get; set; }
        [Display(Name = "Address Line 2")]
        public string Line2 { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string city { get; set; }
        public string state { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string country { get; set; }
        public bool giftWrap { get; set; }

    }
}
