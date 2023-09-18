using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;

namespace NLayer.Repository.Configurations
{
    internal class ProductFeatureConfiguration : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            builder.HasKey(a => a.ID);
            builder.Property(a => a.ID).UseIdentityColumn();

            builder.HasOne(a => a.Product).WithOne(a => a.ProductFeature)
                .HasForeignKey<ProductFeature>(a => a.ProductID);
        }
    }
}
