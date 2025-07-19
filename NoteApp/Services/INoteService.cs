using NoteApp.Models;

namespace NoteApp.Services
{
    public interface INoteService
    {
        Task<List<Note>> GetNotesAsync();
        Task<Note?> GetNoteAsync(int id);
        Task<int> SaveNoteAsync(Note note);
        Task<int> DeleteNoteAsync(Note note);
        Task<List<Note>> SearchNotesAsync(string searchTerm);
        Task<List<Note>> GetNotesByCategoryAsync(string category);
        Task<List<string>> GetCategoriesAsync();
    }
}