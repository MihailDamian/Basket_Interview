using Checkout.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace Checkout.Infrastructure.Database
{
    public class ShopContext : DbContext
    {
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketArticle> BasketArticles { get; set; }

        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-UH50LF4\\SQLEXPRESS;Initial Catalog=Checkout;Integrated Security=True");
        }


    }
}