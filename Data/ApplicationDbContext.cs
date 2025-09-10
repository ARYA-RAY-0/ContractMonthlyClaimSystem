using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ContractMonthlyClaimSystem.Models;

namespace ContractMonthlyClaimSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Claim> Claims { get; set; }
        public DbSet<ClaimItem> ClaimItems { get; set; }
        public DbSet<SupportingDocument> SupportingDocuments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Necessary for Identity

            // Configure the Claim entity
            modelBuilder.Entity<Claim>(entity =>
            {
                // Configure decimal precision for TotalAmount
                entity.Property(e => e.TotalAmount)
                      .HasPrecision(18, 2); // 18 total digits, 2 decimal places

                // Configure relationship: Claim has one User
                entity.HasOne(c => c.User)
                      .WithMany() // User can have many Claims
                      .HasForeignKey(c => c.UserId)
                      .OnDelete(DeleteBehavior.Cascade); // Delete claims if user is deleted
            });

            // Configure the ClaimItem entity
            modelBuilder.Entity<ClaimItem>(entity =>
            {
                // Configure relationship: ClaimItem belongs to one Claim
                entity.HasOne(ci => ci.Claim)
                      .WithMany(c => c.ClaimItems) // Claim has many ClaimItems
                      .HasForeignKey(ci => ci.ClaimId)
                      .OnDelete(DeleteBehavior.Cascade); // Delete items if claim is deleted
            });

            // Configure the SupportingDocument entity
            modelBuilder.Entity<SupportingDocument>(entity =>
            {
                // Configure relationship: SupportingDocument belongs to one Claim
                entity.HasOne(sd => sd.Claim)
                      .WithMany(c => c.SupportingDocuments) // Claim has many SupportingDocuments
                      .HasForeignKey(sd => sd.ClaimId)
                      .OnDelete(DeleteBehavior.Cascade); // Delete documents if claim is deleted
            });
        }
    }
}