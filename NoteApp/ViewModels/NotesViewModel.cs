using System.ComponentModel;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
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
        private List<Note> _allNotes = new();
        private CancellationTokenSource? _searchCancellationTokenSource;
        private bool _isRefreshing;
        private string _sortBy = "DateModified";
        private bool _sortAscending = false;
        private int _currentPage = 1;
        private const int PageSize = 20;
        private bool _hasMoreItems = false;
        private bool _isLoadingMore = false;

        public ObservableCollection<Note> Notes { get; set; } = new();
        public ObservableCollection<Note> FilteredNotes { get; private set; } = new();

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged();
                    DelayedSearch();
                }
            }
        }

        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (_selectedCategory != value)
                {
                    _selectedCategory = value;
                    OnPropertyChanged();
                    _ = FilterNotesAsync();
                }
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

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
                OnPropertyChanged();
            }
        }

        public string SortBy
        {
            get => _sortBy;
            set
            {
                if (_sortBy != value)
                {
                    _sortBy = value;
                    OnPropertyChanged();
                    SortNotes();
                }
            }
        }

        public bool SortAscending
        {
            get => _sortAscending;
            set
            {
                if (_sortAscending != value)
                {
                    _sortAscending = value;
                    OnPropertyChanged();
                    SortNotes();
                }
            }
        }

        public bool HasMoreItems
        {
            get => _hasMoreItems;
            set
            {
                _hasMoreItems = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoadingMore
        {
            get => _isLoadingMore;
            set
            {
                _isLoadingMore = value;
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
        public ICommand RefreshCommand { get; }
        public ICommand ToggleSortOrderCommand { get; }
        public ICommand LoadMoreCommand { get; }
        public ICommand QuickAddNoteCommand { get; }
        public ICommand DeleteSelectedNotesCommand { get; }
        public ICommand ShowHelpCommand { get; }
        public ICommand OpenSettingsCommand { get; }

        public NotesViewModel(INoteService noteService, ILogger<NotesViewModel>? logger = null) : base(logger)
        {
            _noteService = noteService;
            Title = "My Notes";

            // Initialize collections
            Notes = new ObservableCollection<Note>();
            FilteredNotes = new ObservableCollection<Note>();
            Categories = new List<string> { "All" };

            LoadNotesCommand = CreateAsyncCommand(LoadNotes);
            AddNoteCommand = CreateAsyncCommand(AddNote);
            SelectNoteCommand = CreateAsyncCommand<Note>(SelectNote);
            DeleteNoteCommand = CreateAsyncCommand<Note>(DeleteNote);
            SearchCommand = CreateAsyncCommand(SearchNotes);
            ClearSearchCommand = CreateCommand(() => 
            {
                SearchText = string.Empty;
                _ = LoadNotes();
            });
            FilterByCategoryCommand = CreateAsyncCommand(FilterByCategory);
            RefreshCommand = CreateAsyncCommand(RefreshNotes);
            ToggleSortOrderCommand = CreateCommand(() => 
            {
                SortAscending = !SortAscending;
            });
            LoadMoreCommand = CreateAsyncCommand(LoadMoreNotes);
            QuickAddNoteCommand = CreateAsyncCommand<string>(QuickAddNote);
            DeleteSelectedNotesCommand = CreateAsyncCommand<List<Note>>(DeleteSelectedNotes);
            ShowHelpCommand = CreateAsyncCommand(ShowHelp);
            OpenSettingsCommand = CreateAsyncCommand(OpenSettings);
        }

        private async Task LoadNotes()
        {
            if (IsBusy) return;
            
            await ExecuteAsync(async () =>
            {
                _currentPage = 1;
                
                // Clear collections on UI thread
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    Notes.Clear();
                    FilteredNotes.Clear();
                });
                
                var sw = Stopwatch.StartNew();
                _allNotes = await _noteService.GetNotesAsync();
                
                Categories = await _noteService.GetCategoriesAsync();
                
                await FilterNotesAsync();
                
                sw.Stop();
                Debug.WriteLine($"Loaded {_allNotes.Count} notes in {sw.ElapsedMilliseconds}ms");
            });
        }

        private async Task LoadMoreNotes()
        {
            if (IsLoadingMore || !HasMoreItems) return;
            
            IsLoadingMore = true;
            try
            {
                _currentPage++;
                var startIndex = (_currentPage - 1) * PageSize;
                var endIndex = Math.Min(startIndex + PageSize, FilteredNotes.Count);
                
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    for (int i = startIndex; i < endIndex; i++)
                    {
                        Notes.Add(FilteredNotes[i]);
                    }
                    
                    HasMoreItems = Notes.Count < FilteredNotes.Count;
                });
            }
            finally
            {
                IsLoadingMore = false;
            }
        }

        private async Task FilterNotesAsync()
        {
            await Task.Run(async () =>
            {
                var filtered = _allNotes.AsEnumerable();
                
                if (!string.IsNullOrWhiteSpace(SearchText))
                {
                    var searchTerm = SearchText.ToLowerInvariant();
                    filtered = filtered.Where(n => n.MatchesSearchTerm(searchTerm));
                }
                
                if (!string.IsNullOrWhiteSpace(SelectedCategory) && SelectedCategory != "All")
                {
                    filtered = filtered.Where(n => n.Category == SelectedCategory);
                }
                
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    FilteredNotes.Clear();
                    foreach (var note in filtered)
                    {
                        FilteredNotes.Add(note);
                    }
                });
                
                SortNotes();
                await LoadFirstPageAsync();
            });
        }

        private async Task LoadFirstPageAsync()
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Notes.Clear();
                _currentPage = 1;
                
                var endIndex = Math.Min(PageSize, FilteredNotes.Count);
                for (int i = 0; i < endIndex; i++)
                {
                    Notes.Add(FilteredNotes[i]);
                }
                
                HasMoreItems = Notes.Count < FilteredNotes.Count;
            });
        }

        private void SortNotes()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                var sorted = SortBy switch
                {
                    "Title" => SortAscending 
                        ? FilteredNotes.OrderBy(n => n.Title).ToList()
                        : FilteredNotes.OrderByDescending(n => n.Title).ToList(),
                    "Category" => SortAscending
                        ? FilteredNotes.OrderBy(n => n.Category).ThenByDescending(n => n.DateModified).ToList()
                        : FilteredNotes.OrderByDescending(n => n.Category).ThenByDescending(n => n.DateModified).ToList(),
                    "DateCreated" => SortAscending
                        ? FilteredNotes.OrderBy(n => n.DateCreated).ToList()
                        : FilteredNotes.OrderByDescending(n => n.DateCreated).ToList(),
                    _ => SortAscending
                        ? FilteredNotes.OrderBy(n => n.DateModified).ToList()
                        : FilteredNotes.OrderByDescending(n => n.DateModified).ToList()
                };
                
                FilteredNotes.Clear();
                foreach (var note in sorted)
                {
                    FilteredNotes.Add(note);
                }
            });
        }

        private async void DelayedSearch()
        {
            _searchCancellationTokenSource?.Cancel();
            _searchCancellationTokenSource = new CancellationTokenSource();
            
            try
            {
                await Task.Delay(300, _searchCancellationTokenSource.Token);
                await FilterNotesAsync();
            }
            catch (TaskCanceledException)
            {
                // Search was cancelled, this is expected
            }
        }

        private async Task SearchNotes()
        {
            await FilterNotesAsync();
        }

        private async Task RefreshNotes()
        {
            IsRefreshing = true;
            try
            {
                await LoadNotes();
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        private async Task AddNote()
        {
            try
            {
                Logger?.LogDebug("AddNote command triggered");
                
                var newNote = new Note
                {
                    Title = "",  // Start with empty title
                    Content = "", // Start with empty content
                    Category = SelectedCategory == "All" ? "General" : SelectedCategory
                };
                
                Logger?.LogDebug("Navigating to NoteDetailPage with new note");
                
                await Shell.Current.GoToAsync($"{nameof(NoteDetailPage)}?NoteId=0", new Dictionary<string, object>
                {
                    ["Note"] = newNote
                });
                
                Logger?.LogDebug("Navigation to NoteDetailPage completed");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "Error navigating to new note");
                await Shell.Current.DisplayAlert("Error", "Could not create new note. Please try again.", "OK");
            }
        }

        private async Task QuickAddNote(string title)
        {
            if (string.IsNullOrWhiteSpace(title)) return;
            
            try
            {
                var newNote = new Note
                {
                    Title = title,
                    Content = "",
                    Category = SelectedCategory == "All" ? "General" : SelectedCategory
                };
                
                await _noteService.SaveNoteAsync(newNote);
                await LoadNotes(); // Refresh the list
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "Error creating quick note");
                await Shell.Current.DisplayAlert("Error", "Could not create note. Please try again.", "OK");
            }
        }

        private async Task SelectNote(Note? note)
        {
            if (note == null) return;
            
            try
            {
                await Shell.Current.GoToAsync($"{nameof(NoteDetailPage)}?NoteId={note.Id}");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "Error navigating to note {NoteId}", note.Id);
                await Shell.Current.DisplayAlert("Error", "Could not open note. Please try again.", "OK");
            }
        }

        private async Task DeleteNote(Note? note)
        {
            if (note == null) return;

            try
            {
                bool confirm = await Shell.Current.DisplayAlert("Delete Note", 
                    $"Are you sure you want to delete '{note.Title}'?", "Yes", "No");
                
                if (confirm)
                {
                    await _noteService.DeleteNoteAsync(note);
                    
                    // Remove from local collections immediately for UI responsiveness
                    await MainThread.InvokeOnMainThreadAsync(() =>
                    {
                        _allNotes.Remove(note);
                        FilteredNotes.Remove(note);
                        Notes.Remove(note);
                    });
                    
                    // Optionally refresh to ensure consistency
                    await LoadNotes();
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "Error deleting note {NoteId}", note.Id);
                await Shell.Current.DisplayAlert("Error", "Could not delete note. Please try again.", "OK");
                
                // Refresh to restore UI state if delete failed
                await LoadNotes();
            }
        }

        private async Task DeleteSelectedNotes(List<Note>? notes)
        {
            if (notes == null || !notes.Any()) return;
            
            try
            {
                var count = notes.Count;
                bool confirm = await Shell.Current.DisplayAlert("Delete Notes", 
                    $"Are you sure you want to delete {count} note{(count > 1 ? "s" : "")}?", "Yes", "No");
                
                if (confirm)
                {
                    await _noteService.DeleteMultipleNotesAsync(notes);
                    
                    // Remove from local collections
                    await MainThread.InvokeOnMainThreadAsync(() =>
                    {
                        foreach (var note in notes)
                        {
                            _allNotes.Remove(note);
                            FilteredNotes.Remove(note);
                            Notes.Remove(note);
                        }
                    });
                    
                    // Refresh to ensure consistency
                    await LoadNotes();
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "Error deleting multiple notes");
                await Shell.Current.DisplayAlert("Error", "Could not delete notes. Please try again.", "OK");
                
                // Refresh to restore UI state
                await LoadNotes();
            }
        }

        private async Task FilterByCategory()
        {
            await FilterNotesAsync();
        }

        public async Task OnAppearing()
        {
            await LoadNotes();
        }

        private async Task ShowHelp()
        {
            var helpText = @"📝 Quicknote Help



NAVIGATION:
• Double-click any note to open it
• Use Open/Delete buttons on each note
• Click 'New' button to create notes

AUTO-SAVE:
Notes auto-save as you type 
Configure auto-save interval in Settings.

ORGANISATION:
• Add categories: Work, Personal, Ideas, etc.
• Use tags: comma, separated, tags

CUSTOMISATION:
• Dark Mode
• Performance Mode: Optimised for slower devices
• Font settings and more in Settings

DATABASE:
Your notes are stored locally on this device.

Version: Quicknote 4.1.0 - Fhox Edition 2025";

            await Shell.Current.DisplayAlert("Help - Quicknote", helpText, "Close");
        }

        private async Task OpenSettings()
        {
            try
            {
                Logger?.LogDebug("Opening settings page");
                System.Diagnostics.Debug.WriteLine("Attempting to navigate to SettingsPage");
                
                await Shell.Current.GoToAsync(nameof(SettingsPage));
                
                Logger?.LogDebug("Successfully navigated to settings");
                System.Diagnostics.Debug.WriteLine("Navigation to SettingsPage completed");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "Error navigating to settings");
                System.Diagnostics.Debug.WriteLine($"Settings navigation error: {ex.Message}");
                
                // Try alternative navigation method
                try
                {
                    await Shell.Current.GoToAsync("//SettingsPage");
                }
                catch (Exception ex2)
                {
                    Logger?.LogError(ex2, "Error with alternative navigation to settings");
                    await Shell.Current.DisplayAlert("Error", $"Could not open settings. Error: {ex.Message}", "OK");
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _searchCancellationTokenSource?.Cancel();
                _searchCancellationTokenSource?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}