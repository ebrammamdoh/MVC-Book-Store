using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BookStore.Domain.Entities
{
    public class Book
    {
        //[Key]
        [HiddenInput(DisplayValue = false)]
        public int BookId { get; set; }
        [Required(ErrorMessage ="Please enter a book title")]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        public string Description { get; set; }
        public string Category { get; set; }
        [Required]
        [Range(minimum:1, maximum:9999.99)]
        public decimal Price { get; set; }
        public string Author { get; set; }

    }
}
