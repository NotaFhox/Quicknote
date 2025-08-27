using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NoteApp.Data;
using NoteApp.Models;

namespace NoteApp.Services
{
    public class DatabaseNoteService : INoteService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DatabaseNoteService> _logger;
        private readonly SemaphoreSlim _semaphore = new(1, 1);

        public DatabaseNoteService(IServiceProvider serviceProvider, ILogger<DatabaseNoteService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        // Create a new context for each operation to avoid tracking conflicts
        private NoteDbContext CreateContext()
        {
            var scope = _serviceProvider.CreateScope();
            return scope.ServiceProvider.GetRequiredService<NoteDbContext>();
        }

        public async Task<List<Note>> GetNotesAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                using var context = CreateContext();
                
                var notes = await context.Notes
                    .AsNoTracking()
                    .OrderByDescending(n => n.DateModified)
                    .ToListAsync();

                _logger.LogInformation("Retrieved {NoteCount} notes", notes.Count);
                return notes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving notes");
                throw;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<Note?> GetNoteAsync(int id)
        {
            try
            {
                using var context = CreateContext();
                
                var note = await context.Notes
                    .AsNoTracking()
                    .FirstOrDefaultAsync(n => n.Id == id);
                
                if (note != null)
                {
                    _logger.LogInformation("Retrieved note with ID: {NoteId}", id);
                }
                else
                {
                    _logger.LogWarning("Note not found with ID: {NoteId}", id);
                }
                
                return note;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving note with ID: {NoteId}", id);
                throw;
            }
        }

        public async Task<int> SaveNoteAsync(Note note)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(note);
                
                await _semaphore.WaitAsync();
                try
                {
                    using var context = CreateContext();
                    
                    if (note.Id == 0)
                    {
                        _logger.LogDebug("Creating new note: {NoteTitle}", note.Title);
                        
                        // Create a new note entity to avoid tracking issues
                        var newNote = new Note
                        {
                            Title = note.Title,
                            Content = note.Content,
                            Category = note.Category,
                            Tags = note.Tags,
                            DateCreated = DateTime.Now,
                            DateModified = DateTime.Now
                        };
                        
                        context.Notes.Add(newNote);
                        await context.SaveChangesAsync();
                        
                        // Update the original note with the new ID
                        note.Id = newNote.Id;
                        note.DateCreated = newNote.DateCreated;
                        note.DateModified = newNote.DateModified;
                        
                        _logger.LogInformation("Created new note with ID: {NoteId}", newNote.Id);
                        return newNote.Id;
                    }
                    else
                    {
                        _logger.LogDebug("Updating note ID: {NoteId}", note.Id);
                        
                        // Find the existing note and update its properties
                        var existingNote = await context.Notes.FindAsync(note.Id);
                        if (existingNote == null)
                        {
                            throw new InvalidOperationException($"Note with ID {note.Id} not found");
                        }
                        
                        existingNote.Title = note.Title;
                        existingNote.Content = note.Content;
                        existingNote.Category = note.Category;
                        existingNote.Tags = note.Tags;
                        existingNote.DateModified = DateTime.Now;
                        
                        await context.SaveChangesAsync();
                        
                        // Update the original note's modified date
                        note.DateModified = existingNote.DateModified;
                        
                        _logger.LogInformation("Updated note ID: {NoteId}", note.Id);
                        return note.Id;
                    }
                }
                finally
                {
                    _semaphore.Release();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving note: {NoteTitle}", note?.Title ?? "Unknown");
                throw;
            }
        }

        public async Task<int> DeleteNoteAsync(Note note)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(note);
                
