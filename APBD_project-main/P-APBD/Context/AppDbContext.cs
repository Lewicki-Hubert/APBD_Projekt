using Microsoft.EntityFrameworkCore;
using Projekt.Models.Entities;

namespace Projekt.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Individual> Individuals { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Software> Softwares { get; set; }
        public virtual DbSet<Promotion> Promotions { get; set; }
        public virtual DbSet<SoftwareContract> SoftwareContracts { get; set; }
        public virtual DbSet<ContractPayment> ContractPayments { get; set; }
        public virtual DbSet<ServiceSubscription> ServiceSubscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Individual>(entity =>
            {
                entity.HasKey(e => e.CustomerId);
                entity.ToTable("individual");
                entity.Property(e => e.CustomerId).HasColumnName("CustomerId");
                entity.Property(e => e.FirstName).HasColumnName("FirstName");
                entity.Property(e => e.LastName).HasColumnName("LastName");
                entity.Property(e => e.HomeAddress).HasColumnName("HomeAddress");
                entity.Property(e => e.EmailAddress).HasColumnName("EmailAddress");
                entity.Property(e => e.MobileNumber).HasColumnName("MobileNumber");
                entity.Property(e => e.NationalId).HasColumnName("NationalId");

                entity.HasOne(d => d.Customer)
                    .WithOne(p => p.Individual)
                    .HasForeignKey<Individual>(d => d.CustomerId);
            });
            
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);
                entity.ToTable("customer");
                entity.Property(e => e.CustomerId).HasColumnName("CustomerId");
                entity.Property(e => e.Type).HasConversion<string>().HasColumnName("Type");
                entity.Property(e => e.IsDeprecated).HasColumnName("IsDeprecated");
            });
            
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.CustomerId);
                entity.ToTable("company");
                entity.Property(e => e.CustomerId).HasColumnName("CustomerId");
                entity.Property(e => e.CompanyName).HasColumnName("CompanyName");
                entity.Property(e => e.CompanyAddress).HasColumnName("CompanyAddress");
                entity.Property(e => e.CompanyEmail).HasColumnName("CompanyEmail");
                entity.Property(e => e.CompanyPhoneNumber).HasColumnName("CompanyPhoneNumber");
                entity.Property(e => e.RegistrationNumber).HasColumnName("RegistrationNumber");
                entity.HasOne(d => d.Customer).WithOne(p => p.Company)
                    .HasForeignKey<Company>(d => d.CustomerId);
            });
            
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasKey(e => e.Username);
                entity.ToTable("applicationUser");
                entity.Property(e => e.Username).HasColumnName("Username");
                entity.Property(e => e.UserPassword).HasColumnName("UserPassword");
                entity.Property(e => e.UserRole).HasConversion<string>().HasColumnName("UserRole");
            });
            
            
            modelBuilder.Entity<Software>(entity =>
            {
                entity.HasKey(e => e.SoftwareId);
                entity.ToTable("software");
                entity.Property(e => e.SoftwareId).HasColumnName("SoftwareId");
                entity.Property(e => e.SoftwareName).HasColumnName("SoftwareName");
                entity.Property(e => e.SoftwareDescription).HasColumnName("SoftwareDescription");
                entity.Property(e => e.SoftwareVersion).HasColumnName("SoftwareVersion");
                entity.Property(e => e.AnnualCost).HasColumnName("AnnualCost");
            });

            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.HasKey(e => e.PromotionId);
                entity.ToTable("promotion");
                entity.Property(e => e.PromotionId).HasColumnName("PromotionId");
                entity.Property(e => e.PromotionName).HasColumnName("PromotionName");
                entity.Property(e => e.DiscountValue).HasColumnName("DiscountValue");
                entity.Property(e => e.StartDate).HasColumnName("StartDate");
                entity.Property(e => e.EndDate).HasColumnName("EndDate");
            });
            
            modelBuilder.Entity<ContractPayment>(entity =>
            {
                entity.HasKey(e => e.PaymentId);
                entity.ToTable("contractPayment");
                entity.Property(e => e.PaymentId).HasColumnName("PaymentId");
                entity.Property(e => e.ContractId).HasColumnName("ContractId");
                entity.Property(e => e.PaymentDescription).HasColumnName("PaymentDescription");
                entity.Property(e => e.PaymentDate).HasColumnName("PaymentDate");
                entity.Property(e => e.Amount).HasColumnName("Amount");

                entity.HasOne(d => d.SoftwareContract)
                    .WithMany(p => p.ContractPayments)
                    .HasForeignKey(d => d.ContractId);
            });
            
            modelBuilder.Entity<SoftwareContract>(entity =>
            {
                entity.HasKey(e => e.ContractId);
                entity.ToTable("softwareContract");
                entity.Property(e => e.ContractId).HasColumnName("ContractId");
                entity.Property(e => e.CustomerId).HasColumnName("CustomerId");
                entity.Property(e => e.SoftwareId).HasColumnName("SoftwareId");
                entity.Property(e => e.SoftwareVersion).HasColumnName("SoftwareVersion");
                entity.Property(e => e.StartDate).HasColumnName("StartDate");
                entity.Property(e => e.EndDate).HasColumnName("EndDate");
                entity.Property(e => e.UpdateDescription).HasColumnName("UpdateDescription");
                entity.Property(e => e.SupportDuration).HasColumnName("SupportDuration");
                entity.Property(e => e.PromotionId).HasColumnName("PromotionId");
                entity.Property(e => e.DiscountValue).HasColumnName("DiscountValue");
                entity.Property(e => e.TotalCost).HasColumnName("TotalCost");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.SoftwareContracts)
                    .HasForeignKey(d => d.CustomerId);

                entity.HasOne(d => d.Software)
                    .WithMany(p => p.SoftwareContracts)
                    .HasForeignKey(d => d.SoftwareId);

                entity.HasOne(d => d.Promotion)
                    .WithMany(p => p.SoftwareContracts)
                    .HasForeignKey(d => d.PromotionId);
            });
            

            modelBuilder.Entity<ServiceSubscription>(entity =>
            {
                entity.HasKey(e => e.SubscriptionId);
                entity.ToTable("serviceSubscription");
                entity.Property(e => e.SubscriptionId).HasColumnName("SubscriptionId");
                entity.Property(e => e.SoftwareId).HasColumnName("SoftwareId");
                entity.Property(e => e.SubscriptionName).HasColumnName("SubscriptionName");
                entity.Property(e => e.RenewalPeriodInMonths).HasColumnName("RenewalPeriodInMonths");
                entity.Property(e => e.SubscriptionCost).HasColumnName("SubscriptionCost");

                entity.HasOne(d => d.Software)
                    .WithMany(p => p.ServiceSubscriptions)
                    .HasForeignKey(d => d.SoftwareId);
            });
        }
    }
}
