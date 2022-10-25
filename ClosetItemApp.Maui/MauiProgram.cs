using ClosetItemApp.Maui.Services;
using ClosetItemApp.Maui.ViewModels;
using ClosetItemApp.Maui.Views;

namespace ClosetItemApp.Maui;

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

		string dbPath = Path.Combine(FileSystem.AppDataDirectory, "closetitems.db3");


		builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<ClosetItemDatabaseService>(s, dbPath));

		builder.Services.AddTransient<ClosetItemApiService>();

		builder.Services.AddSingleton<ClosetItemListViewModel>();
        builder.Services.AddSingleton<LoadingPageViewModel>();
        builder.Services.AddSingleton<LoginPageViewModel>();
        builder.Services.AddTransient<ClosetItemDetailsViewModel>();

		builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<LoadingPage>();
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddTransient<ClosetItemDetailsPage>();

		return builder.Build();
	}
}
