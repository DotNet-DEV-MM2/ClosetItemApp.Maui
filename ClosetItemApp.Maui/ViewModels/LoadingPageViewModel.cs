using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClosetItemApp.Maui.ViewModels
{
    public partial class LoadingPageViewModel : BaseViewModel
    {

        public LoadingPageViewModel()
        {
            CheckUserLoginDetails();
        }

        private async void CheckUserLoginDetails()
        {
            // Retrieve token from internal storage
            var token = await SecureStorage.GetAsync("Token");

            if(string.IsNullOrEmpty(token))
            {
                await GoToLoginPage();
            }

            // Evaluate token and decide if valid


        }

        private async Task GoToLoginPage()
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        }
    }
}
