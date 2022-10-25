using ClosetItemApp.Maui.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace ClosetItemApp.Maui.Services
{

    public class ClosetItemApiService
    {
         HttpClient _httpClient;
         public static string BaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:8079" :
                "http://localhost:8079";
         public string StatusMessage;

        public ClosetItemApiService()
        {
            _httpClient = new() { BaseAddress = new Uri(BaseAddress)};
        }
        public async Task<List<ClosetItem>> GetClosetItems()
        {
            try
            {
                await SetAuthToken();
                var response = await _httpClient.GetStringAsync("/closetitems");
                return JsonConvert.DeserializeObject<List<ClosetItem>>(response);
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to retrieve data.";
            }

            return null;
        }

        public async Task<ClosetItem> GetClosetItem(int id)
        {
            try
            {
                var response = await _httpClient.GetStringAsync("/closetItems/" + id);
                return JsonConvert.DeserializeObject<ClosetItem>(response);
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to retrieve data.";
            }

            return null;
        }

        public async Task AddClosetItem(ClosetItem closetItem)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/closetItems/", closetItem);
                response.EnsureSuccessStatusCode();
                StatusMessage = "Insert Successful";
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to add data.";
            }
        }

        public async Task DeleteClosetItem(int id)
        {
            try
            {

                var response = await _httpClient.DeleteAsync("/closetItems/" + id);
                response.EnsureSuccessStatusCode();
                StatusMessage = "Delete Successful";
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to delete data.";
            }
        }

        public async Task UpdateClosetItem(int id, ClosetItem closetItem)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("/closetItems/" + id, closetItem);
                response.EnsureSuccessStatusCode();
                StatusMessage = "Update Successful";
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to update data.";
            }
        }

        public async Task<AuthResponseModel> Login(LoginModel loginModel)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/login", loginModel);
                response.EnsureSuccessStatusCode();
                StatusMessage = "Login Successful";

                return JsonConvert.DeserializeObject<AuthResponseModel>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to login successfully.";
                return new AuthResponseModel();
            }
        }

        public async Task SetAuthToken()
        {
            var token = await SecureStorage.GetAsync("Token");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }
}
