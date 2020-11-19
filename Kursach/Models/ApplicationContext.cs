using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Kursach.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Fanfic> Fanfics { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public  DbSet<TagsToFanfics> TagsToFanfics { get; set; }
        public DbSet<Mark> Marks { get; set; }    
       // public DbSet<User> Users { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
          : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
