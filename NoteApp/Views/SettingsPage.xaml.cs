using NoteApp.ViewModels;

namespace NoteApp.Views
{
    public partial class SettingsPage : ContentPage
    {
        private readonly SettingsViewModel _viewModel;

        public SettingsPage(SettingsViewModel viewModel)
        {
            InitializeComponent();
            
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            BindingContext = _viewModel;
            
            System.Diagnostics.Debug.WriteLine($"SettingsPage created with ViewModel: {viewModel != null}");
            System.Diagnostics.Debug.WriteLine($"Settings object: {viewModel?.Settings != null}");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            try
            {
                _viewModel.Initialize();
                
                System.Diagnostics.Debug.WriteLine("SettingsPage appearing");
                System.Diagnostics.Debug.WriteLine($"Dark mode setting: {_viewModel?.Settings?.IsDarkMode}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in SettingsPage.OnAppearing: {ex.Message}");
            }
        }
    }
}