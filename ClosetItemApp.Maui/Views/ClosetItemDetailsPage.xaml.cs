using ClosetItemApp.Maui.ViewModels;

namespace ClosetItemApp.Maui.Views;

public partial class ClosetItemDetailsPage : ContentPage
{
    private readonly ClosetItemDetailsViewModel closetItemDetailsViewModel;
    public ClosetItemDetailsPage(ClosetItemDetailsViewModel closetItemDetailsViewModel)
	{
		InitializeComponent();
		BindingContext = closetItemDetailsViewModel;
        this.closetItemDetailsViewModel = closetItemDetailsViewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await closetItemDetailsViewModel.GetClosetItemData();
    }
}