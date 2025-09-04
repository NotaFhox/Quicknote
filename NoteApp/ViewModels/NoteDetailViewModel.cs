using NoteApp.Models;
using NoteApp.Services;
using Microsoft.Extensions.Logging;
using System.Windows.Input;

namespace NoteApp.ViewModels
{
    [QueryProperty(nameof(NoteId), "NoteId")]
    [QueryProperty(nameof(Note), "Note")]
    public class NoteDetailViewModel : BaseViewModel
    {
        // |---------------------|
        // |                     |
        // |   Service Fields    |
        // |                     |
        // |---------------------|
        private readonly INoteService _noteService;
        private readonly ISettingsService _settingsService;
        
        // |---------------------|
        // |                     |
        // |   Private Fields    |
        // |                     |
        // |---------------------|
        private Note _note = new();
        private Note? _originalNote;
        private Timer? _autoSaveTimer;
        private bool _hasUnsavedChanges;
        private string _lastSavedTitle = string.Empty;
        private string _lastSavedContent = string.Empty;
        private bool _isNavigating = false;

        // |---------------------|
        // |                     |
        // |    Properties       |
        // |                     |
        // |---------------------|
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
                    OnPropertyChanged(nameof(CanDelete));
                    
                    _lastSavedTitle = _note.Title;
                    _lastSavedContent = _note.Content;
                    _hasUnsavedChanges = false;
                    OnPropertyChanged(nameof(HasUnsavedChanges));
                }
            }
        }

        public int NoteId { get; set; }
        public bool IsNewNote => NoteId == 0;
        public bool CanSave => !IsBusy;
        public bool CanDelete => !IsBusy && !IsNewNote;
        public bool CanGoBack => !IsBusy;
        
        public bool HasUnsavedChanges
        {
            get => _hasUnsavedChanges;
            private set
            {
                _hasUnsavedChanges = value;
                OnPropertyChanged();
            }
        }

        // |---------------------|
        // |                     |
        // |      Commands       |
        // |                     |
        // |---------------------|
        public ICommand SaveCommand { get; }
        public ICommand BackCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ShowHelpCommand { get; }

        // |---------------------|
        // |                     |
        // |    Constructor      |
        // |                     |
        // |---------------------|
        public NoteDetailViewModel(INoteService noteService, ISettingsService settingsService, ILogger<NoteDetailViewModel>? logger = null) : base(logger)
        {
            _noteService = noteService;
            _settingsService = settingsService;
            
            SaveCommand = new Command(async () => await SaveNote(), () => CanSave);
            BackCommand = new Command(async () => await HandleBackNavigation(), () => CanGoBack);
            DeleteCommand = new Command(async () => await DeleteNote(), () => CanDelete);
            ShowHelpCommand = new Command(async () => await ShowHelp());
        }

        // |---------------------|
        // |                     |
        // |  Loading Methods    |
        // |                     |
        // |---------------------|
        public async Task LoadNote()
        {
            try
            {
                if (NoteId > 0)
                {
                    var note = await _noteService.GetNoteAsync(NoteId);
                    if (note != null)
                    {
                        _originalNote = new Note(note);
                        Note = new Note(note);
                        Title = Note.Title;
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Error", "Note not found.", "OK");
                        await Shell.Current.GoToAsync("..");
                        return;
                    }
                }
                else
                {
                    Title = "New Note";
                    Note = new Note
                    {
                        Category = "General",
                        Title = "",
                        Content = ""
                    };
                }
                
                StartAutoSave();
                RefreshCommandStates();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "Error loading note {NoteId}", NoteId);
                await Shell.Current.DisplayAlert("Error", "Could not load note. Please try again.", "OK");
            }
        }

        // |---------------------|
        // |                     |
        // |  Event Handlers     |
        // |                     |
        // |---------------------|
        private void OnNotePropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Note.Title) || e.PropertyName == nameof(Note.Content))
            {
                CheckForChanges();
                OnPropertyChanged(nameof(CanSave));
                OnPropertyChanged(nameof(CanDelete));
                RefreshCommandStates();
                
                RestartAutoSaveTimer();
            }
        }

        // |---------------------|
        // |                     |
        // |   Utility Methods   |
        // |                     |
        // |---------------------|
        private void RefreshCommandStates()
        {
            try
            {
                ((Command)SaveCommand).ChangeCanExecute();
                ((Command)BackCommand).ChangeCanExecute();
                ((Command)DeleteCommand).ChangeCanExecute();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "Error refreshing command states");
            }
        }

        private void CheckForChanges()
        {
            HasUnsavedChanges = Note.Title != _lastSavedTitle || Note.Content != _lastSavedContent;
        }

        // |---------------------|
        // |                     |
        // |   Auto-Save Logic   |
        // |                     |
        // |---------------------|
        private void StartAutoSave()
        {
            _autoSaveTimer?.Dispose();
            
            bool autoSaveEnabled = _settingsService?.Settings?.AutoSaveEnabled ?? true;
            int autoSaveInterval = _settingsService?.Settings?.AutoSaveInterval ?? 10;
            
            if (autoSaveEnabled)
            {
                var interval = TimeSpan.FromSeconds(autoSaveInterval);
                _autoSaveTimer = new Timer(async _ => await AutoSave(), null, TimeSpan.FromSeconds(5), interval);
            }
        }

        private void RestartAutoSaveTimer()
        {
            bool autoSaveEnabled = _settingsService?.Settings?.AutoSaveEnabled ?? true;
            int autoSaveInterval = _settingsService?.Settings?.AutoSaveInterval ?? 10;
            
            if (autoSaveEnabled)
            {
                var interval = TimeSpan.FromSeconds(autoSaveInterval);
                _autoSaveTimer?.Change(TimeSpan.FromSeconds(3), interval);
            }
        }

        private async Task AutoSave()
        {
            bool autoSaveEnabled = _settingsService?.Settings?.AutoSaveEnabled ?? true;
            
            if (HasUnsavedChanges && !IsBusy && !_isNavigating && autoSaveEnabled)
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(Note.Title) || !string.IsNullOrWhiteSpace(Note.Content))
                    {
                        if (string.IsNullOrWhiteSpace(Note.Title))
                        {
                            Note.Title = GenerateDefaultTitle();
                        }

                        await _noteService.SaveNoteAsync(Note);
                        
                        if (IsNewNote && Note.Id > 0)
                        {
                            NoteId = Note.Id;
                            OnPropertyChanged(nameof(IsNewNote));
                            OnPropertyChanged(nameof(CanDelete));
                            RefreshCommandStates();
                        }
                        
                        _lastSavedTitle = Note.Title;
                        _lastSavedContent = Note.Content;
                        
                        await MainThread.InvokeOnMainThreadAsync(() =>
                        {
                            HasUnsavedChanges = false;
                            Logger?.LogDebug("Auto-saved note: {NoteTitle}", Note.Title);
                        });
                    }
                }
                catch (Exception ex)
                {
                    Logger?.LogError(ex, "Auto-save failed for note: {NoteTitle}", Note.Title);
                }
            }
        }

        // |---------------------|
        // |                     |
        // |  Command Methods    |
        // |                     |
        // |---------------------|
        private async Task SaveNote()
        {
            if (IsBusy) return;
            
            try
            {
                IsBusy = true;
                
                if (string.IsNullOrWhiteSpace(Note.Title) && string.IsNullOrWhiteSpace(Note.Content))
                {
                    Note.Title = GenerateDefaultTitle();
                    Note.Content = "";
                }
                else if (string.IsNullOrWhiteSpace(Note.Title))
                {
                    Note.Title = GenerateDefaultTitle();
                }

                Logger?.LogDebug("Saving note: {NoteTitle}", Note.Title);

                var noteId = await _noteService.SaveNoteAsync(Note);
                NoteId = noteId;
                Note.Id = noteId;
                
                _lastSavedTitle = Note.Title;
                _lastSavedContent = Note.Content;
                HasUnsavedChanges = false;
                
                OnPropertyChanged(nameof(IsNewNote));
                OnPropertyChanged(nameof(CanSave));
                OnPropertyChanged(nameof(CanDelete));
                RefreshCommandStates();
                
                await Shell.Current.DisplayAlert("Saved", "Your note has been saved successfully.", "OK");
                
                Logger?.LogInformation("Successfully saved note with ID: {NoteId}", noteId);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "Error saving note");
                await Shell.Current.DisplayAlert("Error", "Could not save note. Please try again.", "OK");
            }
            finally
            {
                IsBusy = false;
                RefreshCommandStates();
            }
        }

        private async Task DeleteNote()
        {
            if (IsNewNote || IsBusy) return;

            try
            {
                bool confirm = await Shell.Current.DisplayAlert("Delete Note", 
                    $"Are you sure you want to delete '{Note.Title}'?", "Yes", "No");
                
                if (confirm)
                {
                    IsBusy = true;
                    _isNavigating = true;
                    
                    Logger?.LogDebug("Deleting note: {NoteId}", Note.Id);
                    
                    await _noteService.DeleteNoteAsync(Note);
                    await Shell.Current.GoToAsync("..");
                    
                    Logger?.LogInformation("Successfully deleted note: {NoteId}", Note.Id);
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "Error deleting note");
                await Shell.Current.DisplayAlert("Error", "Could not delete note. Please try again.", "OK");
            }
            finally
            {
                IsBusy = false;
                _isNavigating = false;
                RefreshCommandStates();
            }
        }

        private async Task HandleBackNavigation()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                _isNavigating = true;

                if (HasUnsavedChanges)
                {
                    var result = await Shell.Current.DisplayAlert("Unsaved Changes", 
                        "You have unsaved changes. Do you want to save before leaving?", 
                        "Save", "Don't Save");
                    
                    if (result == true)
                    {
                        if (CanSave)
                        {
                            if (IsNewNote && string.IsNullOrWhiteSpace(Note.Title))
                            {
                                Note.Title = GenerateDefaultTitle();
                            }
                            
                            await _noteService.SaveNoteAsync(Note);
                            Logger?.LogInformation("Auto-saved note before navigation");
                        }
                    }
                }
                
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "Error during back navigation");
                await Shell.Current.GoToAsync("..");
            }
            finally
            {
                IsBusy = false;
                _isNavigating = false;
            }
        }

        // |---------------------|
        // |                     |
        // |  Helper Methods     |
        // |                     |
        // |---------------------|
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
            return $"New Note {DateTime.Now:dd/MM/yyyy HH:mm}";
        }

        private async Task ShowHelp()
        {
            var helpText = @"📝 Quicknote Help

EDITING FEATURES:
Auto-save
Auto-title generation
Organise notes by category
Add comma-separated tags for better organisation


AUTO-SAVE:
• Saves 3 seconds after you stop typing
• Then saves every 10 seconds while editing
• Configure auto-save settings in the main Settings menu
• Orange dot (●) shows when there are unsaved changes

SHORTCUTS & NAVIGATION:
• Save button: Force save current note
• Delete button: Remove current note (with confirmation)
• Back button: Return to notes list (prompts to save if needed)
• Help button: Show this help menu

CUSTOMISATION:
Visit Settings from the main screen to:
• Enable Dark Mode
• Adjust Performance Mode
• Configure auto-save intervals
• Change default category

Version: Quicknote 4.1.0 - Fhox Edition 2025";

            await Shell.Current.DisplayAlert("Help - Quicknote", helpText, "Close");
        }

        // |---------------------|
        // |                     |
        // |  Cleanup Methods    |
        // |                     |
        // |---------------------|
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