using ClosetItemApp.Maui.ViewModels;

namespace ClosetItemApp.Maui;

public partial class LoadingPage : ContentPage
{
	public LoadingPage(LoadingPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}