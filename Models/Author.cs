using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PgBookStore.Models
{
    public partial class Author
    {
        [Display(Name = "ID")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public int AuthorID {get; set;}

        [Display(Name = "Author's Name")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [StringLength(256, ErrorMessage = "{0} tidak boleh lebih dari {1} karakter.")]
        public string Name {get; set;}

        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "{0} tidak valid.")]
        [StringLength(256, MinimumLength = 10, ErrorMessage = "{0} harus memiliki maksimal {1} dan minimal {2} karakter.")]
        public string Email {get; set;}

        public ICollection<BookAuthor> BooksAuthors {get; set;}
    }
}