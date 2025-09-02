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
            _settingsService = settingsService;
            Title = "Settings";

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
        }

        private async Task ResetSettings()
        {
            var confirm = await Shell.Current.DisplayAlert(
                "Reset Settings", 
                "Are you sure you want to reset all settings to defaults? This cannot be undone.", 
                "Reset", 
                "Cancel");

            if (confirm)
            {
                _settingsService.ResetToDefaults();
                await Shell.Current.DisplayAlert("Settings Reset", "All settings have been reset to defaults.", "OK");
            }
        }

        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async Task ClearAllData()
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
                    try
                    {
                        IsBusy = true;
                        
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
                    }
                    catch (Exception ex)
                    {
                        Logger?.LogError(ex, "Error clearing data");
                        await Shell.Current.DisplayAlert("Error", "Could not clear all data. Please try again.", "OK");
                    }
                    finally
                    {
                        IsBusy = false;
                    }
                }
            }
        }

        public void Initialize()
        {
            System.Diagnostics.Debug.WriteLine("SettingsViewModel Initialize called");
            System.Diagnostics.Debug.WriteLine($"Settings service: {_settingsService != null}");
            System.Diagnostics.Debug.WriteLine($"Settings object: {_settingsService?.Settings != null}");
            System.Diagnostics.Debug.WriteLine($"Dark mode: {_settingsService?.Settings?.IsDarkMode}");
            
            OnPropertyChanged(nameof(Settings));
        }
    }
}