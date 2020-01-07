using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDemo.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public ICollection<Material> Materials { get; set; }
    }

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //builder.ToTable("Products");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Price).HasColumnType("decimal(8,2)");
            builder.Property(e => e.Description).HasMaxLength(200);

            //关系配置 外键 删除依赖
            //builder.HasMany(p => p.Materials).WithOne(m => m.Product).HasForeignKey(m => m.ProductId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
