using Microsoft.EntityFrameworkCore;


namespace Kursach.Models
{
    public class Fanfic
    {
        public int ID { get; set; }
        public string Tags { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string userId { get; set; }
        public User user { get; set; }
    }
    public class ApplicationContext : DbContext
    {
        public DbSet<Fanfic> Fanfics { get; set; }
    }

}
