using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;

namespace NLayer.Repository.Seeds
{
    internal class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(new Product
            {
                ID = 1,
                CategoryID = 1,
                Name = "Kalem 1",
                Price = 100,
                Stock = 20,
                CreatedDate = DateTime.Now

            },
            new Product
            {
                ID = 2,
                CategoryID = 1,
                Name = "Kalem 2",
                Price = 200,
                Stock = 30,
                CreatedDate = DateTime.Now
            },
            new Product
            {
                ID = 3,
                CategoryID = 1,
                Name = "Kalem 3",
                Price = 600,
                Stock = 60,
                CreatedDate = DateTime.Now
            },
            new Product
            {
                ID = 4,
                CategoryID = 2,
                Name = "Kitap 1",
                Price = 500,
                Stock = 3200,
                CreatedDate = DateTime.Now
            },
            new Product
            {
                ID = 5,
                CategoryID = 2,
                Name = "Kitap 2",
                Price = 700,
                Stock = 1200,
                CreatedDate = DateTime.Now
            });
        }
    }
}
