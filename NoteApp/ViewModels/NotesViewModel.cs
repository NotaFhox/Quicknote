using System.Collections.ObjectModel;
using System.Windows.Input;
using NoteApp.Models;
using NoteApp.Services;
using NoteApp.Views;

namespace NoteApp.ViewModels
{
    public class NotesViewModel : BaseViewModel
    {
        private readonly INoteService _noteService;

        public ObservableCollection<Note> Notes { get; set; } = new();

        public ICommand LoadNotesCommand { get; }
        public ICommand AddNoteCommand { get; }
        public ICommand SelectNoteCommand { get; }
        public ICommand DeleteNoteCommand { get; }

        public NotesViewModel(INoteService noteService)
        {
            _noteService = noteService;
            Title = "My Notes";

            LoadNotesCommand = new Command(async () => await LoadNotes());
            AddNoteCommand = new Command(async () => await AddNote());
            SelectNoteCommand = new Command<Note>(async (note) => await SelectNote(note));
            DeleteNoteCommand = new Command<Note>(async (note) => await DeleteNote(note));
        }

        private async Task LoadNotes()
        {
            if (IsBusy) return;
            
            IsBusy = true;
            try
            {
                Notes.Clear();
                var notes = await _noteService.GetNotesAsync();
                foreach (var note in notes)
                {
                    Notes.Add(note);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task AddNote()
        {
            var newNote = new Note
            {
                Title = "New Note",
                Content = "Start typing..."
            };
            
            await Shell.Current.GoToAsync($"{nameof(NoteDetailPage)}?NoteId=0", new Dictionary<string, object>
            {
                ["Note"] = newNote
            });
        }

        private async Task SelectNote(Note note)
        {
            if (note == null) return;
            
            await Shell.Current.GoToAsync($"{nameof(NoteDetailPage)}?NoteId={note.Id}", new Dictionary<string, object>
            {
                ["Note"] = note
            });
        }

        private async Task DeleteNote(Note note)
        {
            if (note == null) return;

            bool confirm = await Shell.Current.DisplayAlert("Delete Note", 
                $"Are you sure you want to delete '{note.Title}'?", "Yes", "No");
            
            if (confirm)
            {
                await _noteService.DeleteNoteAsync(note);
                Notes.Remove(note);
            }
        }

        public async Task OnAppearing()
        {
            await LoadNotes();
        }
    }
}
