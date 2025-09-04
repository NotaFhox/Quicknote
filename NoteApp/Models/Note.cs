using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace NoteApp.Models
{
    public class Note : INotifyPropertyChanged
    {
        // |---------------------|
        // |                     |
        // |   Private Fields    |
        // |                     |
        // |---------------------|
        private string _title = string.Empty;
        private string _content = string.Empty;
        private DateTime _dateCreated;
        private DateTime _dateModified;
        private string _category = "General";
        private string _tags = string.Empty;
        private string? _contentPreview;
        private string? _formattedDateCreated;
        private string? _formattedDateModified;
        private List<string>? _tagList;

        // |---------------------|
        // |                     |
        // |  Core Properties    |
        // |                     |
        // |---------------------|
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value ?? string.Empty;
                    DateModified = DateTime.Now;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(HasContent));
                }
            }
        }

        [StringLength(5000, ErrorMessage = "Content cannot exceed 5000 characters")]
        public string Content
        {
            get => _content;
            set
            {
                if (_content != value)
                {
                    _content = value ?? string.Empty;
                    _contentPreview = null;
                    DateModified = DateTime.Now;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(HasContent));
                    OnPropertyChanged(nameof(ContentPreview));
                }
            }
        }

        // |---------------------|
        // |                     |
        // |Organization Fields  |
        // |                     |
        // |---------------------|
        [StringLength(50, ErrorMessage = "Category cannot exceed 50 characters")]
        public string Category
        {
            get => _category;
            set
            {
                if (_category != value)
                {
                    _category = value ?? "General";
                    DateModified = DateTime.Now;
                    OnPropertyChanged();
                }
            }
        }

        [StringLength(500, ErrorMessage = "Tags cannot exceed 500 characters")]
        public string Tags
        {
            get => _tags;
            set
            {
                if (_tags != value)
                {
                    _tags = value ?? string.Empty;
                    _tagList = null;
                    DateModified = DateTime.Now;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TagList));
                }
            }
        }

        // |---------------------|
        // |                     |
        // |  Timestamp Fields   |
        // |                     |
        // |---------------------|
        public DateTime DateCreated
        {
            get => _dateCreated;
            set
            {
                if (_dateCreated != value)
                {
                    _dateCreated = value;
                    _formattedDateCreated = null;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(FormattedDateCreated));
                }
            }
        }

        public DateTime DateModified
        {
            get => _dateModified;
            set
            {
                if (_dateModified != value)
                {
                    _dateModified = value;
                    _formattedDateModified = null;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(FormattedDateModified));
                    OnPropertyChanged(nameof(TimeAgo));
                }
            }
        }

        // |---------------------|
        // |                     |
        // | Computed Properties |
        // |                     |
        // |---------------------|
        public bool HasContent => !string.IsNullOrWhiteSpace(Title) || !string.IsNullOrWhiteSpace(Content);
        
        public string ContentPreview 
        { 
            get 
            {
                if (_contentPreview == null)
                {
                    if (string.IsNullOrWhiteSpace(Content))
                        _contentPreview = "No content";
                    else
                    {
                        const int maxLength = 100;
                        var cleanContent = Content.Replace("\r\n", " ").Replace("\n", " ").Replace("  ", " ").Trim();
                        _contentPreview = cleanContent.Length <= maxLength 
                            ? cleanContent 
                            : cleanContent[..maxLength] + "...";
                    }
                }
                return _contentPreview;
            }
        }

        public string FormattedDateCreated => _formattedDateCreated ??= DateCreated.ToString("dd MMM yyyy HH:mm");
        public string FormattedDateModified => _formattedDateModified ??= DateModified.ToString("dd MMM yyyy HH:mm");

        public string TimeAgo
        {
            get
            {
                var timeSpan = DateTime.Now - DateModified;
                
                return timeSpan.TotalDays switch
                {
                    >= 365 => $"{(int)(timeSpan.TotalDays / 365)} year{((int)(timeSpan.TotalDays / 365) == 1 ? "" : "s")} ago",
                    >= 30 => $"{(int)(timeSpan.TotalDays / 30)} month{((int)(timeSpan.TotalDays / 30) == 1 ? "" : "s")} ago",
                    >= 1 => $"{(int)timeSpan.TotalDays} day{((int)timeSpan.TotalDays == 1 ? "" : "s")} ago",
                    >= 1.0/24 => $"{(int)timeSpan.TotalHours} hour{((int)timeSpan.TotalHours == 1 ? "" : "s")} ago",
                    >= 1.0/(24*60) => $"{(int)timeSpan.TotalMinutes} minute{((int)timeSpan.TotalMinutes == 1 ? "" : "s")} ago",
                    _ => "Just now"
                };
            }
        }

        public List<string> TagList
        {
            get
            {
                if (_tagList == null)
                {
                    if (string.IsNullOrWhiteSpace(Tags))
                        _tagList = new List<string>();
                    else
                    {
                        _tagList = Tags.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                  .Select(tag => tag.Trim())
                                  .Where(tag => !string.IsNullOrWhiteSpace(tag))
                                  .ToList();
                    }
                }
                return _tagList;
            }
        }

        // |---------------------|
        // |                     |
        // |    Constructors     |
        // |                     |
        // |---------------------|
        public Note()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }

        public Note(Note other)
        {
            ArgumentNullException.ThrowIfNull(other);
            
            Id = other.Id;
            Title = other.Title;
            Content = other.Content;
            Category = other.Category;
            Tags = other.Tags;
            DateCreated = other.DateCreated;
            DateModified = other.DateModified;
        }

        // |---------------------|
        // |                     |
        // | Property Changed    |
        // |                     |
        // |---------------------|
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // |---------------------|
        // |                     |
        // |   Validation        |
        // |                     |
        // |---------------------|
        public List<ValidationResult> Validate()
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(this);
            
            Validator.TryValidateObject(this, context, results, true);
            
            return results;
        }

        public bool IsValid => !Validate().Any();

        // |---------------------|
        // |                     |
        // |  Object Overrides   |
        // |                     |
        // |---------------------|
        public override bool Equals(object? obj)
        {
            return obj is Note other && Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"Note: {Title} ({Id})";
        }

        // |---------------------|
        // |                     |
        // |  Utility Methods    |
        // |                     |
        // |---------------------|
        public bool MatchesSearchTerm(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return true;

            var term = searchTerm.ToLowerInvariant();
            return Title.ToLowerInvariant().Contains(term) ||
                   Content.ToLowerInvariant().Contains(term) ||
                   Category.ToLowerInvariant().Contains(term) ||
                   Tags.ToLowerInvariant().Contains(term);
        }

        // |---------------------|
        // |                     |
        // |   Tag Management    |
        // |                     |
        // |---------------------|
        public void AddTag(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag))
                return;

            var currentTags = TagList;
            var normalizedTag = tag.Trim();
            
            if (!currentTags.Contains(normalizedTag, StringComparer.OrdinalIgnoreCase))
            {
                currentTags.Add(normalizedTag);
                Tags = string.Join(", ", currentTags);
            }
        }

        public void RemoveTag(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag))
                return;

            var currentTags = TagList;
            var normalizedTag = tag.Trim();
            
            var tagToRemove = currentTags.FirstOrDefault(t => 
                string.Equals(t, normalizedTag, StringComparison.OrdinalIgnoreCase));
            
            if (tagToRemove != null)
            {
                currentTags.Remove(tagToRemove);
                Tags = string.Join(", ", currentTags);
            }
        }
    }
}