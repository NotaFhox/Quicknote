using NoteApp.Views;

namespace NoteApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		
		
		Routing.RegisterRoute(nameof(NoteDetailPage), typeof(NoteDetailPage));
		Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
	}
}