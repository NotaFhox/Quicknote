using NoteApp.Services;
using NoteApp.ViewModels;
using NoteApp.Views;

namespace NoteApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // Register services
        builder.Services.AddSingleton<INoteService, MockNoteService>();
        
        // Register view models
        builder.Services.AddTransient<NotesViewModel>();
        builder.Services.AddTransient<NoteDetailViewModel>();
        
        // Register views
        builder.Services.AddTransient<NotesPage>();
        builder.Services.AddTransient<NoteDetailPage>();

        return builder.Build();
    }
}