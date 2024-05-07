using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.Entity;

namespace SocialNetwork.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<MessageEntity> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasMany(a => a.ReceivedMessages)
                .WithOne(a => a.FromUser)
                .HasForeignKey(c => c.FromUserId);

            modelBuilder.Entity<UserEntity>()
                .HasMany(a => a.SentMessages)
                .WithOne(a => a.ToUser)
                .HasForeignKey(c => c.ToUserId);

            modelBuilder.Entity<MessageEntity>()
                .HasOne(m => m.FromUser)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.FromUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MessageEntity>()
                .HasOne(m => m.ToUser)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.ToUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
