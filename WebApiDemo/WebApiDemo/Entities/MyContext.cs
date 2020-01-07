using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDemo.Entities
{
    public class MyContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Material> Materials { get; set; }

        public MyContext(DbContextOptions<MyContext> options)
            :base(options)
        {
            //Database.EnsureCreated();
            Database.Migrate();
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("xxxx connection string");
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Product>().ToTable("Products");
            //modelBuilder.Entity<Product>().HasKey(e => e.Id);
            //modelBuilder.Entity<Product>().Property(e => e.Name).IsRequired().HasMaxLength(50);
            //modelBuilder.Entity<Product>().Property(e=>e.Price).HasColumnType("decimal(8,2)");

            //ProductMap(modelBuilder);

            modelBuilder.ApplyConfiguration<Product>(new ProductConfiguration());
            modelBuilder.ApplyConfiguration<Material>(new MaterialConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public void ProductMap(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Product>().HasKey(e => e.Id);
            modelBuilder.Entity<Product>().Property(e => e.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Product>().Property(e => e.Price).HasColumnType("decimal(8,2)");
        }
    }
}