                await _semaphore.WaitAsync();
                try
                {
                    using var context = CreateContext();
                    
                    _logger.LogDebug("Deleting note ID: {NoteId}", note.Id);
                    
                    // Find the note by ID to ensure we're deleting the right one
                    var noteToDelete = await context.Notes.FindAsync(note.Id);
                    if (noteToDelete == null)
                    {
                        _logger.LogWarning("Note with ID {NoteId} not found for deletion", note.Id);
                        return 0;
                    }
                    
                    context.Notes.Remove(noteToDelete);
                    var result = await context.SaveChangesAsync();
                    
                    _logger.LogInformation("Deleted note ID: {NoteId}", note.Id);
                    return result;
                }
                finally
                {
                    _semaphore.Release();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting note ID: {NoteId}", note?.Id ?? 0);
                throw;
            }
        }

        public async Task<List<Note>> SearchNotesAsync(string searchTerm)
        {
            try
            {
                using var context = CreateContext();
                
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return await GetNotesAsync();
                }

                _logger.LogDebug("Searching with term: {SearchTerm}", searchTerm);
                
                var normalizedSearchTerm = searchTerm.Trim().ToLowerInvariant();
                
                var notes = await context.Notes
                    .AsNoTracking()
                    .Where(n => EF.Functions.Like(n.Title.ToLower(), $"%{normalizedSearchTerm}%") || 
                               EF.Functions.Like(n.Content.ToLower(), $"%{normalizedSearchTerm}%") ||
                               EF.Functions.Like(n.Tags.ToLower(), $"%{normalizedSearchTerm}%") ||
                               EF.Functions.Like(n.Category.ToLower(), $"%{normalizedSearchTerm}%"))
                    .OrderByDescending(n => n.DateModified)
                    .ToListAsync();
                
                _logger.LogInformation("Found {NoteCount} notes for: {SearchTerm}", notes.Count, searchTerm);
                return notes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching with term: {SearchTerm}", searchTerm);
                throw;
            }
        }

        public async Task<List<Note>> GetNotesByCategoryAsync(string category)
        {
            try
            {
                using var context = CreateContext();
                
                if (string.IsNullOrWhiteSpace(category) || category.Equals("All", StringComparison.OrdinalIgnoreCase))
                {
                    return await GetNotesAsync();
                }

                _logger.LogDebug("Getting notes for category: {Category}", category);
                
                var notes = await context.Notes
                    .AsNoTracking()
                    .Where(n => n.Category == category)
                    .OrderByDescending(n => n.DateModified)
                    .ToListAsync();
                
                _logger.LogInformation("Found {NoteCount} notes in category: {Category}", notes.Count, category);
                return notes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting category: {Category}", category);
                throw;
            }
        }

        public async Task<List<string>> GetCategoriesAsync()
        {
            try
            {
                using var context = CreateContext();
                
                _logger.LogDebug("Retrieving categories");
                
                var categories = await context.Notes
                    .AsNoTracking()
                    .Select(n => n.Category)
                    .Distinct()
                    .Where(c => !string.IsNullOrEmpty(c))
                    .OrderBy(c => c)
                    .ToListAsync();

                var result = new List<string> { "All" };
                result.AddRange(categories);
                
                _logger.LogInformation("Retrieved {CategoryCount} categories", categories.Count);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving categories");
                throw;
            }
        }

        public async Task<int> DeleteMultipleNotesAsync(IEnumerable<Note> notes)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(notes);
                
                await _semaphore.WaitAsync();
                try
                {
                    using var context = CreateContext();
                    
                    var noteList = notes.ToList();
                    _logger.LogDebug("Deleting {NoteCount} notes", noteList.Count);
                    
                    var noteIds = noteList.Select(n => n.Id).ToList();
                    var notesToDelete = await context.Notes
                        .Where(n => noteIds.Contains(n.Id))
                        .ToListAsync();
                    
                    if (notesToDelete.Any())
                    {
                        context.Notes.RemoveRange(notesToDelete);
                        var result = await context.SaveChangesAsync();
                        
                        _logger.LogInformation("Deleted {NoteCount} notes", notesToDelete.Count);
                        return result;
                    }
                    
                    return 0;
                }
                finally
                {
                    _semaphore.Release();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting multiple notes");
                throw;
            }
        }

        public async Task<bool> IsHealthyAsync()
        {
            try
            {
                using var context = CreateContext();
                await context.Database.CanConnectAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database health check failed");
                return false;
            }
        }
    }
}