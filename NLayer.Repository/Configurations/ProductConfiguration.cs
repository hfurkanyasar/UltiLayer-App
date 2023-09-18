using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;

namespace NLayer.Repository.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(a => a.ID);
            builder.Property(a => a.ID).UseIdentityColumn();
            builder.Property(a => a.Name).IsRequired().HasMaxLength(200);
            builder.Property(a => a.Stock).IsRequired();

            // ****************,**(18,2) (toplam 18 karakter 2 tanesi virgül sonrası)
            builder.Property(a => a.Price).IsRequired().HasColumnType("decimal(18,2)");

            builder.ToTable("Product");

            builder.HasOne(a => a.Category).WithMany(a => a.Products).HasForeignKey(a => a.CategoryID);


        }
    }
}
