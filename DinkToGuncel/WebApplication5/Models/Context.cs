using Microsoft.EntityFrameworkCore;

namespace WebApplication5.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<bill> Bills { get; set; }
        public DbSet<Compan> Compans { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Veritabanı bağlantı dizesini burada belirtin
                optionsBuilder.UseSqlServer("baglanti");
            }


        }

    }
}
