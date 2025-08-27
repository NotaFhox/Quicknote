using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NoteApp.Data;
using NoteApp.Models;
using System.Collections.Concurrent;

namespace NoteApp.Services
{
    public class DatabaseNoteService : INoteService
    {
        private readonly NoteDbContext _context;
        private readonly ILogger<DatabaseNoteService> _logger;
        private readonly ConcurrentDictionary<int, Note> _noteCache = new();
        private readonly SemaphoreSlim _semaphore = new(1, 1);
        private DateTime _lastCacheRefresh = DateTime.MinValue;
        private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(5);

        public DatabaseNoteService(NoteDbContext context, ILogger<DatabaseNoteService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Note>> GetNotesAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                if (DateTime.Now - _lastCacheRefresh > _cacheExpiration)
                {
                    await RefreshCacheAsync();
                }

                return _noteCache.Values
                    .OrderByDescending(n => n.DateModified)
                    .ToList();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<Note?> GetNoteAsync(int id)
        {
            if (_noteCache.TryGetValue(id, out var cachedNote))
            {
                return cachedNote;
            }

            try
            {
                _logger.LogDebug("Cache miss for note ID: {NoteId}", id);
                
                var note = await _context.Notes
                    .AsNoTracking()
                    .FirstOrDefaultAsync(n => n.Id == id);
                
                if (note != null)
                {
                    _noteCache.TryAdd(id, note);
                    _logger.LogInformation("Cached note with ID: {NoteId}", id);
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
                    if (note.Id == 0)
                    {
                        _logger.LogDebug("Creating new note: {NoteTitle}", note.Title);
                        
                        note.DateCreated = DateTime.Now;
                        note.DateModified = DateTime.Now;
                        _context.Notes.Add(note);
                    }
                    else
                    {
                        _logger.LogDebug("Updating note ID: {NoteId}", note.Id);
                        
                        note.DateModified = DateTime.Now;
                        _context.Notes.Update(note);
                    }

                    await _context.SaveChangesAsync();
                    
                    _noteCache.AddOrUpdate(note.Id, note, (key, oldValue) => note);
                    
                    _logger.LogInformation("Saved note ID: {NoteId}", note.Id);
                    return note.Id;
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
                    _logger.LogDebug("Deleting note ID: {NoteId}", note.Id);
                    
                    _context.Notes.Remove(note);
                    var result = await _context.SaveChangesAsync();
                    
                    _noteCache.TryRemove(note.Id, out _);
                    
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
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return await GetNotesAsync();
                }

                _logger.LogDebug("Searching with term: {SearchTerm}", searchTerm);
                
                var normalizedSearchTerm = searchTerm.Trim().ToLowerInvariant();
                
                if (_noteCache.Any())
                {
                    return _noteCache.Values
                        .Where(n => n.MatchesSearchTerm(normalizedSearchTerm))
                        .OrderByDescending(n => n.DateModified)
                        .ToList();
                }
                
                var notes = await _context.Notes
                    .AsNoTracking()
                    .Where(n => EF.Functions.Like(n.Title.ToLower(), $"%{normalizedSearchTerm}%") || 
                               EF.Functions.Like(n.Content.ToLower(), $"%{normalizedSearchTerm}%") ||
                               EF.Functions.Like(n.Tags.ToLower(), $"%{normalizedSearchTerm}%"))
                    .OrderByDescending(n => n.DateModified)
                    .ToListAsync();
                
                _logger.LogInformation("Found {NoteCount} notes for: {SearchTerm}", 
                    notes.Count, searchTerm);
                
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
                if (string.IsNullOrWhiteSpace(category) || category.Equals("All", StringComparison.OrdinalIgnoreCase))
                {
                    return await GetNotesAsync();
                }

                _logger.LogDebug("Getting notes for category: {Category}", category);
                
                if (_noteCache.Any())
                {
                    return _noteCache.Values
                        .Where(n => n.Category == category)
                        .OrderByDescending(n => n.DateModified)
                        .ToList();
                }
                
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
                _logger.LogError(ex, "Error getting category: {Category}", category);
                throw;
            }
        }

        public async Task<List<string>> GetCategoriesAsync()
        {
            try
            {
                _logger.LogDebug("Retrieving categories");
                
                List<string> categories;
                
                if (_noteCache.Any())
                {
                    categories = _noteCache.Values
                        .Select(n => n.Category)
                        .Distinct()
                        .Where(c => !string.IsNullOrEmpty(c))
                        .OrderBy(c => c)
                        .ToList();
                }
                else
                {
                    categories = await _context.Notes
                        .AsNoTracking()
                        .Select(n => n.Category)
                        .Distinct()
                        .Where(c => !string.IsNullOrEmpty(c))
                        .OrderBy(c => c)
                        .ToListAsync();
                }

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
                    var noteList = notes.ToList();
                    _logger.LogDebug("Deleting {NoteCount} notes", noteList.Count);
                    
                    _context.Notes.RemoveRange(noteList);
                    var result = await _context.SaveChangesAsync();
                    
                    foreach (var note in noteList)
                    {
                        _noteCache.TryRemove(note.Id, out _);
                    }
                    
                    _logger.LogInformation("Deleted {NoteCount} notes", noteList.Count);
                    return result;
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
                await _context.Database.CanConnectAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database health check failed");
                return false;
            }
        }

        private async Task RefreshCacheAsync()
        {
            try
            {
                _logger.LogDebug("Refreshing note cache");
                
                var notes = await _context.Notes
                    .AsNoTracking()
                    .ToListAsync();
                
                _noteCache.Clear();
                
                foreach (var note in notes)
                {
                    _noteCache.TryAdd(note.Id, note);
                }
                
                _lastCacheRefresh = DateTime.Now;
                
                _logger.LogInformation("Cache refreshed with {NoteCount} notes", notes.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error refreshing cache");
            }
        }
    }
}