using NoteApp.Views;

namespace NoteApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		
		// Register routes
		Routing.RegisterRoute(nameof(NoteDetailPage), typeof(NoteDetailPage));
	}
}