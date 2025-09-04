using NoteApp.ViewModels;

namespace NoteApp.Views
{
    public partial class NotesPage : ContentPage
    {
        // |---------------------|
        // |                     |
        // |    Private Fields   |
        // |                     |
        // |---------------------|
        private NotesViewModel _viewModel;

        // |---------------------|
        // |                     |
        // |    Constructor      |
        // |                     |
        // |---------------------|
        public NotesPage(NotesViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = viewModel;
        }

        // |---------------------|
        // |                     |
        // |  Lifecycle Events   |
        // |                     |
        // |---------------------|
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
           
            _viewModel.ClearSearchCommand.Execute(null);
        }
    }
}