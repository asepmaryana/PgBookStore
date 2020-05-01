using System;
using System.ComponentModel.DataAnnotations;

namespace PgBookStore.Models
{
    public partial class BookViewModel
    {
        [Display(Name = "ISBN")]
        public int ISBN {get; set;}

        [Display(Name = "Category")]
        public string CategoryName {get; set;}

        [Display(Name = "Title")]
        public string Title {get; set;}

        [Display(Name = "Photo")]
        public string Photo {get; set;}

        [Display(Name = "Publish Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime PublishDate {get; set;}

        [Display(Name = "Price")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double Price {get; set;}

        [Display(Name = "Quantity")]
        public int Quantity {get; set;}

        [Display(Name = "List Author Names")]
        public string AuthorNames {get; set;}
    }
}