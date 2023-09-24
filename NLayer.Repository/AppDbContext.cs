using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using System.Reflection;

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


        public override int SaveChanges()
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReferans)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                Entry(entityReferans).Property(a => a.UpdateDate).IsModified = false;
                                entityReferans.CreatedDate = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                Entry(entityReferans).Property(a => a.CreatedDate).IsModified = false;
                                entityReferans.UpdateDate = DateTime.Now;
                                break;

                            }
                    }
                }
            }
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {


            //savechanges metodunu ezerek upadte ve cratedatelerini güncelleyeceğiz.


            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReferans)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                Entry(entityReferans).Property(a => a.UpdateDate).IsModified = false;
                                entityReferans.CreatedDate = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                Entry(entityReferans).Property(a => a.CreatedDate).IsModified = false;

                                entityReferans.UpdateDate = DateTime.Now;
                                break;

                            }
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

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
