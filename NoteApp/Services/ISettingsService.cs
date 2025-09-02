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
        
        public AppSettings Settings => _settings;
        
        public event EventHandler<AppSettings>? SettingsChanged;

        public SettingsService()
        {
            _settings = new AppSettings();
            _settings.PropertyChanged += (s, e) => 
            {
                SaveSettings();
                ApplyTheme();
                SettingsChanged?.Invoke(this, _settings);
            };
        }

        public void LoadSettings()
        {
            _settings.LoadFromPreferences();
            ApplyTheme();
        }

        public void SaveSettings()
        {
            _settings.SaveToPreferences();
        }

        public void ResetToDefaults()
        {
            _settings.IsDarkMode = false;
            _settings.IsPerformanceMode = false;
            _settings.AutoSaveEnabled = true;
            _settings.AutoSaveInterval = 10;
            _settings.DefaultCategory = "General";
            _settings.ShowLineNumbers = false;
            _settings.FontSize = 12;
            _settings.FontFamily = "System Default";
            
            SaveSettings();
            ApplyTheme();
        }

        public void ApplyTheme()
        {
            try 
            {
                if (Application.Current?.Resources != null)
                {
                    var resources = Application.Current.Resources;
                    
                    if (_settings.IsDarkMode)
                    {
                        // Apply dark theme colors
                        resources["Primary"] = Color.FromArgb("#2D2D30");
                        resources["Secondary"] = Color.FromArgb("#3E3E42");
                        resources["WindowsBeige"] = Color.FromArgb("#1E1E1E");
                        resources["ButtonFace"] = Color.FromArgb("#37373D");
                        resources["ContentWhite"] = Color.FromArgb("#252526");
                        resources["PaperWhite"] = Color.FromArgb("#2D2D30");
                        resources["SearchBackground"] = Color.FromArgb("#3C3C3C");
                        resources["NoteContentBackground"] = Color.FromArgb("#1E1E1E");
                        resources["Black"] = Colors.White;
                        resources["White"] = Color.FromArgb("#1E1E1E");
                        resources["Gray100"] = Color.FromArgb("#37373D");
                        resources["Gray200"] = Color.FromArgb("#3E3E42");
                        resources["Gray300"] = Color.FromArgb("#505050");
                        resources["Gray400"] = Color.FromArgb("#6A6A6A");
                        resources["Gray500"] = Color.FromArgb("#808080");
                        resources["Gray600"] = Color.FromArgb("#B0B0B0");
                        resources["Gray700"] = Colors.LightGray;
                        resources["Gray800"] = Colors.Gainsboro;
                        resources["Gray900"] = Colors.WhiteSmoke;
                    }
                    else
                    {
                        // Apply light theme colors (restore defaults)
                        resources["Primary"] = Color.FromArgb("#E6DDD4");
                        resources["Secondary"] = Color.FromArgb("#F8F6F0");
                        resources["WindowsBeige"] = Color.FromArgb("#F5F3ED");
                        resources["ButtonFace"] = Color.FromArgb("#EFEAE0");
                        resources["ContentWhite"] = Color.FromArgb("#FEFCFA");
                        resources["PaperWhite"] = Color.FromArgb("#FFFEF9");
                        resources["SearchBackground"] = Color.FromArgb("#FEFEFC");
                        resources["NoteContentBackground"] = Color.FromArgb("#FFFFFE");
                        resources["Black"] = Colors.Black;
                        resources["White"] = Colors.White;
                        resources["Gray100"] = Color.FromArgb("#F7F5F0");
                        resources["Gray200"] = Color.FromArgb("#EFEBE4");
                        resources["Gray300"] = Color.FromArgb("#E0D8CC");
                        resources["Gray400"] = Color.FromArgb("#CFC5B8");
                        resources["Gray500"] = Color.FromArgb("#B8AC9C");
                        resources["Gray600"] = Color.FromArgb("#A0947F");
                        resources["Gray700"] = Color.FromArgb("#877B68");
                        resources["Gray800"] = Color.FromArgb("#6B5F4C");
                        resources["Gray900"] = Color.FromArgb("#4A3F32");
                    }

                    // Apply performance mode settings
                    ApplyPerformanceMode();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error applying theme: {ex.Message}");
            }
        }

        private void ApplyPerformanceMode()
        {
            try
            {
                if (Application.Current?.Resources != null)
                {
                    var resources = Application.Current.Resources;
                    
                    if (_settings.IsPerformanceMode)
                    {
                        // Disable animations and reduce visual effects
                        if (resources.ContainsKey("DefaultTransition"))
                            resources.Remove("DefaultTransition");
                        
                        // Reduce shadow effects
                        resources["ShadowOpacity"] = 0.1;
                        resources["AnimationDuration"] = 0;
                        
                        // Simplify gradients to solid colors
                        resources["UseGradients"] = false;
                    }
                    else
                    {
                        // Enable full visual effects
                        resources["ShadowOpacity"] = 0.3;
                        resources["AnimationDuration"] = 250;
                        resources["UseGradients"] = true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error applying performance mode: {ex.Message}");
            }
        }
    }
}