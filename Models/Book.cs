using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PgBookStore.Models
{
    public partial class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ISBN")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "{0} harus angka.")]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "{0} tidak boleh lebih dari {1} dan tidak boleh kurang {2} karakter.")]
        public int BookID {get; set;}

        [ForeignKey("Category")]
        [Display(Name = "Category ID")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "{0} harus angka.")]
        public int CategoryID {get; set;}

        public Category Category {get; set;}

        [Display(Name = "Title")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Title {get; set;}

        [Display(Name = "Photo")]
        public string Photo {get; set;}

        [Display(Name = "Publish Date")]
        public DateTime PublishDate {get; set;}

        [Display(Name = "Price")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "{0} harus angka.")]
        [Range(10000, 100000, ErrorMessage = "{0} harus diantara Rp. {1} dan Rp. {2}")]
        public double Price {get; set;}

        [Display(Name ="Quantity")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "{0} harus angka.")]
        public int Quantity {get; set;}

        public ICollection<BookAuthor> BooksAuthors {get; set;}
        
    }
}