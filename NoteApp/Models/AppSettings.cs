using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NoteApp.Models
{
    public class AppSettings : INotifyPropertyChanged
    {
        private bool _isDarkMode = false;
        private bool _isPerformanceMode = false;
        private bool _autoSaveEnabled = true;
        private int _autoSaveInterval = 10; // seconds
        private string _defaultCategory = "General";
        private bool _showLineNumbers = false;
        private int _fontSize = 12;
        private string _fontFamily = "System Default";

        public bool IsDarkMode
        {
            get => _isDarkMode;
            set
            {
                if (_isDarkMode != value)
                {
                    _isDarkMode = value;
                    OnPropertyChanged();
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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void LoadFromPreferences()
        {
            IsDarkMode = Preferences.Get(nameof(IsDarkMode), false);
            IsPerformanceMode = Preferences.Get(nameof(IsPerformanceMode), false);
            AutoSaveEnabled = Preferences.Get(nameof(AutoSaveEnabled), true);
            AutoSaveInterval = Preferences.Get(nameof(AutoSaveInterval), 10);
            DefaultCategory = Preferences.Get(nameof(DefaultCategory), "General");
            ShowLineNumbers = Preferences.Get(nameof(ShowLineNumbers), false);
            FontSize = Preferences.Get(nameof(FontSize), 12);
            FontFamily = Preferences.Get(nameof(FontFamily), "System Default");
        }

        public void SaveToPreferences()
        {
            Preferences.Set(nameof(IsDarkMode), IsDarkMode);
            Preferences.Set(nameof(IsPerformanceMode), IsPerformanceMode);
            Preferences.Set(nameof(AutoSaveEnabled), AutoSaveEnabled);
            Preferences.Set(nameof(AutoSaveInterval), AutoSaveInterval);
            Preferences.Set(nameof(DefaultCategory), DefaultCategory);
            Preferences.Set(nameof(ShowLineNumbers), ShowLineNumbers);
            Preferences.Set(nameof(FontSize), FontSize);
            Preferences.Set(nameof(FontFamily), FontFamily);
        }
    }
}