using NoteApp.ViewModels;

namespace NoteApp.Views
{
    public partial class SettingsPage : ContentPage
    {
        private SettingsViewModel _viewModel;

        public SettingsPage(SettingsViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = viewModel;
            
            System.Diagnostics.Debug.WriteLine($"SettingsPage created with ViewModel: {viewModel != null}");
            System.Diagnostics.Debug.WriteLine($"Settings object: {viewModel?.Settings != null}");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.Initialize();
            
            System.Diagnostics.Debug.WriteLine("SettingsPage appearing");
            System.Diagnostics.Debug.WriteLine($"Dark mode setting: {_viewModel?.Settings?.IsDarkMode}");
        }
    }
}