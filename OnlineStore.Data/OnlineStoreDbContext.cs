using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Common.Constants;
using OnlineStore.Models;
using System;

namespace OnlineStore.Data
{
    public class OnlineStoreDbContext : IdentityDbContext<User>
    {
        public DbSet<Comment> Comments { get; set; }

        public DbSet<DeliveryInfo> DeliverysInfos { get; set; }

        public DbSet<District> Districts { get; set; }

        public DbSet<PopulatedPlace> PopulatedPlaces { get; set; }

        public DbSet<OrderProduct> OrderProducts { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderStatus> OrdersStatuses { get; set; }

        public DbSet<PaymentType> PaymentTypes { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<SubCategory> SubCategories { get; set; }

        public DbSet<UserFavoriteProduct> UsersFavoriteProducts { get; set; }

        public OnlineStoreDbContext(DbContextOptions<OnlineStoreDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(user =>
            {
                user.Property(u => u.RegisterDate)
                    .IsRequired()
                    .HasDefaultValue(DateTime.UtcNow);

                user.HasOne(u => u.ProfilePicture)
                    .WithOne(p => p.User);

                user.HasMany(u => u.Orders)
                    .WithOne(o => o.User)
                    .HasForeignKey(o => o.UserId);

                user.HasMany(u => u.DeliveryInfos)
                    .WithOne(di => di.User)
                    .HasForeignKey(di => di.UserId);

                user.HasMany(u => u.Reviews)
                    .WithOne(r => r.User)
                    .HasForeignKey(r => r.UserId);

                user.HasMany(u => u.Questions)
                    .WithOne(q => q.User)
                    .HasForeignKey(q => q.UserId);

                user.HasMany(u => u.Comments)
                    .WithOne(c => c.User)
                    .HasForeignKey(c => c.UserId);
            });

            builder.Entity<Comment>(comment =>
            {
                comment.HasKey(c => c.Id);

                comment.Property(c => c.Description)
                    .IsRequired()
                    .HasMaxLength(ModelsConstants.DescriptionMaxLength);

                comment.Property(c => c.LikesCount)
                    .IsRequired();

                comment.Property(c => c.DislikesCount)
                    .IsRequired();

                comment.Property(c => c.IsVerified)
                    .IsRequired();

                comment.Property(c => c.PostDate)
                    .IsRequired()
                    .HasDefaultValue(DateTime.Now);

                comment.HasOne(c => c.User)
                    .WithMany(u => u.Comments)
                    .HasForeignKey(c => c.UserId);

                comment.HasMany(c => c.Questions)
                    .WithOne(q => q.Comment)
                    .HasForeignKey(q => q.CommentId);
            });

            builder.Entity<DeliveryInfo>(deliveryInfo =>
            {
                deliveryInfo.HasKey(di => di.Id);

                deliveryInfo.Property(di => di.Address)
                    .IsRequired()
                    .HasMaxLength(ModelsConstants.AddressMaxLength);

                deliveryInfo.Property(di => di.FullName)
                    .IsRequired()
                    .HasMaxLength(ModelsConstants.NameMaxLength);

                deliveryInfo.Property(di => di.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(ModelsConstants.PhoneNumberMaxLength);

                deliveryInfo.HasMany(di => di.Orders)
                    .WithOne(o => o.DeliveryInfo)
                    .HasForeignKey(o => o.DeliveryInfoId);

                deliveryInfo.HasOne(di => di.District)
                    .WithMany(d => d.DeliverysInfos)
                    .HasForeignKey(di => di.DistrictId);

                deliveryInfo.HasOne(di => di.PopulatedPlace)
                    .WithMany(pp => pp.DeliveryInfos)
                    .HasForeignKey(di => di.PopulatedPlaceId);

                deliveryInfo.HasOne(di => di.PopulatedPlace)
                    .WithMany(pp => pp.DeliveryInfos)
                    .HasForeignKey(di => di.PopulatedPlaceId);
            });

            builder.Entity<District>(district =>
            {
                district.HasKey(d => d.Id);

                district.Property(d => d.Name)
                    .IsRequired();

                district.HasMany(d => d.DeliverysInfos)
                    .WithOne(di => di.District);

                district.HasMany(d => d.PopulatedPlaces)
                    .WithOne(pp => pp.District)
                    .HasForeignKey(pp => pp.DistrictId);
            });

            builder.Entity<PopulatedPlace>(populatedPlace =>
            {
                populatedPlace.HasKey(pp => pp.Id);

                populatedPlace.Property(pp => pp.Name)
                    .IsRequired();

                populatedPlace.HasOne(pp => pp.District)
                    .WithMany(d => d.PopulatedPlaces)
                    .HasForeignKey(pp => pp.DistrictId);
            });

            builder.Entity<OrderProduct>(orderProduct =>
            {
                orderProduct.HasKey(op => new { op.OrderId, op.ProductId });

                orderProduct.Property(op => op.Count)
                    .IsRequired();
            });

            builder.Entity<Order>(order =>
            {
                order.HasKey(o => o.Id);

                order.Property(o => o.OrderDate)
                    .IsRequired()
                    .HasDefaultValue(DateTime.Now);

                order.Property(o => o.TotalPrice)
                    .IsRequired();

                order.Property(o => o.DeliveryExpectedTime)
                    .IsRequired()
                    .HasDefaultValue(DateTime.Now.AddDays(3));

                order.Property(o => o.DeliveryPrice)
                    .IsRequired();

                order.HasMany(o => o.OrderProducts)
                    .WithOne(op => op.Order)
                    .HasForeignKey(o => o.OrderId);

                order.HasOne(o => o.PaymentType)
                    .WithMany(pt => pt.Orders)
                    .HasForeignKey(o => o.PaymentTypeId);

                order.HasOne(o => o.DeliveryInfo)
                    .WithMany(di => di.Orders)
                    .HasForeignKey(o => o.DeliveryInfoId);

                order.HasOne(o => o.OrderStatus)
                    .WithMany(os => os.Orders)
                    .HasForeignKey(o => o.OrderStatusId);

                order.HasOne(o => o.User)
                    .WithMany(u => u.Orders)
                    .HasForeignKey(o => o.UserId);
            });

            builder.Entity<OrderStatus>(orderStatus =>
            {
                orderStatus.HasKey(os => os.Id);

                orderStatus.Property(os => os.Name)
                    .IsRequired();

                orderStatus.HasMany(os => os.Orders)
                    .WithOne(o => o.OrderStatus)
                    .HasForeignKey(o => o.OrderStatusId);
            });

            builder.Entity<PaymentType>(paymentType =>
            {
                paymentType.HasKey(pt => pt.Id);

                paymentType.Property(pt => pt.Name)
                    .HasMaxLength(ModelsConstants.NameMaxLength);

                paymentType.HasMany(pt => pt.Orders)
                    .WithOne(o => o.PaymentType)
                    .HasForeignKey(o => o.PaymentTypeId);
            });

            builder.Entity<Product>(product =>
            {
                product.HasKey(p => p.Id);

                product.Property(p => p.Name)
                    .HasMaxLength(ModelsConstants.NameMaxLength)
                    .IsRequired();

                product.Property(p => p.Price)
                    .IsRequired();

                product.Property(p => p.PromoPrice)
                    .IsRequired(false);

                product.Property(p => p.CountsLeft)
                    .IsRequired();

                product.Property(p => p.Description)
                    .HasMaxLength(ModelsConstants.DescriptionMaxLength)
                    .IsRequired();

                product.Property(p => p.Specifications)
                    .IsRequired();

                product
                    .HasMany(p => p.Reviews)
                    .WithOne(r => r.Product)
                    .HasForeignKey(r => r.ProductId);

                product.HasMany(p => p.Questions)
                    .WithOne(q => q.Product)
                    .HasForeignKey(q => q.ProductId);

                product.HasMany(p => p.Orders)
                    .WithOne(op => op.Product)
                    .HasForeignKey(op => op.ProductId);

                product.HasOne(p => p.SubCategory)
                    .WithMany(sc => sc.Products)
                    .HasForeignKey(sc => sc.SubCategoryId);

                product.HasMany(p => p.Photos)
                    .WithOne(p => p.Product)
                    .HasForeignKey(p => p.ProductId);
            });

            builder.Entity<Question>(question =>
            {
                question.HasKey(q => q.Id);

                question.Property(q => q.Description)
                    .HasMaxLength(ModelsConstants.DescriptionMaxLength)
                    .IsRequired();

                question.Property(q => q.PostDate)
                    .IsRequired()
                    .HasDefaultValue(DateTime.Now);

                question.HasOne(q => q.Comment)
                    .WithMany(c => c.Questions)
                    .HasForeignKey(q => q.CommentId);

                question.HasOne(q => q.User)
                    .WithMany(u => u.Questions)
                    .HasForeignKey(q => q.UserId);

                question.HasOne(q => q.Product)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(q => q.ProductId);
            });

            builder.Entity<Review>(review =>
            {
                review.HasKey(r => r.Id);

                review.Property(r => r.Title)
                    .IsRequired()
                    .HasMaxLength(ModelsConstants.TitleMaxLength);

                review.Property(r => r.Description)
                    .IsRequired()
                    .HasMaxLength(ModelsConstants.DescriptionMaxLength);

                review.Property(r => r.StarsCount)
                    .IsRequired()
                    .HasMaxLength(5);

                review.Property(r => r.IsVerified)
                    .IsRequired();

                review.Property(r => r.LikesCount)
                    .IsRequired();

                review.Property(r => r.DislikesCount)
                    .IsRequired();

                review.Property(r => r.PostDate)
                    .IsRequired()
                    .HasDefaultValue(DateTime.Now);

                review.HasMany(r => r.Pictures)
                    .WithOne(rp => rp.Review)
                    .HasForeignKey(rp => rp.ReviewId);

                review.HasOne(r => r.Product)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(r => r.ProductId);

                review.HasOne(r => r.User)
                    .WithMany(u => u.Reviews)
                    .HasForeignKey(r => r.UserId);
            });

            builder.Entity<Photo>(photos =>
            {
                photos.HasKey(rp => rp.Id);

                photos.Property(rp => rp.Data)
                    .IsRequired();

                photos.HasOne(p => p.Product)
                    .WithMany(p => p.Photos)
                    .HasForeignKey(p => p.ProductId);

                photos.HasOne(p => p.Review)
                    .WithMany(r => r.Pictures)
                    .HasForeignKey(p => p.ReviewId);

                photos.HasOne(p => p.User)
                    .WithOne(u => u.ProfilePicture);
            });

            builder.Entity<Category>(category =>
            {
                category.HasKey(c => c.Id);

                category.Property(c => c.Name)
                    .IsRequired();

                category.HasMany(c => c.SubCategories)
                    .WithOne(sc => sc.Category)
                    .HasForeignKey(c => c.CategoryId);
            });

            builder.Entity<SubCategory>(subCategory =>
            {
                subCategory.HasKey(sc => sc.Id);

                subCategory.Property(sc => sc.Name)
                    .IsRequired();

                subCategory.HasOne(sc => sc.Category)
                    .WithMany(c => c.SubCategories)
                    .HasForeignKey(sc => sc.CategoryId);

                subCategory.HasMany(sc => sc.Products)
                    .WithOne(p => p.SubCategory)
                    .HasForeignKey(p => p.SubCategoryId);
            });

            builder.Entity<UserFavoriteProduct>(userFavoriteProduct =>
            {
                userFavoriteProduct.HasKey(ufp => new { ufp.ProductId, ufp.UserId });

                userFavoriteProduct.HasOne(ufp => ufp.User)
                    .WithMany(u => u.UserFavoriteProducts)
                    .HasForeignKey(ufp => ufp.UserId);

                userFavoriteProduct.HasOne(ufp => ufp.Product)
                    .WithMany(p => p.UserFavoriteProducts)
                    .HasForeignKey(ufp => ufp.ProductId);
            });
        }
    }
}
