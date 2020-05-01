using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PgBookStore.Models;

namespace PgBookStore.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Catetories {get; set;}
        public virtual DbSet<Book> Books {get; set;}
        public virtual DbSet<Author> Authors {get; set;}
        public virtual DbSet<BookAuthor> BookAuthors {get; set;}
    }
}
