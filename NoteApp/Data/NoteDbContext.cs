using Microsoft.EntityFrameworkCore;
using NoteApp.Models;

namespace NoteApp.Data
{
    public class NoteDbContext : DbContext
    {
        // |---------------------|
        // |                     |
        // |      DbSets         |
        // |                     |
        // |---------------------|
        public DbSet<Note> Notes { get; set; }

        // |---------------------|
        // |                     |
        // |    Constructor      |
        // |                     |
        // |---------------------|
        public NoteDbContext(DbContextOptions<NoteDbContext> options)
            : base(options)
        {
        }

        // |---------------------|
        // |                     |
        // |  Model Configuration|
        // |                     |
        // |---------------------|
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Note>(entity =>
            {
                // |---------------------|
                // |                     |
                // |   Primary Key       |
                // |                     |
                // |---------------------|
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                // |---------------------|
                // |                     |
                // | Property Constraints|
                // |                     |
                // |---------------------|
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

                // |---------------------|
                // |                     |
                // | Date Configuration  |
                // |                     |
                // |---------------------|
                entity.Property(e => e.DateCreated)
                    .IsRequired()
                    .HasDefaultValueSql("datetime('now')")
                    .HasComment("When the note was created");

                entity.Property(e => e.DateModified)
                    .IsRequired()
                    .HasDefaultValueSql("datetime('now')")
                    .HasComment("When the note was last modified");
                
                // |---------------------|
                // |                     |
                // |Performance Indexes  |
                // |                     |
                // |---------------------|
                entity.HasIndex(e => e.Title)
                    .HasDatabaseName("IX_Notes_Title");
                
                entity.HasIndex(e => e.DateModified)
                    .HasDatabaseName("IX_Notes_DateModified")
                    .IsDescending();
                
                entity.HasIndex(e => e.Category)
                    .HasDatabaseName("IX_Notes_Category");
                
                // |---------------------|
                // |                     |
                // |  Composite Indexes  |
                // |                     |
                // |---------------------|
                entity.HasIndex(e => new { e.Category, e.DateModified })
                    .HasDatabaseName("IX_Notes_Category_DateModified")
                    .IsDescending(false, true);

                entity.HasIndex(e => new { e.Title, e.Content })
                    .HasDatabaseName("IX_Notes_Search");
            });

            // |---------------------|
            // |                     |
            // |     Seed Data       |
            // |                     |
            // |---------------------|
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

        // |---------------------|
        // |                     |
        // | Database Configuration|
        // |                     |
        // |---------------------|
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            
            if (!optionsBuilder.IsConfigured)
            {
                var dbPath = Path.Combine(FileSystem.AppDataDirectory, "notes.db");
                optionsBuilder.UseSqlite($"Data Source={dbPath}");
            }

            // |---------------------|
            // |                     |
            // |Performance Settings |
            // |                     |
            // |---------------------|
            optionsBuilder.EnableSensitiveDataLogging(false);
            optionsBuilder.EnableDetailedErrors(false);
            
#if DEBUG
            optionsBuilder.EnableSensitiveDataLogging(true);
            optionsBuilder.EnableDetailedErrors(true);
            optionsBuilder.LogTo(message => System.Diagnostics.Debug.WriteLine(message));
#endif
        }

        // |---------------------|
        // |                     |
        // | Save Changes Override|
        // |                     |
        // |---------------------|
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

        // |---------------------|
        // |                     |
        // | Timestamp Management|
        // |                     |
        // |---------------------|
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