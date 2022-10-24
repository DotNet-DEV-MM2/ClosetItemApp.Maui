using ClosetItemApp.Maui.ViewModels;

namespace ClosetItemApp.Maui;

public partial class MainPage : ContentPage
{
	public MainPage(ClosetItemListViewModel closetItemListViewModel)
	{
		InitializeComponent();
		BindingContext = closetItemListViewModel;
	}
}

