
using Microsoft.EntityFrameworkCore;
using miniHelpDesk.Core.Entities;

namespace miniHelpDesk.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketHistory> TicketHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (!typeof(EntityBase).IsAssignableFrom(entityType.ClrType))
                    continue;

                var properties = entityType.GetProperties()
                    .Where(p => p.Name != nameof(EntityBase.Id)
                             && p.Name != nameof(EntityBase.CreatedAt)
                             && p.Name != nameof(EntityBase.UpdatedAt))
                    .ToList();

                var entity = modelBuilder.Entity(entityType.ClrType);

                entity.Property(nameof(EntityBase.Id)).HasColumnOrder(0);

                for (var i = 0; i < properties.Count; i++)
                {
                    entity.Property(properties[i].Name)
                        .HasColumnOrder(i + 1);
                }

                entity.Property(nameof(EntityBase.CreatedAt))
                    .HasColumnOrder(properties.Count + 1);

                entity.Property(nameof(EntityBase.UpdatedAt))
                    .HasColumnOrder(properties.Count + 2);
            }

            modelBuilder.Entity<TicketHistory>()
                .HasOne(h => h.ChangedBy)
                .WithMany()
                .HasForeignKey(h => h.ChangedById)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TicketHistory>()
                .HasOne(h => h.Ticket)
                .WithMany(t => t.History)
                .HasForeignKey(h => h.TicketId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
