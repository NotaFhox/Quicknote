using NoteApp.Models;

namespace NoteApp.Services
{
    public interface INoteService
    {
        // Core CRUD operations
        Task<List<Note>> GetNotesAsync();
        Task<Note?> GetNoteAsync(int id);
        Task<int> SaveNoteAsync(Note note);
        Task<int> DeleteNoteAsync(Note note);
        
        // Search and filtering operations
        Task<List<Note>> SearchNotesAsync(string searchTerm);
        Task<List<Note>> GetNotesByCategoryAsync(string category);
        Task<List<string>> GetCategoriesAsync();
        
        // Enhanced operations for .NET 9.0
        Task<int> DeleteMultipleNotesAsync(IEnumerable<Note> notes);
        Task<bool> IsHealthyAsync();
    }
}