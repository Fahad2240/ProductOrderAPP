using DotNET.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNET.DAL
{
    public class ProductOrderDbContext : DbContext
    {
        public ProductOrderDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<TblProducts> tblProducts { get; set; } 
        public DbSet<TblOrders> tblOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the foreign key explicitly (optional)
            modelBuilder.Entity < TblOrders>()
                .HasOne(o => o.TblProducts)
                .WithMany() // Product can have many orders
                .HasForeignKey(o => o.IntProductId)
                .OnDelete(DeleteBehavior.Cascade); // Optional: Cascade delete
        }
    }
}
