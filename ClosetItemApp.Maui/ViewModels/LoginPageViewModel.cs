using ClosetItemApp.Maui.Models;
using ClosetItemApp.Maui.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ClosetItemApp.Maui.ViewModels
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        public LoginPageViewModel(ClosetItemApiService closetItemApiService)
        {
            this.closetItemApiService = closetItemApiService;
        }

        [ObservableProperty]
        string username;

        [ObservableProperty]
        string password;
        private ClosetItemApiService closetItemApiService;

        [RelayCommand]
        async Task Login()
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayLoginMessage("Invalid Login Attempt");
            }
            else
            {
                // Call API to attempt a login
                var loginModel = new LoginModel(username, password);

                var response = await closetItemApiService.Login(loginModel);

                // display message
                await DisplayLoginMessage(closetItemApiService.StatusMessage);

                if (!string.IsNullOrEmpty(response.Token))
                {
                    // Store token in secure storage 
                    await SecureStorage.SetAsync("Token", response.Token);

                    // build a menu on the fly...based on the user role
                    var jsonToken = new JwtSecurityTokenHandler().ReadToken(response.Token) as JwtSecurityToken;

                    var role = jsonToken.Claims.FirstOrDefault(q => q.Type.Equals(ClaimTypes.Role))?.Value;

                    App.UserInfo = new UserInfo()
                    {
                        Username = Username,
                        Role = role
                    };


                    // navigate to app's main page
                   // MenuBuilder.BuildMenu();
                    await Shell.Current.GoToAsync($"{nameof(MainPage)}");

                }
                else
                {
                    await DisplayLoginMessage("Invalid Login Attempt");
                }
            }
        }

        async Task DisplayLoginMessage(string message)
        {
            await Shell.Current.DisplayAlert("Login Attempt Result", message, "OK");
            Password = string.Empty;
        }
    }
}
