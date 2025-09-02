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
        
        // Register DbContext with proper lifetime management
        builder.Services.AddDbContext<NoteDbContext>(options =>
        {
            options.UseSqlite($"Data Source={dbPath}");
           
#if DEBUG
            options.EnableSensitiveDataLogging();
            options.EnableDetailedErrors();
#endif
        }, ServiceLifetime.Scoped); // Use Scoped instead of Singleton

        // Register services in correct order
        builder.Services.AddSingleton<ISettingsService, SettingsService>();
        builder.Services.AddScoped<INoteService, DatabaseNoteService>();
        
        // Register ViewModels - they will get the services injected
        builder.Services.AddTransient<NotesViewModel>();
        builder.Services.AddTransient<NoteDetailViewModel>();
        builder.Services.AddTransient<SettingsViewModel>();
        
        // Register Views
        builder.Services.AddTransient<NotesPage>();
        builder.Services.AddTransient<NoteDetailPage>();
        builder.Services.AddTransient<SettingsPage>();

        // Configure logging
#if DEBUG
        builder.Logging.AddDebug();
        builder.Logging.SetMinimumLevel(LogLevel.Debug);
#else
        builder.Logging.SetMinimumLevel(LogLevel.Warning);
#endif

        var app = builder.Build();

        // Initialize settings service first
        try
        {
            using var scope = app.Services.CreateScope();
            var settingsService = scope.ServiceProvider.GetRequiredService<ISettingsService>();
            settingsService.LoadSettings();
            
            var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("NoteApp.Startup");
            logger.LogInformation("Settings service initialized successfully");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Settings initialization error: {ex.Message}");
        }

        // Initialize database in a scope that gets disposed properly
        try
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<NoteDbContext>();
            
            // Ensure database is created
            context.Database.EnsureCreated();
            
            var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("NoteApp.Startup");
            logger.LogInformation("Database initialized successfully at {DbPath}", dbPath);
            
            // Test database connectivity
            var noteService = scope.ServiceProvider.GetRequiredService<INoteService>();
            var isHealthy = noteService.IsHealthyAsync().GetAwaiter().GetResult();
            logger.LogInformation("Database health check: {IsHealthy}", isHealthy);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Database initialization error: {ex.Message}");
            // Log but don't crash the app
        }

        return app;
    }
}