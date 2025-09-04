using System.Collections.ObjectModel;
using System.Windows.Input;
using NoteApp.Models;
using NoteApp.Services;
using Microsoft.Extensions.Logging;

namespace NoteApp.ViewModels
{
    // |------------------------------------------------------|
    // |                                                      |
    // |               Settings View Model                    |
    // |                                                      |
    // |------------------------------------------------------|
    public class SettingsViewModel : BaseViewModel
    {
        // The service for managing application settings
        private readonly ISettingsService _settingsService;
        
        // Property to bind settings directly to the UI
        public AppSettings Settings => _settingsService.Settings;

        // Collections for UI dropdown menus
        public ObservableCollection<string> AvailableCategories { get; }
        public ObservableCollection<string> FontFamilies { get; }
        public ObservableCollection<int> FontSizes { get; }
        public ObservableCollection<int> AutoSaveIntervals { get; }

        // Commands for user actions
        public ICommand ResetSettingsCommand { get; }
        public ICommand BackCommand { get; }
        public ICommand ClearDataCommand { get; }

        // |------------------------------------------------------|
        // |                                                      |
        // |                 Constructor                          |
        // |                                                      |
        // |------------------------------------------------------|
        public SettingsViewModel(ISettingsService settingsService, ILogger<SettingsViewModel>? logger = null) 
            : base(logger)
        {
            // Ensure the settings service is not null
            _settingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsService));
            Title = "Settings";

            Logger?.LogDebug("SettingsViewModel constructor - Settings service: {HasService}", _settingsService != null);
            Logger?.LogDebug("Settings object: {HasSettings}", _settingsService.Settings != null);

            // Populate collections with predefined values
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

            
            ResetSettingsCommand = CreateAsyncCommand(ResetSettings);
            BackCommand = CreateAsyncCommand(GoBack);
            ClearDataCommand = CreateAsyncCommand(ClearAllData);

            
            Initialize();
        }

        // Initializes the view model by loading settings
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

                // Load the settings from storage
                _settingsService?.LoadSettings();
                
                // Notify the UI that the settings data has changed
                OnPropertyChanged(nameof(Settings));
            }
            catch (Exception ex)
            {
                // Log any errors that occur during initialization
                Logger?.LogError(ex, "Error initializing settings view model");
            }
        }

        // Resets all application settings to their default values
        private async Task ResetSettings()
        {
            try
            {
                // Prompt the user for confirmation
                var confirm = await Shell.Current.DisplayAlert(
                    "Reset Settings", 
                    "Are you sure you want to reset all settings to defaults? This cannot be undone.", 
                    "Reset", 
                    "Cancel");

                if (confirm)
                {
                    // Perform the reset operation
                    _settingsService.ResetToDefaults();
                    
                    // Notify UI of the change
                    OnPropertyChanged(nameof(Settings));
                    
                    await Shell.Current.DisplayAlert("Settings Reset", "All settings have been reset to defaults.", "OK");
                    
                    Logger?.LogInformation("Settings reset to defaults");
                }
            }
            catch (Exception ex)
            {
                // Log and display any errors
                Logger?.LogError(ex, "Error resetting settings");
                await Shell.Current.DisplayAlert("Error", "Could not reset settings. Please try again.", "OK");
            }
        }

        // Navigates back to the previous page
        private async Task GoBack()
        {
            try
            {
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                // Log and display any errors
                Logger?.LogError(ex, "Error navigating back");
                await Shell.Current.DisplayAlert("Error", "Could not navigate back. Please try again.", "OK");
            }
        }

        // Permanently deletes all notes and settings
        private async Task ClearAllData()
        {
            try
            {
                // First confirmation dialog
                var confirm = await Shell.Current.DisplayAlert(
                    "Clear All Data", 
                    "⚠️ WARNING: This will permanently delete ALL your notes and settings. This action cannot be undone!\n\nAre you absolutely sure?", 
                    "DELETE ALL", 
                    "Cancel");

                if (confirm)
                {
                    // Second, final confirmation
                    var doubleConfirm = await Shell.Current.DisplayAlert(
                        "Final Confirmation", 
                        "This is your last chance! All notes will be permanently lost.\n\nConfirm deletion?", 
                        "YES, DELETE EVERYTHING", 
                        "Cancel");

                    if (doubleConfirm)
                    {
                        await ExecuteAsync(async () =>
                        {
                            // Clear all application preferences
                            Preferences.Clear();
                            
                            // Get the path to the database file
                            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "notes.db");
                            if (File.Exists(dbPath))
                            {
                                // Delete the database file
                                File.Delete(dbPath);
                            }

                            // Delete any other related application data files
                            var appDataFiles = Directory.GetFiles(FileSystem.AppDataDirectory, "*.*");
                            foreach (var file in appDataFiles)
                            {
                                try
                                {
                                    File.Delete(file);
                                }
                                catch
                                {
                                    // Continue even if a file can't be deleted
                                }
                            }

                            await Shell.Current.DisplayAlert(
                                "Data Cleared", 
                                "All notes and settings have been deleted. The app will now restart.", 
                                "OK");

                            // Force the application to exit and restart
                            System.Environment.Exit(0);
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // Log and display any errors
                Logger?.LogError(ex, "Error clearing all data");
                await Shell.Current.DisplayAlert("Error", "Could not clear all data. Please try again.", "OK");
            }
        }
    }
}