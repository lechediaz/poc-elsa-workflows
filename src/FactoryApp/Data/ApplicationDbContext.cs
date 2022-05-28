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
                .HasOne(s => s.CreatedBy)
                .WithMany(s => s.RequestsCreated)
                .HasForeignKey(k => k.CreatedById)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            modelBuilder.Entity<Request>()
                .HasOne(s => s.Receiver)
                .WithMany(s => s.RequestsAsReceiver)
                .HasForeignKey(k => k.ReceiverId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
