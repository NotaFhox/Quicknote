using System.Windows.Input;
using NoteApp.Models;
using NoteApp.Services;
using Microsoft.Extensions.Logging;

namespace NoteApp.ViewModels
{
    [QueryProperty(nameof(NoteId), "NoteId")]
    [QueryProperty(nameof(Note), "Note")]
    public class NoteDetailViewModel : BaseViewModel
    {
        private readonly INoteService _noteService;
        private Note _note = new();
        private Note? _originalNote;
        private Timer? _autoSaveTimer;
        private bool _hasUnsavedChanges;
        private string _lastSavedTitle = string.Empty;
        private string _lastSavedContent = string.Empty;

        public Note Note
        {
            get => _note;
            set
            {
                if (_note != value)
                {
                    if (_note != null)
                        _note.PropertyChanged -= OnNotePropertyChanged;
                    
                    _note = value ?? new Note();
                    _note.PropertyChanged += OnNotePropertyChanged;
                    
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsNewNote));
                    OnPropertyChanged(nameof(CanSave));
                    
                    _lastSavedTitle = _note.Title;
                    _lastSavedContent = _note.Content;
                    _hasUnsavedChanges = false;
                    OnPropertyChanged(nameof(HasUnsavedChanges));
                }
            }
        }

        public int NoteId { get; set; }
        public bool IsNewNote => NoteId == 0;
        public bool CanSave => !string.IsNullOrWhiteSpace(Note.Title) || !string.IsNullOrWhiteSpace(Note.Content);
        public bool HasUnsavedChanges
        {
            get => _hasUnsavedChanges;
            private set
            {
                _hasUnsavedChanges = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand BackCommand { get; }
        public ICommand DeleteCommand { get; }

        public NoteDetailViewModel(INoteService noteService, ILogger<NoteDetailViewModel>? logger = null) : base(logger)
        {
            _noteService = noteService;
            SaveCommand = CreateAsyncCommand(SaveNote, () => CanSave);
            BackCommand = CreateAsyncCommand(HandleBackNavigation);
            DeleteCommand = CreateAsyncCommand(DeleteNote, () => !IsNewNote);
        }

        public async Task LoadNote()
        {
            if (NoteId > 0)
            {
                var note = await _noteService.GetNoteAsync(NoteId);
                if (note != null)
                {
                    _originalNote = new Note(note);
                    Note = note;
                    Title = Note.Title;
                }
            }
            else
            {
                Title = "New Note";
                Note = new Note
                {
                    Category = "General"
                };
            }
            
            StartAutoSave();
        }

        private void OnNotePropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Note.Title) || e.PropertyName == nameof(Note.Content))
            {
                CheckForChanges();
                OnPropertyChanged(nameof(CanSave));
                ((Command)SaveCommand).ChangeCanExecute();
                
                RestartAutoSaveTimer();
            }
        }

        private void CheckForChanges()
        {
            HasUnsavedChanges = Note.Title != _lastSavedTitle || Note.Content != _lastSavedContent;
        }

        private void StartAutoSave()
        {
            _autoSaveTimer?.Dispose();
            _autoSaveTimer = new Timer(async _ => await AutoSave(), null, TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(30));
        }

        private void RestartAutoSaveTimer()
        {
            _autoSaveTimer?.Change(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(30));
        }

        private async Task AutoSave()
        {
            if (HasUnsavedChanges && CanSave)
            {
                try
                {
                    await _noteService.SaveNoteAsync(Note);
                    _lastSavedTitle = Note.Title;
                    _lastSavedContent = Note.Content;
                    
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        HasUnsavedChanges = false;
                        Logger?.LogDebug("Auto-saved note: {NoteTitle}", Note.Title);
                    });
                }
                catch (Exception ex)
                {
                    Logger?.LogError(ex, "Auto-save failed for note: {NoteTitle}", Note.Title);
                }
            }
        }

        private async Task SaveNote()
        {
            if (!CanSave) return;

            if (IsNewNote && string.IsNullOrWhiteSpace(Note.Title))
            {
                Note.Title = GenerateDefaultTitle();
            }

            var noteId = await _noteService.SaveNoteAsync(Note);
            NoteId = noteId;
            Note.Id = noteId;
            
            _lastSavedTitle = Note.Title;
            _lastSavedContent = Note.Content;
            HasUnsavedChanges = false;
            
            OnPropertyChanged(nameof(IsNewNote));
            ((Command)DeleteCommand).ChangeCanExecute();
            
            await Shell.Current.DisplayAlert("Saved", "Your note has been saved successfully.", "OK");
        }

        private async Task DeleteNote()
        {
            if (IsNewNote) return;

            bool confirm = await Shell.Current.DisplayAlert("Delete Note", 
                $"Are you sure you want to delete '{Note.Title}'?", "Yes", "No");
            
            if (confirm)
            {
                await _noteService.DeleteNoteAsync(Note);
                await Shell.Current.GoToAsync("..");
            }
        }

        private async Task HandleBackNavigation()
        {
            if (HasUnsavedChanges)
            {
                var result = await Shell.Current.DisplayAlert("Unsaved Changes", 
                    "You have unsaved changes. Do you want to save before leaving?", 
                    "Save", "Don't Save");
                
                if (result == true)
                {
                    if (CanSave)
                    {
                        await SaveNote();
                        await Shell.Current.GoToAsync("..");
                    }
                }
                else if (result == false)
                {
                    await Shell.Current.GoToAsync("..");
                }
            }
            else
            {
                await Shell.Current.GoToAsync("..");
            }
        }

        private string GenerateDefaultTitle()
        {
            if (!string.IsNullOrWhiteSpace(Note.Content))
            {
                var firstLine = Note.Content.Split('\n')[0].Trim();
                if (!string.IsNullOrWhiteSpace(firstLine))
                {
                    return firstLine.Length > 50 ? firstLine[..50] + "..." : firstLine;
                }
            }
            return $"Note {DateTime.Now:dd/MM/yyyy HH:mm}";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _autoSaveTimer?.Dispose();
                if (_note != null)
                {
                    _note.PropertyChanged -= OnNotePropertyChanged;
                }
            }
            base.Dispose(disposing);
        }
    }
}