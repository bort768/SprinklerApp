using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using Newtonsoft.Json;
using SprinklerApp.Helpers;
using System.Text;

namespace SprinklerApp.ViewModels
{
    public partial class SettingsViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string apiAddress;

        [ObservableProperty]
        private string weatherApiAddress;

        public SettingsViewModel()
        {
            ApiAddress = ApiSettings.Instance.ApiAddress;
            WeatherApiAddress = ApiSettings.Instance.WeatherApiAddress; 
        }

        [RelayCommand]
        public async Task SaveApiAddressAsync()
        {
            if (string.IsNullOrWhiteSpace(ApiAddress))
            {
                await ToastSaveFail("API address cannot be empty.");
                return;
            }
            ApiSettings.Instance.UpdateApiAddress(ApiAddress);

            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(ApiAddress);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{ApiSettings.Instance.ApiAddress}SettingsController", content);

                if (response.IsSuccessStatusCode)
                {
                    await ToastSaveSuccess("Data saved successfully.");
                }
                else
                {
                    await ToastSaveFail("Failed to save data to the database.");
                }
            }
        }

        [RelayCommand]
        public async Task SaveWeatherApiAddressAsync() 
        {
            ApiSettings.Instance.UpdateWeatherApiAddress(WeatherApiAddress);

            if (string.IsNullOrWhiteSpace(ApiAddress))
            { 
                await ToastSaveFail("API address cannot be empty to save data in the database");
                return;
            }

            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(ApiAddress);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{ApiSettings.Instance.ApiAddress}SettingsController", content);

                if (response.IsSuccessStatusCode)
                {
                    await ToastSaveSuccess("Data saved successfully.");
                }
                else
                {
                    await ToastSaveFail("Failed to save data to the database.");
                }
            }
        }
    }
}
