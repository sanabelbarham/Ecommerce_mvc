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
            optionsBuilder.UseSqlServer("Server=db31219.public.databaseasp.net; Database=db31219; User Id=db31219; Password=G!d4i7#T5+Wt; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True; ");
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
