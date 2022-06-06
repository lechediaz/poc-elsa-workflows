using Microsoft.EntityFrameworkCore;
using FactoryApp.Models;

namespace FactoryApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<RawMaterial> RawMaterials { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestDetail> RequestDetails { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(s => s.Supervisor)
                .WithMany(s => s.Subordinates)
                .HasForeignKey(k => k.SupervisorId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            modelBuilder.Entity<Request>()
                .HasOne(s => s.Author)
                .WithMany(s => s.RequestsCreated)
                .HasForeignKey(k => k.AuthorId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            modelBuilder.Entity<Request>()
                .HasOne(s => s.Approver)
                .WithMany(s => s.RequestsAsApprover)
                .HasForeignKey(k => k.ApproverId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
            
            modelBuilder.Entity<RawMaterial>()
                .Property(p => p.Stock)
                .HasDefaultValue(0);
        }
    }
}
