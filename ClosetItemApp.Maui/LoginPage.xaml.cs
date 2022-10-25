using ClosetItemApp.Maui.ViewModels;

namespace ClosetItemApp.Maui;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}