using ClosetItemApp.Maui.Services;

namespace ClosetItemApp.Maui;

public partial class App : Application
{
	public static ClosetItemDatabaseService ClosetItemDatabaseService { get; set; }
	public App(ClosetItemDatabaseService closetItemDatabaseService)
	{
		InitializeComponent();

		MainPage = new AppShell();
		ClosetItemDatabaseService = closetItemDatabaseService;
	}
}
