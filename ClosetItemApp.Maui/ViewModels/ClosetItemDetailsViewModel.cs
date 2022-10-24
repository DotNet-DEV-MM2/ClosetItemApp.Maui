using ClosetItemApp.Maui.Models;
using ClosetItemApp.Maui.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Web;

namespace ClosetItemApp.Maui.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public partial class ClosetItemDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly ClosetItemApiService closetItemApiService;

        public ClosetItemDetailsViewModel(ClosetItemApiService closetItemApiService)
        {
            this.closetItemApiService = closetItemApiService;
        }

        NetworkAccess accessType = Connectivity.Current.NetworkAccess;

        [ObservableProperty]
        ClosetItem closetItem;

        [ObservableProperty]
        int id;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Id = Convert.ToInt32(HttpUtility.UrlDecode(query["Id"].ToString()));
        }

        public async Task GetClosetItemData()
        {
            if (accessType == NetworkAccess.Internet)
            {
                ClosetItem = await closetItemApiService.GetClosetItem(Id);
            }
            else
            {
                ClosetItem = App.ClosetItemDatabaseService.GetClosetItem(Id);
            }
        }
    }
}