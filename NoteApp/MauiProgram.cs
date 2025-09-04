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
        }, ServiceLifetime.Scoped);

       
        builder.Services.AddSingleton<ISettingsService, SettingsService>();
        builder.Services.AddScoped<INoteService, DatabaseNoteService>();
        
       
        builder.Services.AddTransient<NotesViewModel>();
        builder.Services.AddTransient<NoteDetailViewModel>();
        builder.Services.AddTransient<SettingsViewModel>();
        
        
        builder.Services.AddTransient<NotesPage>();
        builder.Services.AddTransient<NoteDetailPage>();
        builder.Services.AddTransient<SettingsPage>();

        
#if DEBUG
        builder.Logging.AddDebug();
        builder.Logging.SetMinimumLevel(LogLevel.Debug);
#else
        builder.Logging.SetMinimumLevel(LogLevel.Warning);
#endif

        var app = builder.Build();

        
        try
        {
           
            var settingsService = app.Services.GetRequiredService<ISettingsService>();
            settingsService.LoadSettings();
            
            System.Diagnostics.Debug.WriteLine("Settings service initialized successfully");
            System.Diagnostics.Debug.WriteLine($"Dark mode: {settingsService.Settings.IsDarkMode}");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Settings initialization error: {ex.Message}");
        }

        
        try
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<NoteDbContext>();
            
       
            context.Database.EnsureCreated();
            
            System.Diagnostics.Debug.WriteLine($"Database initialized successfully at {dbPath}");
           
            var noteService = scope.ServiceProvider.GetRequiredService<INoteService>();
            var isHealthy = noteService.IsHealthyAsync().GetAwaiter().GetResult();
            System.Diagnostics.Debug.WriteLine($"Database health check: {isHealthy}");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Database initialization error: {ex.Message}");
        }

        return app;
    }
}