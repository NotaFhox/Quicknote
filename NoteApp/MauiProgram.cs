using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NoteApp.Data;
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

        // Configure database
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "notes.db");
        builder.Services.AddDbContext<NoteDbContext>(options =>
            options.UseSqlite($"Data Source={dbPath}"));

        // Register services
        builder.Services.AddScoped<INoteService, DatabaseNoteService>();
        
        // Register view models
        builder.Services.AddTransient<NotesViewModel>();
        builder.Services.AddTransient<NoteDetailViewModel>();
        
        // Register views
        builder.Services.AddTransient<NotesPage>();
        builder.Services.AddTransient<NoteDetailPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        var app = builder.Build();

        // Initialize database
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<NoteDbContext>();
            context.Database.EnsureCreated();
        }

        return app;
    }
}