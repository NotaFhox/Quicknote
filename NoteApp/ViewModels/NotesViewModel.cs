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
        private string _searchText = string.Empty;
        private string _selectedCategory = "All";
        private List<string> _categories = new();

        public ObservableCollection<Note> Notes { get; set; } = new();

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                SearchCommand.Execute(null);
            }
        }

        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
                FilterByCategoryCommand.Execute(null);
            }
        }

        public List<string> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadNotesCommand { get; }
        public ICommand AddNoteCommand { get; }
        public ICommand SelectNoteCommand { get; }
        public ICommand DeleteNoteCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand ClearSearchCommand { get; }
        public ICommand FilterByCategoryCommand { get; }

        public NotesViewModel(INoteService noteService)
        {
            _noteService = noteService;
            Title = "My Notes";

            LoadNotesCommand = new Command(async () => await LoadNotes());
            AddNoteCommand = new Command(async () => await AddNote());
            SelectNoteCommand = new Command<Note>(async (note) => await SelectNote(note));
            DeleteNoteCommand = new Command<Note>(async (note) => await DeleteNote(note));
            SearchCommand = new Command(async () => await SearchNotes());
            ClearSearchCommand = new Command(() => 
            {
                SearchText = string.Empty;
                LoadNotesCommand.Execute(null);
            });
            FilterByCategoryCommand = new Command(async () => await FilterByCategory());
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
                
                // Load categories
                Categories = await _noteService.GetCategoriesAsync();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task SearchNotes()
        {
            if (IsBusy) return;
            
            IsBusy = true;
            try
            {
                Notes.Clear();
                var notes = await _noteService.SearchNotesAsync(SearchText);
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
                Content = ""
            };
            
            await Shell.Current.GoToAsync($"{nameof(NoteDetailPage)}?NoteId=0", new Dictionary<string, object>
            {
                ["Note"] = newNote
            });
        }

        private async Task SelectNote(Note note)
        {
            if (note == null) return;
            
            await Shell.Current.GoToAsync($"{nameof(NoteDetailPage)}?NoteId={note.Id}");
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

        private async Task FilterByCategory()
        {
            if (IsBusy) return;
            
            IsBusy = true;
            try
            {
                Notes.Clear();
                var notes = await _noteService.GetNotesByCategoryAsync(SelectedCategory);
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
    }
}