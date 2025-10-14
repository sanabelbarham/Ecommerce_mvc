using Ecommerce_mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_mvc.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.;Database=Ecommerce_mvc;Trusted_Connection=True;TrustServerCertificate=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                
                new Category { Id=1,Name="Mobiles"},
                new Category { Id=2,Name="Labtops"},
                new Category { Id=3,Name="Tablets   "}
                );
        }
    }
}
