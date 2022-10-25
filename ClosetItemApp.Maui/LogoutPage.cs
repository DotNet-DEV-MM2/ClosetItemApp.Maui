using ClosetItemApp.Maui.ViewModels;

namespace ClosetItemApp.Maui;

public class LogoutPage : ContentPage
{
	public LogoutPage(LogoutViewModel logoutViewModel)
	{
		Content = new VerticalStackLayout
		{
			Children = {
				new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI!"
				}
			}
		};

		BindingContext = logoutViewModel;
	}
}