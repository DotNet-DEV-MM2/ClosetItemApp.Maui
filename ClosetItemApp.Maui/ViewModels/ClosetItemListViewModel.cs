using ClosetItemApp.Maui.Models;
using ClosetItemApp.Maui.Services;
using ClosetItemApp.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;


namespace ClosetItemApp.Maui.ViewModels
{
    public partial class ClosetItemListViewModel : BaseViewModel
    {
        const string editButtonText = "Update ClosetItem";
        const string createButtonText = "Add ClosetItem";
        private readonly ClosetItemApiService closetItemApiService;
        NetworkAccess accessType = Connectivity.Current.NetworkAccess;
        string message = string.Empty;

        public ObservableCollection<ClosetItem> ClosetItems { get; private set; } = new();

        public ClosetItemListViewModel(ClosetItemApiService closetItemApiService)
        {
            Title = "ClosetItem List";
            AddEditButtonText = createButtonText;
            this.closetItemApiService = closetItemApiService;
        }

        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        string shortName;

        [ObservableProperty]
        string itemType;

        [ObservableProperty]
        string color;

        [ObservableProperty]
        string addEditButtonText;

        [ObservableProperty]
        int closetItemId;

        [RelayCommand]
        async Task GetClosetItemList()
        {
            if (IsLoading) return;
            try
            {
                IsLoading = true;
                if (ClosetItems.Any()) ClosetItems.Clear();
                var closetItems = new List<ClosetItem>();
                if (accessType == NetworkAccess.Internet)
                {
                    closetItems = await closetItemApiService.GetClosetItems();
                }
                else
                {
                    closetItems = App.ClosetItemDatabaseService.GetClosetItems();
                }
                foreach (var closetItem in closetItems) ClosetItems.Add(closetItem);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get closetItems: {ex.Message}");
                await ShowAlert("Failed to retrive list of closetItems.");
            }
            finally
            {
                IsLoading = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        async Task GetClosetItemDetails(int id)
        {
            if (id == 0) return;

            await Shell.Current.GoToAsync($"{nameof(ClosetItemDetailsPage)}?Id={id}", true);
        }

        [RelayCommand]
        async Task SaveClosetItem()
        {
            if (string.IsNullOrEmpty(ShortName) || string.IsNullOrEmpty(ItemType) || string.IsNullOrEmpty(Color))
            {
                await ShowAlert("Please insert valid data");
                return;
            }

            var closetItem = new ClosetItem
            {
                Id = ClosetItemId,
                ShortName = ShortName,
                ItemType = ItemType,
                Color = Color
            };

            if (ClosetItemId != 0)
            {
                if (accessType == NetworkAccess.Internet)
                {
                    await closetItemApiService.UpdateClosetItem(ClosetItemId, closetItem);
                    message = closetItemApiService.StatusMessage;
                }
                else
                {
                    App.ClosetItemDatabaseService.UpdateClosetItem(closetItem);
                    message = App.ClosetItemDatabaseService.StatusMessage;
                }
            }
            else
            {
                if (accessType == NetworkAccess.Internet)
                {
                    await closetItemApiService.AddClosetItem(closetItem);
                    message = closetItemApiService.StatusMessage;
                }
                else
                {
                    App.ClosetItemDatabaseService.AddClosetItem(closetItem);
                    message = App.ClosetItemDatabaseService.StatusMessage;
                }

            }
            await ShowAlert(message);
            await GetClosetItemList();
            await ClearForm();
        }

        [RelayCommand]
        async Task DeleteClosetItem(int id)
        {
            if (id == 0)
            {
                await ShowAlert("Please try again");
                return;
            }

            if (accessType == NetworkAccess.Internet)
            {
                await closetItemApiService.DeleteClosetItem(id);
                message = closetItemApiService.StatusMessage;
            }
            else
            {
                App.ClosetItemDatabaseService.DeleteClosetItem(id);
                message = App.ClosetItemDatabaseService.StatusMessage;
            }
            await ShowAlert(message);
            await GetClosetItemList();
        }

        [RelayCommand]
        async Task UpdateClosetItem(int id)
        {
            AddEditButtonText = editButtonText;
            return;
        }

        [RelayCommand]
        async Task SetEditMode(int id)
        {
            AddEditButtonText = editButtonText;
            ClosetItemId = id;
            ClosetItem closetItem;
            if (accessType == NetworkAccess.Internet)
            {
                closetItem = await closetItemApiService.GetClosetItem(ClosetItemId);
            }
            else
            {
                closetItem = App.ClosetItemDatabaseService.GetClosetItem(ClosetItemId);
            }

            ShortName = closetItem.ShortName;
            ItemType = closetItem.ItemType;
            Color = closetItem.Color;
        }

        [RelayCommand]
        async Task ClearForm()
        {
            AddEditButtonText = createButtonText;
            ClosetItemId = 0;
            ShortName = string.Empty;
            ItemType = string.Empty;
            Color = string.Empty;
        }

        private async Task ShowAlert(string message)
        {
            await Shell.Current.DisplayAlert("Info", message, "Ok");
        }
    }
}