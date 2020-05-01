using System.ComponentModel.DataAnnotations;

namespace PgBookStore.Models
{
    public partial class UserCreateFormViewModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string UserName {get; set;}

        [Display(Name = "Role")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string[] RoleID {get; set;}

        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [StringLength(256, ErrorMessage = "{0} tidak boleh lebih {1} karakter.")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "{0} tidak valid.")]
        public string Email {get; set;}

        [Display(Name ="Password")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [StringLength(256, ErrorMessage = "{0} tidak boleh lebih {1} karakter.")]
        [DataType(DataType.Password)]
        public string Password {set; get;}

        [Display(Name ="Password Confirm")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [StringLength(256, ErrorMessage = "{0} tidak boleh lebih {1} karakter.")]
        [Compare("Password", ErrorMessage = "{0} dan {1} harus sama.")]
        [DataType(DataType.Password)]
        public string PasswordConfirm {set; get;}

        [Display(Name ="Full Name")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [StringLength(256, ErrorMessage = "{0} tidak boleh lebih {1} karakter.")]
        public string FullName {set; get;}

    }
}