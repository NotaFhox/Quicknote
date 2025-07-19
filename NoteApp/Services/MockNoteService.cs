using NoteApp.Models;

namespace NoteApp.Services
{
    public class MockNoteService : INoteService
    {
        private static List<Note> _notes = new List<Note>
        {
            new Note { Id = 1, Title = "Welcome Note", Content = "This is your first note! Tap to edit." },
            new Note { Id = 2, Title = "Shopping List", Content = "- Milk\n- Bread\n- Eggs\n- Butter" },
            new Note { Id = 3, Title = "Meeting Notes", Content = "Discussed project timeline and deliverables." }
        };
        
        private int _nextId = 4;

        public async Task<List<Note>> GetNotesAsync()
        {
            await Task.Delay(100); // Simulate async operation
            return _notes.OrderByDescending(n => n.DateModified).ToList();
        }

        public async Task<Note?> GetNoteAsync(int id)
        {
            await Task.Delay(50);
            return _notes.FirstOrDefault(n => n.Id == id);
        }

        public async Task<int> SaveNoteAsync(Note note)
        {
            await Task.Delay(100);
            
            if (note.Id == 0)
            {
                note.Id = _nextId++;
                note.DateCreated = DateTime.Now;
                _notes.Add(note);
            }
            else
            {
                var existingNote = _notes.FirstOrDefault(n => n.Id == note.Id);
                if (existingNote != null)
                {
                    existingNote.Title = note.Title;
                    existingNote.Content = note.Content;
                    existingNote.DateModified = DateTime.Now;
                }
            }
            
            return note.Id;
        }

        public async Task<int> DeleteNoteAsync(Note note)
        {
            await Task.Delay(50);
            var existingNote = _notes.FirstOrDefault(n => n.Id == note.Id);
            if (existingNote != null)
            {
                _notes.Remove(existingNote);
                return 1;
            }
            return 0;
        }
    }
}