using Microsoft.EntityFrameworkCore;
using RazzieAwardsAnalyzer.Domain.Entities;

namespace RazzieAwardsAnalyzer.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) { }

        public DbSet<Producer> Producers { get; set; }
        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producer>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name)
                   .HasMaxLength(150)
                   .IsRequired();
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Year)
                      .IsRequired();

                entity.Property(m => m.Title)
                      .HasMaxLength(200)
                      .IsRequired();

                entity.Property(m => m.Winner)
                      .IsRequired();

                entity.HasMany(m => m.Producers)
                      .WithMany(p => p.Movies);
            });
        }
    }
}