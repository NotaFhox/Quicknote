using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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

        public NotesViewModel(INoteService noteService)
        {
            _noteService = noteService;
            Title = "My Notes";

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
        }

        private async Task LoadNotes()
        {
            if (IsBusy) return;
            
            await ExecuteAsync(async () =>
            {
                _currentPage = 1;
                Notes.Clear();
                FilteredNotes.Clear();
                
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
                
                for (int i = startIndex; i < endIndex; i++)
                {
                    Notes.Add(FilteredNotes[i]);
                }
                
                HasMoreItems = Notes.Count < FilteredNotes.Count;
            }
            finally
            {
                IsLoadingMore = false;
            }
        }

        private async Task FilterNotesAsync()
        {
            await Task.Run(() =>
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
                
                FilteredNotes.Clear();
                foreach (var note in filtered)
                {
                    FilteredNotes.Add(note);
                }
                
                SortNotes();
                LoadFirstPage();
            });
        }

        private void LoadFirstPage()
        {
            Notes.Clear();
            _currentPage = 1;
            
            var endIndex = Math.Min(PageSize, FilteredNotes.Count);
            for (int i = 0; i < endIndex; i++)
            {
                Notes.Add(FilteredNotes[i]);
            }
            
            HasMoreItems = Notes.Count < FilteredNotes.Count;
        }

        private void SortNotes()
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
            
            LoadFirstPage();
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
            }
        }

        private async Task SearchNotes()
        {
            await FilterNotesAsync();
        }

        private async Task RefreshNotes()
        {
            IsRefreshing = true;
            await LoadNotes();
            IsRefreshing = false;
        }

        private async Task AddNote()
        {
            var newNote = new Note
            {
                Title = "New Note",
                Content = "",
                Category = SelectedCategory == "All" ? "General" : SelectedCategory
            };
            
            await Shell.Current.GoToAsync($"{nameof(NoteDetailPage)}?NoteId=0", new Dictionary<string, object>
            {
                ["Note"] = newNote
            });
        }

        private async Task QuickAddNote(string title)
        {
            if (string.IsNullOrWhiteSpace(title)) return;
            
            var newNote = new Note
            {
                Title = title,
                Content = "",
                Category = SelectedCategory == "All" ? "General" : SelectedCategory
            };
            
            await _noteService.SaveNoteAsync(newNote);
            await LoadNotes();
        }

        private async Task SelectNote(Note? note)
        {
            if (note == null) return;
            
            await Shell.Current.GoToAsync($"{nameof(NoteDetailPage)}?NoteId={note.Id}");
        }

        private async Task DeleteNote(Note? note)
        {
            if (note == null) return;

            bool confirm = await Shell.Current.DisplayAlert("Delete Note", 
                $"Are you sure you want to delete '{note.Title}'?", "Yes", "No");
            
            if (confirm)
            {
                await _noteService.DeleteNoteAsync(note);
                _allNotes.Remove(note);
                FilteredNotes.Remove(note);
                Notes.Remove(note);
            }
        }

        private async Task DeleteSelectedNotes(List<Note>? notes)
        {
            if (notes == null || !notes.Any()) return;
            
            var count = notes.Count;
            bool confirm = await Shell.Current.DisplayAlert("Delete Notes", 
                $"Are you sure you want to delete {count} note{(count > 1 ? "s" : "")}?", "Yes", "No");
            
            if (confirm)
            {
                await _noteService.DeleteMultipleNotesAsync(notes);
                foreach (var note in notes)
                {
                    _allNotes.Remove(note);
                    FilteredNotes.Remove(note);
                    Notes.Remove(note);
                }
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
    }
}