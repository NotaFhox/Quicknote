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

            // Configure Note entity with enhanced .NET 9.0 features
            modelBuilder.Entity<Note>(entity =>
            {
                // Primary key configuration
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                // Required properties with constraints
                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasComment("The title of the note");

                entity.Property(e => e.Content)
                    .HasMaxLength(5000)
                    .HasComment("The main content of the note");

                entity.Property(e => e.Category)
                    .HasMaxLength(50)
                    .HasDefaultValue("General")
                    .HasComment("Category classification for the note");

                entity.Property(e => e.Tags)
                    .HasMaxLength(500)
                    .HasComment("Comma-separated tags for the note");

                // Date properties with proper configuration
                entity.Property(e => e.DateCreated)
                    .IsRequired()
                    .HasDefaultValueSql("datetime('now')")
                    .HasComment("When the note was created");

                entity.Property(e => e.DateModified)
                    .IsRequired()
                    .HasDefaultValueSql("datetime('now')")
                    .HasComment("When the note was last modified");
                
                // Performance indexes for .NET 9.0 optimization
                entity.HasIndex(e => e.Title)
                    .HasDatabaseName("IX_Notes_Title");
                
                entity.HasIndex(e => e.DateModified)
                    .HasDatabaseName("IX_Notes_DateModified")
                    .IsDescending(); // Optimize for recent notes first
                
                entity.HasIndex(e => e.Category)
                    .HasDatabaseName("IX_Notes_Category");
                
                // Composite index for search optimization
                entity.HasIndex(e => new { e.Category, e.DateModified })
                    .HasDatabaseName("IX_Notes_Category_DateModified")
                    .IsDescending(false, true);

                // Full-text search index for content (SQLite FTS5 when available)
                entity.HasIndex(e => new { e.Title, e.Content })
                    .HasDatabaseName("IX_Notes_Search");
            });

            // Seed data for initial setup (optional)
            modelBuilder.Entity<Note>().HasData(
                new Note
                {
                    Id = 1,
                    Title = "Welcome to NoteApp",
                    Content = "Welcome to your new note-taking app! This app features a classic 90s interface with modern functionality. Tap this note to edit it or create new notes using the Add Note button.",
                    Category = "General",
                    Tags = "welcome, getting-started",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            
            // Enhanced SQLite configuration for .NET 9.0
            if (!optionsBuilder.IsConfigured)
            {
                var dbPath = Path.Combine(FileSystem.AppDataDirectory, "notes.db");
                optionsBuilder.UseSqlite($"Data Source={dbPath}");
            }

            // Performance optimizations for .NET 9.0
            optionsBuilder.EnableSensitiveDataLogging(false); // Security best practice
            optionsBuilder.EnableDetailedErrors(false); // Production security
            
#if DEBUG
            optionsBuilder.EnableSensitiveDataLogging(true);
            optionsBuilder.EnableDetailedErrors(true);
            optionsBuilder.LogTo(message => System.Diagnostics.Debug.WriteLine(message));
#endif
        }

        // Override SaveChanges to automatically update DateModified
        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker.Entries<Note>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.Now;
                }
                entry.Entity.DateModified = DateTime.Now;
            }
        }
    }
}