using Microsoft.EntityFrameworkCore;
using NLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ProductFullModel> ProductFullModels { get; set; }
        public DbSet<Category>  Categories { get; set; }
        public DbSet<Product>  Products { get; set; }
        public DbSet<ProductFeature>  ProductFeatures { get; set; }
        public DbSet<CategoryCount>  CategoryCounts { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductFullModel>().HasNoKey();
            modelBuilder.Entity<CategoryCount>().HasNoKey();
            modelBuilder.Entity<ProductFullModel>().ToView("product_with_feature");
            base.OnModelCreating(modelBuilder);
        }
    }
}
