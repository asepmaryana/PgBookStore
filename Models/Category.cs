using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PgBookStore.Models
{
    public partial class Category
    {
        [Display(Name = "Category ID")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID {get; set;}

        [Display(Name = "Book Category Name")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [StringLength(256, ErrorMessage = "{0} tidak boleh lebih dari {1} karakter.")]
        public string Name {get; set;}

        public ICollection<Book> Books {get; set;}
    }
}