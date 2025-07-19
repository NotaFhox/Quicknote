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
    }
}