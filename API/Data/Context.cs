using API.Model;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            User user = new User()
            {
                UserID = 1,
                Name = "Fatih",
                Mail = "fd@gmail.com",
                Birthday = new DateTime(1998, 10, 15),
                Age = DateTime.Now.Year - new DateTime(1998, 10, 15).Year
            };

            modelBuilder.Entity<User>().HasData(user);
        }

        public DbSet<User> Users { get; set; }

    }
}
