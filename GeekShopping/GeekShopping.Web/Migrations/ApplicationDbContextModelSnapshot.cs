﻿// <auto-generated />
using System;
using GeekShopping.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GeekShopping.Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("GeekShopping.Web.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("GeekShopping.Web.Models.CartDetail", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<long>("CartHeaderId")
                        .HasColumnType("bigint");

                    b.Property<int>("Count")
                        .HasColumnType("int")
                        .HasColumnName("count");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CartHeaderId");

                    b.HasIndex("ProductId");

                    b.ToTable("cart_detail");
                });

            modelBuilder.Entity("GeekShopping.Web.Models.CartHeader", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("CouponCode")
                        .HasColumnType("longtext")
                        .HasColumnName("coupon_code");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.ToTable("cart_header");
                });

            modelBuilder.Entity("GeekShopping.Web.Models.Coupon", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("code");

                    b.Property<decimal>("DiscountAmount")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("discount_amount");

                    b.HasKey("Id");

                    b.ToTable("coupon");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Code = "ERUDIO_2022_10",
                            DiscountAmount = 10m
                        },
                        new
                        {
                            Id = 2L,
                            Code = "ERUDIO_2022_15",
                            DiscountAmount = 15m
                        });
                });

            modelBuilder.Entity("GeekShopping.Web.Models.OrderDetail", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<int>("Count")
                        .HasColumnType("int")
                        .HasColumnName("count");

                    b.Property<long>("OrderHeaderId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("price");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint")
                        .HasColumnName("product_id");

                    b.Property<string>("ProductName")
                        .HasColumnType("longtext")
                        .HasColumnName("product_name");

                    b.HasKey("Id");

                    b.HasIndex("OrderHeaderId");

                    b.ToTable("order_detail");
                });

            modelBuilder.Entity("GeekShopping.Web.Models.OrderHeader", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("CVV")
                        .HasColumnType("longtext")
                        .HasColumnName("cvv");

                    b.Property<string>("CardNumber")
                        .HasColumnType("longtext")
                        .HasColumnName("card_number");

                    b.Property<int>("CartTotalItems")
                        .HasColumnType("int")
                        .HasColumnName("total_items");

                    b.Property<string>("CouponCode")
                        .HasColumnType("longtext")
                        .HasColumnName("coupon_code");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("purchase_date");

                    b.Property<decimal>("DiscountAmount")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("discount_amount");

                    b.Property<string>("Email")
                        .HasColumnType("longtext")
                        .HasColumnName("email");

                    b.Property<string>("ExpireDate")
                        .HasColumnType("longtext")
                        .HasColumnName("expire_date");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext")
                        .HasColumnName("last_name");

                    b.Property<DateTime>("OrderTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("order_time");

                    b.Property<bool>("PaymentStatus")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("payment_status");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext")
                        .HasColumnName("phone_number");

                    b.Property<decimal>("PurchaseAmount")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("purchase_amount");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.ToTable("order_header");
                });

            modelBuilder.Entity("GeekShopping.Web.Models.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("CategoryName")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("category_name");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("description");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)")
                        .HasColumnName("image_url");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)")
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("price");

                    b.HasKey("Id");

                    b.ToTable("product");

                    b.HasData(
                        new
                        {
                            Id = 2L,
                            CategoryName = "T-shirt",
                            Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.<br/>The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.<br/>Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy.",
                            ImageUrl = "https://github.com/PedroPadilhaPortella/Arquitetura_de_Microsservicos_com_Dotnet/blob/main/ShoppingImages/2_no_internet.jpg?raw=true",
                            Name = "Camiseta No Internet",
                            Price = 69.9m
                        },
                        new
                        {
                            Id = 3L,
                            CategoryName = "Action Figure",
                            Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.<br/>The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.<br/>Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy.",
                            ImageUrl = "https://github.com/PedroPadilhaPortella/Arquitetura_de_Microsservicos_com_Dotnet/blob/main/ShoppingImages/3_vader.jpg?raw=true",
                            Name = "Capacete Darth Vader Star Wars Black Series",
                            Price = 999.99m
                        },
                        new
                        {
                            Id = 4L,
                            CategoryName = "Action Figure",
                            Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.<br/>The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.<br/>Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy.",
                            ImageUrl = "https://github.com/PedroPadilhaPortella/Arquitetura_de_Microsservicos_com_Dotnet/blob/main/ShoppingImages/4_storm_tropper.jpg?raw=true",
                            Name = "Star Wars The Black Series Hasbro - Stormtrooper Imperial",
                            Price = 189.99m
                        },
                        new
                        {
                            Id = 5L,
                            CategoryName = "T-shirt",
                            Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.<br/>The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.<br/>Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy.",
                            ImageUrl = "https://github.com/PedroPadilhaPortella/Arquitetura_de_Microsservicos_com_Dotnet/blob/main/ShoppingImages/5_100_gamer.jpg?raw=true",
                            Name = "Camiseta Gamer",
                            Price = 69.99m
                        },
                        new
                        {
                            Id = 6L,
                            CategoryName = "T-shirt",
                            Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.<br/>The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.<br/>Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy.",
                            ImageUrl = "https://github.com/PedroPadilhaPortella/Arquitetura_de_Microsservicos_com_Dotnet/blob/main/ShoppingImages/6_spacex.jpg?raw=true",
                            Name = "Camiseta SpaceX",
                            Price = 49.99m
                        },
                        new
                        {
                            Id = 7L,
                            CategoryName = "T-shirt",
                            Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.<br/>The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.<br/>Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy.",
                            ImageUrl = "https://github.com/PedroPadilhaPortella/Arquitetura_de_Microsservicos_com_Dotnet/blob/main/ShoppingImages/7_coffee.jpg?raw=true",
                            Name = "Camiseta Feminina Coffee Benefits",
                            Price = 69.9m
                        },
                        new
                        {
                            Id = 8L,
                            CategoryName = "Sweatshirt",
                            Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.<br/>The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.<br/>Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy.",
                            ImageUrl = "https://github.com/PedroPadilhaPortella/Arquitetura_de_Microsservicos_com_Dotnet/blob/main/ShoppingImages/8_moletom_cobra_kay.jpg?raw=true",
                            Name = "Moletom Com Capuz Cobra Kai",
                            Price = 159.9m
                        },
                        new
                        {
                            Id = 9L,
                            CategoryName = "Book",
                            Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.<br/>The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.<br/>Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy.",
                            ImageUrl = "https://github.com/PedroPadilhaPortella/Arquitetura_de_Microsservicos_com_Dotnet/blob/main/ShoppingImages/9_neil.jpg?raw=true",
                            Name = "Livro Star Talk – Neil DeGrasse Tyson",
                            Price = 49.9m
                        },
                        new
                        {
                            Id = 10L,
                            CategoryName = "Action Figure",
                            Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.<br/>The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.<br/>Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy.",
                            ImageUrl = "https://github.com/PedroPadilhaPortella/Arquitetura_de_Microsservicos_com_Dotnet/blob/main/ShoppingImages/10_milennium_falcon.jpg?raw=true",
                            Name = "Star Wars Mission Fleet Han Solo Nave Milennium Falcon",
                            Price = 359.99m
                        },
                        new
                        {
                            Id = 11L,
                            CategoryName = "T-shirt",
                            Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.<br/>The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.<br/>Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy.",
                            ImageUrl = "https://github.com/PedroPadilhaPortella/Arquitetura_de_Microsservicos_com_Dotnet/blob/main/ShoppingImages/11_mars.jpg?raw=true",
                            Name = "Camiseta Elon Musk Spacex Marte Occupy Mars",
                            Price = 59.99m
                        },
                        new
                        {
                            Id = 12L,
                            CategoryName = "T-shirt",
                            Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.<br/>The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.<br/>Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy.",
                            ImageUrl = "https://github.com/PedroPadilhaPortella/Arquitetura_de_Microsservicos_com_Dotnet/blob/main/ShoppingImages/12_gnu_linux.jpg?raw=true",
                            Name = "Camiseta GNU Linux Programador Masculina",
                            Price = 59.99m
                        },
                        new
                        {
                            Id = 13L,
                            CategoryName = "T-shirt",
                            Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.<br/>The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.<br/>Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy.",
                            ImageUrl = "https://github.com/PedroPadilhaPortella/Arquitetura_de_Microsservicos_com_Dotnet/blob/main/ShoppingImages/13_dragon_ball.jpg",
                            Name = "Camiseta Goku Fases",
                            Price = 59.99m
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("GeekShopping.Web.Models.CartDetail", b =>
                {
                    b.HasOne("GeekShopping.Web.Models.CartHeader", "CartHeader")
                        .WithMany()
                        .HasForeignKey("CartHeaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GeekShopping.Web.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CartHeader");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("GeekShopping.Web.Models.OrderDetail", b =>
                {
                    b.HasOne("GeekShopping.Web.Models.OrderHeader", "OrderHeader")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderHeaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderHeader");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("GeekShopping.Web.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("GeekShopping.Web.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GeekShopping.Web.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("GeekShopping.Web.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GeekShopping.Web.Models.OrderHeader", b =>
                {
                    b.Navigation("OrderDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
