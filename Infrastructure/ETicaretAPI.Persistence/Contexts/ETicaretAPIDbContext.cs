using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ETicaretAPI.Persistence.Contexts
{
    public class ETicaretAPIDbContext(DbContextOptions options) : IdentityDbContext<AppUser, AppRole, string>(options)
    {
        // public ETicaretAPIDbContext(DbContextOptions options) : base(options){ }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<MainCategory> MainCategories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<UserCard> UserCards { get; set; }
        public DbSet<OrderAddress> OrderAddresses { get; set; }
        public DbSet<OrderPayment> OrderPayments { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>()
                .HasKey(b => b.Id);

            builder.Entity<Order>()
                .HasIndex(o => o.OrderCode)
                .IsUnique();

            builder.Entity<Basket>()
                .HasOne(b => b.Order)
                .WithOne(o => o.Basket)
                .HasForeignKey<Order>(b => b.Id);

            // OrderAdresses
            builder.Entity<Order>()
               .HasOne(x => x.OrderAddressBilling)
               .WithOne(y => y.OrderBilling)
               .HasForeignKey<Order>(z => z.OrderAddressShippingId)
               .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Order>()
                .HasOne(x => x.OrderAddressShipping)
                .WithOne(y => y.OrderShipping)
                .HasForeignKey<Order>(z => z.OrderAddressBillingId)
                .OnDelete(DeleteBehavior.Restrict);
            // Payment
            builder.Entity<Order>()
               .HasOne(x => x.OrderPayment)
               .WithOne(y => y.Order)
               .HasForeignKey<Order>(z => z.OrderPaymentId)
               .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //ChangeTracker : Entityler üzerinden yapılan değişiklerin ya da yeni eklenen verinin yakalanmasını sağlayan propertydir. Update operasyonlarında Track edilen verileri yakalayıp elde etmemizi sağlar.

            var datas = ChangeTracker
                 .Entries<BaseEntity>();

            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
