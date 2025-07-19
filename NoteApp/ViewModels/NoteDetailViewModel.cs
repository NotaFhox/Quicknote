using System.Windows.Input;
using NoteApp.Models;
using NoteApp.Services;

namespace NoteApp.ViewModels
{
    [QueryProperty(nameof(NoteId), "NoteId")]
    [QueryProperty(nameof(Note), "Note")]
    public class NoteDetailViewModel : BaseViewModel
    {
        private readonly INoteService _noteService;
        private Note _note = new();

        public Note Note
        {
            get => _note;
            set
            {
                _note = value;
                OnPropertyChanged();
            }
        }

        public int NoteId { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand BackCommand { get; }

        public NoteDetailViewModel(INoteService noteService)
        {
            _noteService = noteService;
            SaveCommand = new Command(async () => await SaveNote());
            BackCommand = new Command(async () => await GoBack());
        }

        public async Task LoadNote()
        {
            if (NoteId > 0)
            {
                var note = await _noteService.GetNoteAsync(NoteId);
                if (note != null)
                {
                    Note = note;
                    Title = Note.Title;
                }
            }
            else
            {
                Title = "New Note";
            }
        }

        private async Task SaveNote()
        {
            if (string.IsNullOrWhiteSpace(Note.Title) && string.IsNullOrWhiteSpace(Note.Content))
                return;

            await _noteService.SaveNoteAsync(Note);
            await GoBack();
        }

        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}