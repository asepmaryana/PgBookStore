using Microsoft.AspNetCore.Identity;

namespace PgBookStore.Models
{
    public class ApplicationRole : IdentityRole 
    {
        public string Description {get; set;}
    }
}