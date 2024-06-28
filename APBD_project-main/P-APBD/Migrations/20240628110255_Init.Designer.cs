﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Projekt.Context;

#nullable disable

namespace Projekt.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240628110255_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Projekt.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Username");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("UserPassword");

                    b.Property<string>("UserRole")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("UserRole");

                    b.HasKey("Username");

                    b.ToTable("applicationUser", (string)null);
                });

            modelBuilder.Entity("Projekt.Entities.Company", b =>
                {
                    b.Property<int>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("CustomerId");

                    b.Property<string>("CompanyAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CompanyAddress");

                    b.Property<string>("CompanyEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CompanyEmail");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CompanyName");

                    b.Property<string>("CompanyPhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CompanyPhoneNumber");

                    b.Property<string>("RegistrationNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("RegistrationNumber");

                    b.HasKey("CustomerId");

                    b.ToTable("company", (string)null);
                });

            modelBuilder.Entity("Projekt.Entities.ContractPayment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PaymentId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Amount");

                    b.Property<int>("ContractId")
                        .HasColumnType("int")
                        .HasColumnName("ContractId");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("PaymentDate");

                    b.Property<string>("PaymentDescription")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PaymentDescription");

                    b.HasKey("PaymentId");

                    b.HasIndex("ContractId");

                    b.ToTable("contractPayment", (string)null);
                });

            modelBuilder.Entity("Projekt.Entities.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CustomerId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<bool>("IsDeprecated")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeprecated");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Type");

                    b.HasKey("CustomerId");

                    b.ToTable("customer", (string)null);
                });

            modelBuilder.Entity("Projekt.Entities.Individual", b =>
                {
                    b.Property<int>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("CustomerId");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("EmailAddress");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("FirstName");

                    b.Property<string>("HomeAddress")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("HomeAddress");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("LastName");

                    b.Property<string>("MobileNumber")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("MobileNumber");

                    b.Property<string>("NationalId")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("NationalId");

                    b.HasKey("CustomerId");

                    b.ToTable("individual", (string)null);
                });

            modelBuilder.Entity("Projekt.Entities.Promotion", b =>
                {
                    b.Property<int>("PromotionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PromotionId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PromotionId"));

                    b.Property<int>("DiscountValue")
                        .HasColumnType("int")
                        .HasColumnName("DiscountValue");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("EndDate");

                    b.Property<string>("PromotionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PromotionName");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("StartDate");

                    b.HasKey("PromotionId");

                    b.ToTable("promotion", (string)null);
                });

            modelBuilder.Entity("Projekt.Entities.ServiceSubscription", b =>
                {
                    b.Property<int>("SubscriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SubscriptionId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubscriptionId"));

                    b.Property<int>("RenewalPeriodInMonths")
                        .HasColumnType("int")
                        .HasColumnName("RenewalPeriodInMonths");

                    b.Property<int>("SoftwareId")
                        .HasColumnType("int")
                        .HasColumnName("SoftwareId");

                    b.Property<decimal>("SubscriptionCost")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("SubscriptionCost");

                    b.Property<string>("SubscriptionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("SubscriptionName");

                    b.HasKey("SubscriptionId");

                    b.HasIndex("SoftwareId");

                    b.ToTable("serviceSubscription", (string)null);
                });

            modelBuilder.Entity("Projekt.Entities.Software", b =>
                {
                    b.Property<int>("SoftwareId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SoftwareId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SoftwareId"));

                    b.Property<decimal>("AnnualCost")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("AnnualCost");

                    b.Property<string>("SoftwareDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("SoftwareDescription");

                    b.Property<string>("SoftwareName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("SoftwareName");

                    b.Property<string>("SoftwareVersion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("SoftwareVersion");

                    b.HasKey("SoftwareId");

                    b.ToTable("software", (string)null);
                });

            modelBuilder.Entity("Projekt.Entities.SoftwareContract", b =>
                {
                    b.Property<int>("ContractId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ContractId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContractId"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("CustomerId");

                    b.Property<int?>("DiscountValue")
                        .HasColumnType("int")
                        .HasColumnName("DiscountValue");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("EndDate");

                    b.Property<int?>("PromotionId")
                        .HasColumnType("int")
                        .HasColumnName("PromotionId");

                    b.Property<int>("SoftwareId")
                        .HasColumnType("int")
                        .HasColumnName("SoftwareId");

                    b.Property<string>("SoftwareVersion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("SoftwareVersion");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("StartDate");

                    b.Property<int>("SupportDuration")
                        .HasColumnType("int")
                        .HasColumnName("SupportDuration");

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("TotalCost");

                    b.Property<string>("UpdateDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("UpdateDescription");

                    b.HasKey("ContractId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PromotionId");

                    b.HasIndex("SoftwareId");

                    b.ToTable("softwareContract", (string)null);
                });

            modelBuilder.Entity("Projekt.Entities.Company", b =>
                {
                    b.HasOne("Projekt.Entities.Customer", "Customer")
                        .WithOne("Company")
                        .HasForeignKey("Projekt.Entities.Company", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Projekt.Entities.ContractPayment", b =>
                {
                    b.HasOne("Projekt.Entities.SoftwareContract", "SoftwareContract")
                        .WithMany("ContractPayments")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SoftwareContract");
                });

            modelBuilder.Entity("Projekt.Entities.Individual", b =>
                {
                    b.HasOne("Projekt.Entities.Customer", "Customer")
                        .WithOne("Individual")
                        .HasForeignKey("Projekt.Entities.Individual", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Projekt.Entities.ServiceSubscription", b =>
                {
                    b.HasOne("Projekt.Entities.Software", "Software")
                        .WithMany("ServiceSubscriptions")
                        .HasForeignKey("SoftwareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Software");
                });

            modelBuilder.Entity("Projekt.Entities.SoftwareContract", b =>
                {
                    b.HasOne("Projekt.Entities.Customer", "Customer")
                        .WithMany("SoftwareContracts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Projekt.Entities.Promotion", "Promotion")
                        .WithMany("SoftwareContracts")
                        .HasForeignKey("PromotionId");

                    b.HasOne("Projekt.Entities.Software", "Software")
                        .WithMany("SoftwareContracts")
                        .HasForeignKey("SoftwareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Promotion");

                    b.Navigation("Software");
                });

            modelBuilder.Entity("Projekt.Entities.Customer", b =>
                {
                    b.Navigation("Company")
                        .IsRequired();

                    b.Navigation("Individual")
                        .IsRequired();

                    b.Navigation("SoftwareContracts");
                });

            modelBuilder.Entity("Projekt.Entities.Promotion", b =>
                {
                    b.Navigation("SoftwareContracts");
                });

            modelBuilder.Entity("Projekt.Entities.Software", b =>
                {
                    b.Navigation("ServiceSubscriptions");

                    b.Navigation("SoftwareContracts");
                });

            modelBuilder.Entity("Projekt.Entities.SoftwareContract", b =>
                {
                    b.Navigation("ContractPayments");
                });
#pragma warning restore 612, 618
        }
    }
}