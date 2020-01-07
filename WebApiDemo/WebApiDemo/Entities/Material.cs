using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApiDemo.Entities
{
    public class Material
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }

        public Product Product { get; set; }
    }

    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            //builder.ToTable("Materials");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(50);

            //表关系 外键 删除操作依赖
            builder.HasOne(m => m.Product).WithMany(p => p.Materials).HasForeignKey(m=>m.ProductId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
