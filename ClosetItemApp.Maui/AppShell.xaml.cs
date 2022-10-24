using ClosetItemApp.Maui.Views;

namespace ClosetItemApp.Maui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(ClosetItemDetailsPage), typeof(ClosetItemDetailsPage));
    }
}
