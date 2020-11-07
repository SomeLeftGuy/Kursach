using Microsoft.EntityFrameworkCore;

namespace Kursach.Models
{
    public class Fanfic
    {
        public int ID { get; set; }
        public string Tags { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
    }

    public class ApplicationContext : DbContext
    {
        public DbSet<Fanfic> Fanfics { get; set; }
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=dbo.fanfics;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
        }
    }
   
}
