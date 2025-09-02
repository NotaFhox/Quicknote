using NoteApp.ViewModels;

namespace NoteApp.Views
{
    public partial class NoteDetailPage : ContentPage
    {
        private NoteDetailViewModel _viewModel;

        public NoteDetailPage(NoteDetailViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadNote();
        }

        protected override bool OnBackButtonPressed()
        {
            
            if (_viewModel.HasUnsavedChanges)
            {
                
                _viewModel.BackCommand.Execute(null);
                return true; 
            }
            return base.OnBackButtonPressed();
        }
    }
}