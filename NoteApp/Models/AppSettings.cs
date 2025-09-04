using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NoteApp.Models
{
    public class AppSettings : INotifyPropertyChanged
    {
        // |---------------------|
        // |                     |
        // |   Private Fields    |
        // |                     |
        // |---------------------|
        private bool _isDarkMode = false;
        private bool _isPerformanceMode = false;
        private bool _autoSaveEnabled = true;
        private int _autoSaveInterval = 10;
        private string _defaultCategory = "General";
        private bool _showLineNumbers = false;
        private int _fontSize = 12;
        private string _fontFamily = "System Default";

        // |---------------------|
        // |                     |
        // |  Display Properties |
        // |                     |
        // |---------------------|
        public bool IsDarkMode
        {
            get => _isDarkMode;
            set
            {
                if (_isDarkMode != value)
                {
                    _isDarkMode = value;
                    OnPropertyChanged();
                    System.Diagnostics.Debug.WriteLine($"Dark mode changed to: {value}");
                }
            }
        }

        public bool IsPerformanceMode
        {
            get => _isPerformanceMode;
            set
            {
                if (_isPerformanceMode != value)
                {
                    _isPerformanceMode = value;
                    OnPropertyChanged();
                }
            }
        }

        // |---------------------|
        // |                     |
        // | Auto-Save Settings  |
        // |                     |
        // |---------------------|
        public bool AutoSaveEnabled
        {
            get => _autoSaveEnabled;
            set
            {
                if (_autoSaveEnabled != value)
                {
                    _autoSaveEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        public int AutoSaveInterval
        {
            get => _autoSaveInterval;
            set
            {
                if (_autoSaveInterval != value && value >= 5 && value <= 300)
                {
                    _autoSaveInterval = value;
                    OnPropertyChanged();
                }
            }
        }

        // |---------------------|
        // |                     |
        // |  Content Settings   |
        // |                     |
        // |---------------------|
        public string DefaultCategory
        {
            get => _defaultCategory;
            set
            {
                if (_defaultCategory != value)
                {
                    _defaultCategory = value ?? "General";
                    OnPropertyChanged();
                }
            }
        }

        public bool ShowLineNumbers
        {
            get => _showLineNumbers;
            set
            {
                if (_showLineNumbers != value)
                {
                    _showLineNumbers = value;
                    OnPropertyChanged();
                }
            }
        }

        // |---------------------|
        // |                     |
        // |   Font Settings     |
        // |                     |
        // |---------------------|
        public int FontSize
        {
            get => _fontSize;
            set
            {
                if (_fontSize != value && value >= 8 && value <= 24)
                {
                    _fontSize = value;
                    OnPropertyChanged();
                }
            }
        }

        public string FontFamily
        {
            get => _fontFamily;
            set
            {
                if (_fontFamily != value)
                {
                    _fontFamily = value ?? "System Default";
                    OnPropertyChanged();
                }
            }
        }

        // |---------------------|
        // |                     |
        // | Property Changed    |
        // |                     |
        // |---------------------|
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            try
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in OnPropertyChanged for {propertyName}: {ex.Message}");
            }
        }
    }
}