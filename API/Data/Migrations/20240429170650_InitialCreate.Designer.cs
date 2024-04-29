﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Data.Migrations
{
    [DbContext(typeof(PaymentProcessorDbContext))]
    [Migration("20240429170650_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("API.Entities.Benefit", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Category")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MerchantID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("MerchantID");

                    b.ToTable("Benefits");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Category = 0,
                            MerchantID = 1,
                            Name = "Discounted Meal",
                            Price = 500m
                        },
                        new
                        {
                            ID = 2,
                            Category = 1,
                            MerchantID = 2,
                            Name = "Gym Membership",
                            Price = 1500m
                        },
                        new
                        {
                            ID = 3,
                            Category = 3,
                            MerchantID = 3,
                            Name = "Museum Ticket",
                            Price = 1000m
                        },
                        new
                        {
                            ID = 4,
                            Category = 2,
                            MerchantID = 4,
                            Name = "Online Course",
                            Price = 10000m
                        });
                });

            modelBuilder.Entity("API.Entities.Card", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Balance")
                        .HasColumnType("TEXT");

                    b.Property<string>("CardNumber")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("Cards");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Balance = 120000m,
                            CardNumber = "1234567890123456",
                            ExpiryDate = new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserID = 1
                        },
                        new
                        {
                            ID = 2,
                            Balance = 22000m,
                            CardNumber = "9876543210987654",
                            ExpiryDate = new DateTime(2024, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserID = 2
                        },
                        new
                        {
                            ID = 3,
                            Balance = 1600m,
                            CardNumber = "5678901234567890",
                            ExpiryDate = new DateTime(2023, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserID = 3
                        },
                        new
                        {
                            ID = 4,
                            Balance = 5000m,
                            CardNumber = "3210987654321098",
                            ExpiryDate = new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserID = 4
                        },
                        new
                        {
                            ID = 5,
                            Balance = 10000m,
                            CardNumber = "4567890123456789",
                            ExpiryDate = new DateTime(2024, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserID = 5
                        },
                        new
                        {
                            ID = 6,
                            Balance = 9700m,
                            CardNumber = "9012345678901234",
                            ExpiryDate = new DateTime(2023, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserID = 6
                        });
                });

            modelBuilder.Entity("API.Entities.CustomerCompany", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BenefitCategoryForStandardUsers")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("CustomerCompanies");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            BenefitCategoryForStandardUsers = 0,
                            Name = "Novatix"
                        },
                        new
                        {
                            ID = 2,
                            BenefitCategoryForStandardUsers = 1,
                            Name = "CodeTech"
                        },
                        new
                        {
                            ID = 3,
                            BenefitCategoryForStandardUsers = 5,
                            Name = "Simplify"
                        });
                });

            modelBuilder.Entity("API.Entities.Merchant", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Category")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CustomerCompanyID")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("DiscountForPlatinumUsers")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("CustomerCompanyID");

                    b.ToTable("Merchants");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Category = 0,
                            DiscountForPlatinumUsers = 0.10m,
                            Name = "Pause"
                        },
                        new
                        {
                            ID = 2,
                            Category = 1,
                            DiscountForPlatinumUsers = 0.15m,
                            Name = "MegaGym"
                        },
                        new
                        {
                            ID = 3,
                            Category = 3,
                            DiscountForPlatinumUsers = 0.20m,
                            Name = "Museum of Contemporary Art"
                        },
                        new
                        {
                            ID = 4,
                            Category = 2,
                            DiscountForPlatinumUsers = 0.25m,
                            Name = "Educons Online Learning "
                        });
                });

            modelBuilder.Entity("API.Entities.Transaction", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<int>("CardID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsSuccessful")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MerchantID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("CardID");

                    b.HasIndex("MerchantID");

                    b.ToTable("Transactions");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Amount = 16000m,
                            CardID = 1,
                            DateTime = new DateTime(2024, 4, 29, 19, 6, 48, 844, DateTimeKind.Local).AddTicks(770),
                            IsSuccessful = true,
                            MerchantID = 1
                        },
                        new
                        {
                            ID = 2,
                            Amount = 250m,
                            CardID = 2,
                            DateTime = new DateTime(2024, 4, 29, 19, 6, 48, 844, DateTimeKind.Local).AddTicks(858),
                            IsSuccessful = true,
                            MerchantID = 2
                        },
                        new
                        {
                            ID = 3,
                            Amount = 10000.55m,
                            CardID = 3,
                            DateTime = new DateTime(2024, 4, 29, 19, 6, 48, 844, DateTimeKind.Local).AddTicks(868),
                            IsSuccessful = true,
                            MerchantID = 3
                        },
                        new
                        {
                            ID = 4,
                            Amount = 1500.12m,
                            CardID = 4,
                            DateTime = new DateTime(2024, 4, 29, 19, 6, 48, 844, DateTimeKind.Local).AddTicks(876),
                            IsSuccessful = false,
                            MerchantID = 4
                        });
                });

            modelBuilder.Entity("API.Entities.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CustomerCompanyID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserType")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("CustomerCompanyID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            CustomerCompanyID = 1,
                            Email = "petar.maric@gmail.com",
                            FirstName = "Petar",
                            LastName = "Maric",
                            UserType = 0
                        },
                        new
                        {
                            ID = 2,
                            CustomerCompanyID = 1,
                            Email = "milos.saric@gmail.com",
                            FirstName = "Milos",
                            LastName = "Saric",
                            UserType = 0
                        },
                        new
                        {
                            ID = 3,
                            CustomerCompanyID = 3,
                            Email = "nedeljko.rac@yahoo.com",
                            FirstName = "Nedeljko",
                            LastName = "Racic",
                            UserType = 1
                        },
                        new
                        {
                            ID = 4,
                            CustomerCompanyID = 2,
                            Email = "stefan.perovic@yahoo.com",
                            FirstName = "Stefan",
                            LastName = "Perovic",
                            UserType = 1
                        },
                        new
                        {
                            ID = 5,
                            CustomerCompanyID = 1,
                            Email = "mitarjovic@gmail.com",
                            FirstName = "Mitar",
                            LastName = "Jovic",
                            UserType = 2
                        },
                        new
                        {
                            ID = 6,
                            CustomerCompanyID = 2,
                            Email = "rista88@gmail.com",
                            FirstName = "Rista",
                            LastName = "Gocic",
                            UserType = 2
                        });
                });

            modelBuilder.Entity("API.Entities.Benefit", b =>
                {
                    b.HasOne("API.Entities.Merchant", "Merchant")
                        .WithMany("Benefits")
                        .HasForeignKey("MerchantID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Merchant");
                });

            modelBuilder.Entity("API.Entities.Card", b =>
                {
                    b.HasOne("API.Entities.User", "User")
                        .WithOne("Card")
                        .HasForeignKey("API.Entities.Card", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("API.Entities.Merchant", b =>
                {
                    b.HasOne("API.Entities.CustomerCompany", null)
                        .WithMany("MerchantsWithDiscountForPlatinumUsers")
                        .HasForeignKey("CustomerCompanyID");
                });

            modelBuilder.Entity("API.Entities.Transaction", b =>
                {
                    b.HasOne("API.Entities.Card", "Card")
                        .WithMany()
                        .HasForeignKey("CardID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Merchant", "Merchant")
                        .WithMany()
                        .HasForeignKey("MerchantID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("Merchant");
                });

            modelBuilder.Entity("API.Entities.User", b =>
                {
                    b.HasOne("API.Entities.CustomerCompany", "CustomerCompany")
                        .WithMany("Employees")
                        .HasForeignKey("CustomerCompanyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CustomerCompany");
                });

            modelBuilder.Entity("API.Entities.CustomerCompany", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("MerchantsWithDiscountForPlatinumUsers");
                });

            modelBuilder.Entity("API.Entities.Merchant", b =>
                {
                    b.Navigation("Benefits");
                });

            modelBuilder.Entity("API.Entities.User", b =>
                {
                    b.Navigation("Card");
                });
#pragma warning restore 612, 618
        }
    }
}
