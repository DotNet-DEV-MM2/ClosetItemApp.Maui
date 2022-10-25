using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClosetItemApp.Maui.ViewModels
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        public LoginPageViewModel()
        {

        }

        [ObservableProperty]
        string username;

        [ObservableProperty]
        string password;

        [RelayCommand]
        async Task Login()
        {
            throw new NotImplementedException();
        }
    }

}
