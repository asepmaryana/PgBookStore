using Microsoft.AspNetCore.Identity;

namespace PgBookStore.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName {get; set;}
        public string Photo {get; set;}
        
    }
}