using NoteApp.ViewModels;

namespace NoteApp.Views
{
    public partial class NotesPage : ContentPage
    {
        private NotesViewModel _viewModel;

        public NotesPage(NotesViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.OnAppearing();
        }
    }
}