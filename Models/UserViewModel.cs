using System.ComponentModel.DataAnnotations;

namespace PgBookStore.Models
{
    public partial class UserViewModel
    {
        [Display(Name = "Username")]
        public string UserName {get; set;}

        [Display(Name = "Role")]
        public string RoleName {get; set;}

        [Display(Name = "Email")]
        public string Email {get; set;}

        [Display(Name = "Fullname")]
        public string FullName {get; set;}
    }
}