using Microsoft.EntityFrameworkCore;
using NoteApp.Models;

namespace NoteApp.Data
{
    public class NoteDbContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }

        public NoteDbContext(DbContextOptions<NoteDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Note>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Content).HasMaxLength(5000);
                entity.Property(e => e.Category).HasMaxLength(50);
                entity.Property(e => e.Tags).HasMaxLength(500);
                entity.Property(e => e.DateCreated).IsRequired();
                entity.Property(e => e.DateModified).IsRequired();
                
                // Add indexes for better search performance
                entity.HasIndex(e => e.Title);
                entity.HasIndex(e => e.DateModified);
                entity.HasIndex(e => e.Category);
            });
        }
    }
}