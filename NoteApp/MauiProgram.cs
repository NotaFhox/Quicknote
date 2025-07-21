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
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

     
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "notes.db");
        
        builder.Services.AddDbContext<NoteDbContext>(options =>
        {
            options.UseSqlite($"Data Source={dbPath}");
           
#if DEBUG
            options.EnableSensitiveDataLogging();
            options.EnableDetailedErrors();
#endif
        });

       
        builder.Services.AddScoped<INoteService, DatabaseNoteService>();
        
       
        builder.Services.AddTransient<NotesViewModel>();
        builder.Services.AddTransient<NoteDetailViewModel>();
        
        
        builder.Services.AddTransient<NotesPage>();
        builder.Services.AddTransient<NoteDetailPage>();

        
#if DEBUG
        builder.Logging.AddDebug();
        builder.Logging.SetMinimumLevel(LogLevel.Debug);
#else
        builder.Logging.SetMinimumLevel(LogLevel.Warning);
#endif

        var app = builder.Build();

      
        try
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<NoteDbContext>();
            
           
            context.Database.EnsureCreated();
            
            
            var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("NoteApp.Startup");
            logger.LogInformation("Database initialized successfully at {DbPath}", dbPath);
        }
        catch (Exception ex)
        {
            
            System.Diagnostics.Debug.WriteLine($"Database initialization error: {ex.Message}");
            
            
        }

        return app;
    }
}