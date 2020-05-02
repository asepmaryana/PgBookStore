using System;
using System.ComponentModel.DataAnnotations;

namespace PgBookStore.Models
{
    public partial class UserLoginFormViewModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string UserName {get; set;}

        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [DataType(DataType.Password)]
        public string Password {get; set;}
    }
}