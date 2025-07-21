using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NoteApp.Data;
using NoteApp.Models;

namespace NoteApp.Services
{
    public class DatabaseNoteService : INoteService
    {
        private readonly NoteDbContext _context;
        private readonly ILogger<DatabaseNoteService> _logger;

        public DatabaseNoteService(NoteDbContext context, ILogger<DatabaseNoteService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Note>> GetNotesAsync()
        {
            try
            {
                _logger.LogDebug("Retrieving all notes from database");
                
                var notes = await _context.Notes
                    .AsNoTracking() 
                    .OrderByDescending(n => n.DateModified)
                    .ToListAsync();
                
                _logger.LogInformation("Retrieved {NoteCount} notes from database", notes.Count);
                return notes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving notes");
                throw;
            }
        }

        public async Task<Note?> GetNoteAsync(int id)
        {
            try
            {
                _logger.LogDebug("Retrieving note with ID: {NoteId}", id);
                
                var note = await _context.Notes
                    .AsNoTracking() 
                    .FirstOrDefaultAsync(n => n.Id == id);
                
                if (note != null)
                {
                    _logger.LogInformation("Found note with ID: {NoteId}", id);
                }
                else
                {
                    _logger.LogWarning("Note with ID: {NoteId} not found", id);
                }
                
                return note;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving note with ID: {NoteId}", id);
                throw;
            }
        }

        public async Task<int> SaveNoteAsync(Note note)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(note);
                
                if (note.Id == 0)
                {
                    _logger.LogDebug("Creating new note: {NoteTitle}", note.Title);
                    
                    note.DateCreated = DateTime.Now;
                    note.DateModified = DateTime.Now;
                    _context.Notes.Add(note);
                }
                else
                {
                    _logger.LogDebug("Updating existing note with ID: {NoteId}", note.Id);
                    
                    
                    note.DateModified = DateTime.Now;
                    _context.Notes.Update(note);
                }

                var result = await _context.SaveChangesAsync();
                
                _logger.LogInformation("Successfully saved note with ID: {NoteId}", note.Id);
                return note.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving note: {NoteTitle}", note?.Title ?? "Unknown");
                throw;
            }
        }

        public async Task<int> DeleteNoteAsync(Note note)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(note);
                
                _logger.LogDebug("Deleting note with ID: {NoteId}", note.Id);
                
                _context.Notes.Remove(note);
                var result = await _context.SaveChangesAsync();
                
                _logger.LogInformation("Successfully deleted note with ID: {NoteId}", note.Id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting note with ID: {NoteId}", note?.Id ?? 0);
                throw;
            }
        }

       
        public async Task<List<Note>> SearchNotesAsync(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    _logger.LogDebug("Empty search term provided, returning all notes");
                    return await GetNotesAsync();
                }

                _logger.LogDebug("Searching notes with term: {SearchTerm}", searchTerm);
                
                
                var normalizedSearchTerm = searchTerm.Trim().ToLowerInvariant();
                
                var notes = await _context.Notes
                    .AsNoTracking()
                    .Where(n => EF.Functions.Like(n.Title.ToLower(), $"%{normalizedSearchTerm}%") || 
                               EF.Functions.Like(n.Content.ToLower(), $"%{normalizedSearchTerm}%") ||
                               EF.Functions.Like(n.Tags.ToLower(), $"%{normalizedSearchTerm}%"))
                    .OrderByDescending(n => n.DateModified)
                    .ToListAsync();
                
                _logger.LogInformation("Found {NoteCount} notes matching search term: {SearchTerm}", 
                    notes.Count, searchTerm);
                
                return notes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching notes with term: {SearchTerm}", searchTerm);
                throw;
            }
        }

        public async Task<List<Note>> GetNotesByCategoryAsync(string category)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(category) || category.Equals("All", StringComparison.OrdinalIgnoreCase))
                {
                    _logger.LogDebug("Getting all notes (category filter: All or empty)");
                    return await GetNotesAsync();
                }

                _logger.LogDebug("Getting notes for category: {Category}", category);
                
                var notes = await _context.Notes
                    .AsNoTracking()
                    .Where(n => n.Category == category)
                    .OrderByDescending(n => n.DateModified)
                    .ToListAsync();
                
                _logger.LogInformation("Found {NoteCount} notes in category: {Category}", 
                    notes.Count, category);
                
                return notes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving notes for category: {Category}", category);
                throw;
            }
        }

        public async Task<List<string>> GetCategoriesAsync()
        {
            try
            {
                _logger.LogDebug("Retrieving all categories");
                
                var categories = await _context.Notes
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
                _logger.LogError(ex, "Error occurred while retrieving categories");
                throw;
            }
        }

        
        public async Task<int> DeleteMultipleNotesAsync(IEnumerable<Note> notes)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(notes);
                
                var noteList = notes.ToList();
                _logger.LogDebug("Deleting {NoteCount} notes", noteList.Count);
                
                _context.Notes.RemoveRange(noteList);
                var result = await _context.SaveChangesAsync();
                
                _logger.LogInformation("Successfully deleted {NoteCount} notes", noteList.Count);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting multiple notes");
                throw;
            }
        }

        
        public async Task<bool> IsHealthyAsync()
        {
            try
            {
                await _context.Database.CanConnectAsync();
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