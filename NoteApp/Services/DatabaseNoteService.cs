using Microsoft.EntityFrameworkCore;
using NoteApp.Data;
using NoteApp.Models;

namespace NoteApp.Services
{
    public class DatabaseNoteService : INoteService
    {
        private readonly NoteDbContext _context;

        public DatabaseNoteService(NoteDbContext context)
        {
            _context = context;
        }

        public async Task<List<Note>> GetNotesAsync()
        {
            return await _context.Notes
                .OrderByDescending(n => n.DateModified)
                .ToListAsync();
        }

        public async Task<Note?> GetNoteAsync(int id)
        {
            return await _context.Notes
                .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<int> SaveNoteAsync(Note note)
        {
            if (note.Id == 0)
            {
                note.DateCreated = DateTime.Now;
                note.DateModified = DateTime.Now;
                _context.Notes.Add(note);
            }
            else
            {
                note.DateModified = DateTime.Now;
                _context.Notes.Update(note);
            }

            await _context.SaveChangesAsync();
            return note.Id;
        }

        public async Task<int> DeleteNoteAsync(Note note)
        {
            _context.Notes.Remove(note);
            return await _context.SaveChangesAsync();
        }

        // Additional methods for search functionality
        public async Task<List<Note>> SearchNotesAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetNotesAsync();

            searchTerm = searchTerm.ToLower();
            
            return await _context.Notes
                .Where(n => n.Title.ToLower().Contains(searchTerm) || 
                           n.Content.ToLower().Contains(searchTerm))
                .OrderByDescending(n => n.DateModified)
                .ToListAsync();
        }

        public async Task<List<Note>> GetNotesByCategoryAsync(string category)
        {
            if (string.IsNullOrWhiteSpace(category) || category == "All")
                return await GetNotesAsync();

            return await _context.Notes
                .Where(n => n.Category == category)
                .OrderByDescending(n => n.DateModified)
                .ToListAsync();
        }

        public async Task<List<string>> GetCategoriesAsync()
        {
            var categories = await _context.Notes
                .Select(n => n.Category)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();

            categories.Insert(0, "All");
            return categories;
        }
    }
}