using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Kursach.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Fanfic> Fanfics { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public  DbSet<TagsToFanfics> TagsToFanfics { get; set; }
        public DbSet<Chapters> Chapters { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
          : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
