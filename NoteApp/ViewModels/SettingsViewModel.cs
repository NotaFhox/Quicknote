using System.Collections.ObjectModel;
using System.Windows.Input;
using NoteApp.Models;
using NoteApp.Services;
using Microsoft.Extensions.Logging;

namespace NoteApp.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly ISettingsService _settingsService;
        
        public AppSettings Settings => _settingsService.Settings;

        public ObservableCollection<string> AvailableCategories { get; }
        public ObservableCollection<string> FontFamilies { get; }
        public ObservableCollection<int> FontSizes { get; }
        public ObservableCollection<int> AutoSaveIntervals { get; }

        public ICommand ResetSettingsCommand { get; }
        public ICommand BackCommand { get; }
        public ICommand ClearDataCommand { get; }

        public SettingsViewModel(ISettingsService settingsService, ILogger<SettingsViewModel>? logger = null) 
            : base(logger)
        {
            _settingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsService));
            Title = "Settings";

            Logger?.LogDebug("SettingsViewModel constructor - Settings service: {HasService}", _settingsService != null);
            Logger?.LogDebug("Settings object: {HasSettings}", _settingsService.Settings != null);

            // Initialize collections
            AvailableCategories = new ObservableCollection<string>
            {
                "General", "Work", "Personal", "Ideas", "Tasks", 
                "Meeting", "Project", "Research", "Important", "Archive"
            };

            FontFamilies = new ObservableCollection<string>
            {
                "System Default", "MS Sans Serif", "Courier New", 
                "Arial", "Times New Roman", "Segoe UI"
            };

            FontSizes = new ObservableCollection<int>
            {
                8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24
            };

            AutoSaveIntervals = new ObservableCollection<int>
            {
                5, 10, 15, 30, 60, 120, 300
            };

            // Commands
            ResetSettingsCommand = CreateAsyncCommand(ResetSettings);
            BackCommand = CreateAsyncCommand(GoBack);
            ClearDataCommand = CreateAsyncCommand(ClearAllData);

            // Load settings on initialization
            Initialize();
        }

        public void Initialize()
        {
            try
            {
                Logger?.LogDebug("SettingsViewModel Initialize called");
                Logger?.LogDebug("Settings service: {HasService}", _settingsService != null);
                Logger?.LogDebug("Settings object: {HasSettings}", _settingsService?.Settings != null);
                
                if (_settingsService?.Settings != null)
                {
                    Logger?.LogDebug("Dark mode: {DarkMode}", _settingsService.Settings.IsDarkMode);
                    Logger?.LogDebug("Auto-save enabled: {AutoSave}", _settingsService.Settings.AutoSaveEnabled);
                    Logger?.LogDebug("Font size: {FontSize}", _settingsService.Settings.FontSize);
                }

                // Ensure settings are loaded
                _settingsService?.LoadSettings();
                
                // Notify UI that settings are available
                OnPropertyChanged(nameof(Settings));
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "Error initializing settings view model");
            }
        }

        private async Task ResetSettings()
        {
            try
            {
                var confirm = await Shell.Current.DisplayAlert(
                    "Reset Settings", 
                    "Are you sure you want to reset all settings to defaults? This cannot be undone.", 
                    "Reset", 
                    "Cancel");

                if (confirm)
                {
                    _settingsService.ResetToDefaults();
                    
                    // Force UI update
                    OnPropertyChanged(nameof(Settings));
                    
                    await Shell.Current.DisplayAlert("Settings Reset", "All settings have been reset to defaults.", "OK");
                    
                    Logger?.LogInformation("Settings reset to defaults");
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "Error resetting settings");
                await Shell.Current.DisplayAlert("Error", "Could not reset settings. Please try again.", "OK");
            }
        }

        private async Task GoBack()
        {
            try
            {
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "Error navigating back");
                await Shell.Current.DisplayAlert("Error", "Could not navigate back. Please try again.", "OK");
            }
        }

        private async Task ClearAllData()
        {
            try
            {
                var confirm = await Shell.Current.DisplayAlert(
                    "Clear All Data", 
                    "⚠️ WARNING: This will permanently delete ALL your notes and settings. This action cannot be undone!\n\nAre you absolutely sure?", 
                    "DELETE ALL", 
                    "Cancel");

                if (confirm)
                {
                    var doubleConfirm = await Shell.Current.DisplayAlert(
                        "Final Confirmation", 
                        "This is your last chance! All notes will be permanently lost.\n\nConfirm deletion?", 
                        "YES, DELETE EVERYTHING", 
                        "Cancel");

                    if (doubleConfirm)
                    {
                        await ExecuteAsync(async () =>
                        {
                            // Clear preferences
                            Preferences.Clear();
                            
                            // Delete database file
                            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "notes.db");
                            if (File.Exists(dbPath))
                            {
                                File.Delete(dbPath);
                            }

                            // Delete any other app data files
                            var appDataFiles = Directory.GetFiles(FileSystem.AppDataDirectory, "*.*");
                            foreach (var file in appDataFiles)
                            {
                                try
                                {
                                    File.Delete(file);
                                }
                                catch
                                {
                                    // Ignore individual file delete errors
                                }
                            }

                            await Shell.Current.DisplayAlert(
                                "Data Cleared", 
                                "All notes and settings have been deleted. The app will now restart.", 
                                "OK");

                            // Force restart by closing the app
                            System.Environment.Exit(0);
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "Error clearing all data");
                await Shell.Current.DisplayAlert("Error", "Could not clear all data. Please try again.", "OK");
            }
        }
    }
}