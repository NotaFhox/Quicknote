// ENHANCED ISettingsService.cs - Apply dark mode to ALL pages
using NoteApp.Models;

namespace NoteApp.Services
{
    public interface ISettingsService
    {
        AppSettings Settings { get; }
        event EventHandler<AppSettings>? SettingsChanged;
        void LoadSettings();
        void SaveSettings();
        void ResetToDefaults();
        void ApplyTheme();
    }

    public class SettingsService : ISettingsService
    {
        private readonly AppSettings _settings;
        private bool _isApplyingTheme = false; // Prevent recursion
        
        public AppSettings Settings => _settings;
        
        public event EventHandler<AppSettings>? SettingsChanged;

        public SettingsService()
        {
            _settings = new AppSettings();
            _settings.PropertyChanged += OnSettingsPropertyChanged;
            
            // Load settings immediately
            LoadSettings();
        }

        private async void OnSettingsPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                if (_isApplyingTheme) return; // Prevent recursion during theme application
                
                System.Diagnostics.Debug.WriteLine($"Settings property changed: {e.PropertyName}");
                
                SaveSettings();
                
                // Apply theme changes immediately on main thread
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    ApplyTheme();
                    SettingsChanged?.Invoke(this, _settings);
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error handling settings property change: {ex.Message}");
            }
        }

        public void LoadSettings()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Loading settings...");
                
                // Temporarily disable property changed events to prevent recursion
                _settings.PropertyChanged -= OnSettingsPropertyChanged;
                
                _settings.IsDarkMode = Preferences.Get(nameof(_settings.IsDarkMode), false);
                _settings.IsPerformanceMode = Preferences.Get(nameof(_settings.IsPerformanceMode), false);
                _settings.AutoSaveEnabled = Preferences.Get(nameof(_settings.AutoSaveEnabled), true);
                _settings.AutoSaveInterval = Preferences.Get(nameof(_settings.AutoSaveInterval), 10);
                _settings.DefaultCategory = Preferences.Get(nameof(_settings.DefaultCategory), "General");
                _settings.ShowLineNumbers = Preferences.Get(nameof(_settings.ShowLineNumbers), false);
                _settings.FontSize = Preferences.Get(nameof(_settings.FontSize), 12);
                _settings.FontFamily = Preferences.Get(nameof(_settings.FontFamily), "System Default");
                
                // Re-enable property changed events
                _settings.PropertyChanged += OnSettingsPropertyChanged;
                
                System.Diagnostics.Debug.WriteLine($"Loaded settings - Dark mode: {_settings.IsDarkMode}, Auto-save: {_settings.AutoSaveEnabled}");
                
                // Apply theme after loading
                MainThread.BeginInvokeOnMainThread(() => ApplyTheme());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading settings: {ex.Message}");
                // Re-enable events even if loading fails
                _settings.PropertyChanged += OnSettingsPropertyChanged;
                ResetToDefaults();
            }
        }

        public void SaveSettings()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"Saving settings - Dark mode: {_settings.IsDarkMode}");
                
                Preferences.Set(nameof(_settings.IsDarkMode), _settings.IsDarkMode);
                Preferences.Set(nameof(_settings.IsPerformanceMode), _settings.IsPerformanceMode);
                Preferences.Set(nameof(_settings.AutoSaveEnabled), _settings.AutoSaveEnabled);
                Preferences.Set(nameof(_settings.AutoSaveInterval), _settings.AutoSaveInterval);
                Preferences.Set(nameof(_settings.DefaultCategory), _settings.DefaultCategory);
                Preferences.Set(nameof(_settings.ShowLineNumbers), _settings.ShowLineNumbers);
                Preferences.Set(nameof(_settings.FontSize), _settings.FontSize);
                Preferences.Set(nameof(_settings.FontFamily), _settings.FontFamily);
                
                System.Diagnostics.Debug.WriteLine("Settings saved successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving settings: {ex.Message}");
            }
        }

        public void ResetToDefaults()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Resetting settings to defaults...");
                
                // Temporarily disable property changed events
                _settings.PropertyChanged -= OnSettingsPropertyChanged;
                
                _settings.IsDarkMode = false;
                _settings.IsPerformanceMode = false;
                _settings.AutoSaveEnabled = true;
                _settings.AutoSaveInterval = 10;
                _settings.DefaultCategory = "General";
                _settings.ShowLineNumbers = false;
                _settings.FontSize = 12;
                _settings.FontFamily = "System Default";
                
                // Re-enable property changed events
                _settings.PropertyChanged += OnSettingsPropertyChanged;
                
                SaveSettings();
                MainThread.BeginInvokeOnMainThread(() => ApplyTheme());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error resetting settings: {ex.Message}");
                // Re-enable events even if reset fails
                _settings.PropertyChanged += OnSettingsPropertyChanged;
            }
        }

        public void ApplyTheme()
        {
            try 
            {
                if (_isApplyingTheme) return; // Prevent recursion
                _isApplyingTheme = true;
                
                System.Diagnostics.Debug.WriteLine($"Applying theme globally - Dark mode: {_settings.IsDarkMode}");
                
                if (Application.Current?.Resources == null)
                {
                    System.Diagnostics.Debug.WriteLine("Application.Current.Resources is null - cannot apply theme");
                    return;
                }

                var resources = Application.Current.Resources;
                
                if (_settings.IsDarkMode)
                {
                    System.Diagnostics.Debug.WriteLine("Applying dark theme to all pages...");
                    ApplyDarkTheme(resources);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Applying light theme to all pages...");
                    ApplyLightTheme(resources);
                }

                // Apply performance mode settings
                ApplyPerformanceMode(resources);
                
                // Force comprehensive UI refresh across all pages
                ForceGlobalUIRefresh();
                
                System.Diagnostics.Debug.WriteLine("Theme applied successfully to all pages");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error applying theme: {ex.Message}");
            }
            finally
            {
                _isApplyingTheme = false;
            }
        }

        private void ApplyDarkTheme(ResourceDictionary resources)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Setting dark theme colors for all pages...");
                
                // COMPREHENSIVE DARK THEME - ALL COLORS
                
                // Primary theme colors
                SetResourceSafe(resources, "Primary", "#2D2D30");
                SetResourceSafe(resources, "PrimaryDark", "#1E1E1E");
                SetResourceSafe(resources, "PrimaryDarkText", "#FFFFFF");
                SetResourceSafe(resources, "Secondary", "#3E3E42");
                SetResourceSafe(resources, "SecondaryDarkText", "#B0B0B0");
                SetResourceSafe(resources, "Tertiary", "#37373D");
                
                // Background colors for ALL pages
                SetResourceSafe(resources, "WindowsBeige", "#1E1E1E");
                SetResourceSafe(resources, "ButtonFace", "#37373D");
                SetResourceSafe(resources, "ButtonShadow", "#000000");
                SetResourceSafe(resources, "ButtonHighlight", "#505050");
                SetResourceSafe(resources, "ControlDark", "#505050");
                SetResourceSafe(resources, "ControlLight", "#37373D");
                
                // Content colors for ALL pages
                SetResourceSafe(resources, "ContentWhite", "#252526");
                SetResourceSafe(resources, "PaperWhite", "#2D2D30");
                SetResourceSafe(resources, "OffWhite", "#252526");
                SetResourceSafe(resources, "WarmWhite", "#2D2D30");
                SetResourceSafe(resources, "SearchBackground", "#3C3C3C");
                SetResourceSafe(resources, "NoteContentBackground", "#1E1E1E");
                SetResourceSafe(resources, "CategoryBackground", "#37373D");
                SetResourceSafe(resources, "MetadataBackground", "#2D2D30");
                
                // Text colors for ALL pages
                SetResourceSafe(resources, "Black", "#FFFFFF");
                SetResourceSafe(resources, "White", "#1E1E1E");
                SetResourceSafe(resources, "BeigeText", "#B0B0B0");
                
                // Accent colors (keep these for contrast)
                SetResourceSafe(resources, "ClassicBlue", "#4A90E2");
                SetResourceSafe(resources, "ClassicRed", "#E74C3C");
                SetResourceSafe(resources, "AccentBlue", "#4A90E2");
                SetResourceSafe(resources, "AccentGreen", "#27AE60");
                SetResourceSafe(resources, "AccentBrown", "#D68910");
                
                // Complete gray scale for dark mode
                SetResourceSafe(resources, "Gray100", "#37373D");
                SetResourceSafe(resources, "Gray200", "#3E3E42");
                SetResourceSafe(resources, "Gray300", "#505050");
                SetResourceSafe(resources, "Gray400", "#6A6A6A");
                SetResourceSafe(resources, "Gray500", "#808080");
                SetResourceSafe(resources, "Gray600", "#B0B0B0");
                SetResourceSafe(resources, "Gray700", "#D0D0D0");
                SetResourceSafe(resources, "Gray800", "#E0E0E0");
                SetResourceSafe(resources, "Gray900", "#F0F0F0");
                SetResourceSafe(resources, "Gray950", "#FFFFFF");
                
                // Update brushes too
                UpdateBrushResources(resources);
                
                System.Diagnostics.Debug.WriteLine("Dark theme colors applied to all pages");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error applying dark theme: {ex.Message}");
            }
        }

        private void ApplyLightTheme(ResourceDictionary resources)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Setting light theme colors for all pages...");
                
                // COMPREHENSIVE LIGHT THEME - ALL COLORS
                
                // Restore original light theme colors
                SetResourceSafe(resources, "Primary", "#E6DDD4");
                SetResourceSafe(resources, "PrimaryDark", "#D2C7B8");
                SetResourceSafe(resources, "PrimaryDarkText", "#2F2F2F");
                SetResourceSafe(resources, "Secondary", "#F8F6F0");
                SetResourceSafe(resources, "SecondaryDarkText", "#8B7355");
                SetResourceSafe(resources, "Tertiary", "#EFE7DC");
                
                // Background colors for ALL pages
                SetResourceSafe(resources, "WindowsBeige", "#F5F3ED");
                SetResourceSafe(resources, "ButtonFace", "#EFEAE0");
                SetResourceSafe(resources, "ButtonShadow", "#C4B8A8");
                SetResourceSafe(resources, "ButtonHighlight", "#FEFCF8");
                SetResourceSafe(resources, "ControlDark", "#B8AC98");
                SetResourceSafe(resources, "ControlLight", "#FAF8F2");
                
                // Content colors for ALL pages
                SetResourceSafe(resources, "ContentWhite", "#FEFCFA");
                SetResourceSafe(resources, "PaperWhite", "#FFFEF9");
                SetResourceSafe(resources, "OffWhite", "#FEFEFE");
                SetResourceSafe(resources, "WarmWhite", "#FFF9F0");
                SetResourceSafe(resources, "SearchBackground", "#FEFEFC");
                SetResourceSafe(resources, "NoteContentBackground", "#FFFFFE");
                SetResourceSafe(resources, "CategoryBackground", "#F2EDE5");
                SetResourceSafe(resources, "MetadataBackground", "#EAE2D6");
                
                // Text colors for ALL pages
                SetResourceSafe(resources, "Black", "#000000");
                SetResourceSafe(resources, "White", "#FFFFFF");
                SetResourceSafe(resources, "BeigeText", "#6B5B47");
                
                // Accent colors (original)
                SetResourceSafe(resources, "ClassicBlue", "#2B4C85");
                SetResourceSafe(resources, "ClassicRed", "#A0504A");
                SetResourceSafe(resources, "AccentBlue", "#5A7BA8");
                SetResourceSafe(resources, "AccentGreen", "#6B8E5A");
                SetResourceSafe(resources, "AccentBrown", "#8B6914");
                
                // Complete gray scale for light mode
                SetResourceSafe(resources, "Gray100", "#F7F5F0");
                SetResourceSafe(resources, "Gray200", "#EFEBE4");
                SetResourceSafe(resources, "Gray300", "#E0D8CC");
                SetResourceSafe(resources, "Gray400", "#CFC5B8");
                SetResourceSafe(resources, "Gray500", "#B8AC9C");
                SetResourceSafe(resources, "Gray600", "#A0947F");
                SetResourceSafe(resources, "Gray700", "#877B68");
                SetResourceSafe(resources, "Gray800", "#6B5F4C");
                SetResourceSafe(resources, "Gray900", "#4A3F32");
                SetResourceSafe(resources, "Gray950", "#2E251C");
                
                // Update brushes too
                UpdateBrushResources(resources);
                
                System.Diagnostics.Debug.WriteLine("Light theme colors applied to all pages");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error applying light theme: {ex.Message}");
            }
        }

        private void UpdateBrushResources(ResourceDictionary resources)
        {
            try
            {
                // Update brush resources to match the color changes
                var brushesToUpdate = new[]
                {
                    "PrimaryBrush", "SecondaryBrush", "TertiaryBrush", "WhiteBrush", 
                    "ContentWhiteBrush", "PaperWhiteBrush", "BlackBrush", "WindowsBeigeBrush",
                    "ButtonFaceBrush", "Gray100Brush", "Gray200Brush", "Gray300Brush",
                    "Gray400Brush", "Gray500Brush", "Gray600Brush", "Gray700Brush",
                    "Gray800Brush", "Gray900Brush", "Gray950Brush", "CategoryBackgroundBrush",
                    "MetadataBackgroundBrush", "SearchBackgroundBrush", "NoteContentBackgroundBrush"
                };

                foreach (var brushKey in brushesToUpdate)
                {
                    var colorKey = brushKey.Replace("Brush", "");
                    if (resources.ContainsKey(colorKey))
                    {
                        var color = resources[colorKey];
                        if (color is Color colorValue)
                        {
                            var brush = new SolidColorBrush(colorValue);
                            if (resources.ContainsKey(brushKey))
                                resources[brushKey] = brush;
                            else
                                resources.Add(brushKey, brush);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating brush resources: {ex.Message}");
            }
        }

        private void SetResourceSafe(ResourceDictionary resources, string key, string colorValue)
        {
            try
            {
                var color = Color.FromArgb(colorValue);
                if (resources.ContainsKey(key))
                {
                    resources[key] = color;
                }
                else
                {
                    resources.Add(key, color);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error setting resource {key} to {colorValue}: {ex.Message}");
            }
        }

        private void ApplyPerformanceMode(ResourceDictionary resources)
        {
            try
            {
                if (_settings.IsPerformanceMode)
                {
                    if (resources.ContainsKey("ShadowOpacity"))
                        resources["ShadowOpacity"] = 0.1;
                    else
                        resources.Add("ShadowOpacity", 0.1);
                    
                    if (resources.ContainsKey("AnimationDuration"))
                        resources["AnimationDuration"] = 0;
                    else
                        resources.Add("AnimationDuration", 0);
                    
                    if (resources.ContainsKey("UseGradients"))
                        resources["UseGradients"] = false;
                    else
                        resources.Add("UseGradients", false);
                }
                else
                {
                    if (resources.ContainsKey("ShadowOpacity"))
                        resources["ShadowOpacity"] = 0.3;
                    else
                        resources.Add("ShadowOpacity", 0.3);
                    
                    if (resources.ContainsKey("AnimationDuration"))
                        resources["AnimationDuration"] = 250;
                    else
                        resources.Add("AnimationDuration", 250);
                    
                    if (resources.ContainsKey("UseGradients"))
                        resources["UseGradients"] = true;
                    else
                        resources.Add("UseGradients", true);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error applying performance mode: {ex.Message}");
            }
        }

        private void ForceGlobalUIRefresh()
        {
            try
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    try
                    {
                        // Method 1: Refresh the entire app
                        if (Application.Current?.MainPage != null)
                        {
                            var mainPage = Application.Current.MainPage;
                            
                            // Force layout refresh
                            mainPage.ForceLayout();
                            
                            // Trigger visual state updates
                            VisualStateManager.GoToState(mainPage, "Normal");
                            
                            // Refresh all child pages if this is a Shell
                            if (mainPage is Shell shell)
                            {
                                RefreshShellContent(shell);
                            }
                        }
                        
                        // Method 2: Trigger a resource dictionary change event
                        Application.Current?.Resources?.Clear();
                        
                        // Method 3: Force redraw by changing a global property
                        if (Application.Current != null)
                        {
                            var temp = Application.Current.RequestedTheme;
                            // Trigger a theme refresh by reapplying the theme
                            ApplyTheme();
                        }
                        
                        System.Diagnostics.Debug.WriteLine("Global UI refresh completed");
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error in global UI refresh: {ex.Message}");
                    }
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error forcing global UI refresh: {ex.Message}");
            }
        }

        private void RefreshShellContent(Shell shell)
        {
            try
            {
                // Refresh all shell content
                foreach (var item in shell.Items)
                {
                    if (item?.CurrentItem?.CurrentItem is ShellContent shellContent && shellContent.Content is ContentPage page)
                    {
                        page.ForceLayout();
                        VisualStateManager.GoToState(page, "Normal");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error refreshing shell content: {ex.Message}");
            }
        }
    }
}