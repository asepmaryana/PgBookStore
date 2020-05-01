using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PgBookStore.Models
{
    public partial class BookFormViewModel
    {
        [Display(Name = "ISBN")]
        public int ISBN {get; set;}
        
        [Display(Name = "Category")]
        public int CategoryID {get; set;}

        [Display(Name = "Title")]
        public string Title {get; set;}

        [Display(Name = "Photo")]
        [DataType(DataType.Upload)]
        public IFormFile Photo {get; set; }

        [Display(Name = "Publish Date")]
        [DataType(DataType.Date)]
        public DateTime PublishDate {get; set;}
        
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public double Price {get; set;}

        [Display(Name = "Quantity")]
        public int Quantity {get; set;}

        [Display(Name = "List of Author Names")]
        public int[] AuthorIDs {get; set;}
    }
}