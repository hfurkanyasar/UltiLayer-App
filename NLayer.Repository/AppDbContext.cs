using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }


        public DbSet<Product> Products { get; set; }

        public DbSet<ProductFeature> ProductFeatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // assemblylere git tüm ınterfacelere gitsin uygulasın. tek tek verebiliriz ama çok olursa sıkıntı
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            // normalde burası kullanılmaz seeds olarak ayrı bi foldera çıkılır ve içleri okunur,
            // burası örnek olarak. best practice değil.
            modelBuilder.Entity<ProductFeature>().HasData(new ProductFeature()
            {
                ID = 1,
                Color = "Kırmızı",
                Height = 100,
                Widht = 200,
                ProductID = 1

            },
            new ProductFeature()
            {
                ID = 2,
                Color = "Mavi",
                Height = 300,
                Widht = 500,
                ProductID = 2

            });

            base.OnModelCreating(modelBuilder);
        }



    }
}
