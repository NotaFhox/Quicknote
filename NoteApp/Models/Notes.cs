using System.ComponentModel;

namespace NoteApp.Models
{
    public class Note : INotifyPropertyChanged
    {
        private string _title = string.Empty;
        private string _content = string.Empty;
        private DateTime _dateCreated;
        private DateTime _dateModified;
        private string _category = "General";
        private string _tags = string.Empty;

        public int Id { get; set; }
        
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                DateModified = DateTime.Now;
                OnPropertyChanged();
            }
        }

        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                DateModified = DateTime.Now;
                OnPropertyChanged();
            }
        }

        public string Category
        {
            get => _category;
            set
            {
                _category = value;
                DateModified = DateTime.Now;
                OnPropertyChanged();
            }
        }

        public string Tags
        {
            get => _tags;
            set
            {
                _tags = value;
                DateModified = DateTime.Now;
                OnPropertyChanged();
            }
        }

        public DateTime DateCreated
        {
            get => _dateCreated;
            set
            {
                _dateCreated = value;
                OnPropertyChanged();
            }
        }

        public DateTime DateModified
        {
            get => _dateModified;
            set
            {
                _dateModified = value;
                OnPropertyChanged();
            }
        }

        public Note()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}