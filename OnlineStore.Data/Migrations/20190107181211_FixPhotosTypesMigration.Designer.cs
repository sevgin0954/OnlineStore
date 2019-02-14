﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineStore.Data;

namespace OnlineStore.Data.Migrations
{
    [DbContext(typeof(OnlineStoreDbContext))]
    [Migration("20190107181211_FixPhotosTypesMigration")]
    partial class FixPhotosTypesMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("OnlineStore.Models.Category", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("OnlineStore.Models.Comment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(10000);

                    b.Property<int>("DislikesCount");

                    b.Property<bool>("IsVerified");

                    b.Property<int>("LikesCount");

                    b.Property<DateTime>("PostDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2019, 1, 7, 20, 12, 10, 50, DateTimeKind.Local).AddTicks(7921));

                    b.Property<string>("ReviewId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ReviewId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("OnlineStore.Models.DeliveryInfo", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<string>("DistrictId");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("PopulatedPlaceId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.HasIndex("PopulatedPlaceId");

                    b.HasIndex("UserId");

                    b.ToTable("DeliverysInfos");
                });

            modelBuilder.Entity("OnlineStore.Models.DeliveryType", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("DeliverysTypes");
                });

            modelBuilder.Entity("OnlineStore.Models.District", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("OnlineStore.Models.Order", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DeliveryExpectedTime")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2019, 1, 10, 20, 12, 10, 133, DateTimeKind.Local).AddTicks(4929));

                    b.Property<string>("DeliveryInfoId");

                    b.Property<decimal>("DeliveryPrice");

                    b.Property<string>("DeliveryTypeId");

                    b.Property<DateTime>("OrderDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2019, 1, 7, 20, 12, 10, 131, DateTimeKind.Local).AddTicks(203));

                    b.Property<string>("OrderStatusId");

                    b.Property<string>("PaymentTypeId");

                    b.Property<decimal>("TotalPrice");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("DeliveryInfoId");

                    b.HasIndex("DeliveryTypeId");

                    b.HasIndex("OrderStatusId");

                    b.HasIndex("PaymentTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("OnlineStore.Models.OrderProduct", b =>
                {
                    b.Property<string>("OrderId");

                    b.Property<string>("ProductId");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProducts");
                });

            modelBuilder.Entity("OnlineStore.Models.OrderStatus", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("OrdersStatuses");
                });

            modelBuilder.Entity("OnlineStore.Models.PaymentType", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("PaymentTypes");
                });

            modelBuilder.Entity("OnlineStore.Models.Photo", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Data")
                        .IsRequired();

                    b.Property<string>("ProductId");

                    b.Property<string>("ReviewId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("ReviewId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("OnlineStore.Models.PopulatedPlace", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DistrictId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.ToTable("PopulatedPlaces");
                });

            modelBuilder.Entity("OnlineStore.Models.Product", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CountsLeft");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(10000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("OrderQuantity")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(1);

                    b.Property<decimal>("Price");

                    b.Property<decimal?>("PromoPrice");

                    b.Property<string>("Specifications")
                        .IsRequired();

                    b.Property<string>("SubCategoryId");

                    b.HasKey("Id");

                    b.HasIndex("SubCategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("OnlineStore.Models.Question", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CommentId");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(10000);

                    b.Property<DateTime>("PostDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2019, 1, 7, 20, 12, 10, 189, DateTimeKind.Local).AddTicks(7042));

                    b.Property<string>("ProductId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("OnlineStore.Models.Review", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(10000);

                    b.Property<int>("DislikesCount");

                    b.Property<bool>("IsVerified");

                    b.Property<int>("LikesCount");

                    b.Property<DateTime>("PostDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2019, 1, 7, 20, 12, 10, 202, DateTimeKind.Local).AddTicks(6621));

                    b.Property<string>("ProductId");

                    b.Property<int>("StarsCount")
                        .HasMaxLength(5);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("OnlineStore.Models.SubCategory", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("SubCategories");
                });

            modelBuilder.Entity("OnlineStore.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FullName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("PhotoId");

                    b.Property<DateTime>("RegisterDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2019, 1, 7, 18, 12, 10, 26, DateTimeKind.Utc).AddTicks(2808));

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("PhotoId")
                        .IsUnique()
                        .HasFilter("[PhotoId] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("OnlineStore.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("OnlineStore.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OnlineStore.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("OnlineStore.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OnlineStore.Models.Comment", b =>
                {
                    b.HasOne("OnlineStore.Models.Review", "Review")
                        .WithMany("Comments")
                        .HasForeignKey("ReviewId");

                    b.HasOne("OnlineStore.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("OnlineStore.Models.DeliveryInfo", b =>
                {
                    b.HasOne("OnlineStore.Models.District", "District")
                        .WithMany("DeliverysInfos")
                        .HasForeignKey("DistrictId");

                    b.HasOne("OnlineStore.Models.PopulatedPlace", "PopulatedPlace")
                        .WithMany("DeliveryInfos")
                        .HasForeignKey("PopulatedPlaceId");

                    b.HasOne("OnlineStore.Models.User")
                        .WithMany("DeliveryInfos")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("OnlineStore.Models.Order", b =>
                {
                    b.HasOne("OnlineStore.Models.DeliveryInfo", "DeliveryInfo")
                        .WithMany("Orders")
                        .HasForeignKey("DeliveryInfoId");

                    b.HasOne("OnlineStore.Models.DeliveryType", "DeliveryType")
                        .WithMany("Orders")
                        .HasForeignKey("DeliveryTypeId");

                    b.HasOne("OnlineStore.Models.OrderStatus", "OrderStatus")
                        .WithMany("Orders")
                        .HasForeignKey("OrderStatusId");

                    b.HasOne("OnlineStore.Models.PaymentType", "PaymentType")
                        .WithMany("Orders")
                        .HasForeignKey("PaymentTypeId");

                    b.HasOne("OnlineStore.Models.User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("OnlineStore.Models.OrderProduct", b =>
                {
                    b.HasOne("OnlineStore.Models.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OnlineStore.Models.Product", "Product")
                        .WithMany("Orders")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OnlineStore.Models.Photo", b =>
                {
                    b.HasOne("OnlineStore.Models.Product", "Product")
                        .WithMany("Photos")
                        .HasForeignKey("ProductId");

                    b.HasOne("OnlineStore.Models.Review", "Review")
                        .WithMany("Pictures")
                        .HasForeignKey("ReviewId");
                });

            modelBuilder.Entity("OnlineStore.Models.PopulatedPlace", b =>
                {
                    b.HasOne("OnlineStore.Models.District", "District")
                        .WithMany("PopulatedPlaces")
                        .HasForeignKey("DistrictId");
                });

            modelBuilder.Entity("OnlineStore.Models.Product", b =>
                {
                    b.HasOne("OnlineStore.Models.SubCategory", "SubCategory")
                        .WithMany("Products")
                        .HasForeignKey("SubCategoryId");
                });

            modelBuilder.Entity("OnlineStore.Models.Question", b =>
                {
                    b.HasOne("OnlineStore.Models.Comment", "Comment")
                        .WithMany("Questions")
                        .HasForeignKey("CommentId");

                    b.HasOne("OnlineStore.Models.Product", "Product")
                        .WithMany("Questions")
                        .HasForeignKey("ProductId");

                    b.HasOne("OnlineStore.Models.User", "User")
                        .WithMany("Questions")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("OnlineStore.Models.Review", b =>
                {
                    b.HasOne("OnlineStore.Models.Product", "Product")
                        .WithMany("Reviews")
                        .HasForeignKey("ProductId");

                    b.HasOne("OnlineStore.Models.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("OnlineStore.Models.SubCategory", b =>
                {
                    b.HasOne("OnlineStore.Models.Category", "Category")
                        .WithMany("SubCategories")
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("OnlineStore.Models.User", b =>
                {
                    b.HasOne("OnlineStore.Models.Photo", "ProfilePicture")
                        .WithOne("User")
                        .HasForeignKey("OnlineStore.Models.User", "PhotoId");
                });
#pragma warning restore 612, 618
        }
    }
}
