using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using Newtonsoft.Json;
using RestSharp;
using SprinklerApp.Helpers;

namespace SprinklerApp.ViewModels
{
    public partial class WeatherViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string city;
        private static string? _apiKey;

        [ObservableProperty]
        private WeatherRoot weather;

        [ObservableProperty]
        private LocationInfo location;

        [ObservableProperty]
        private double temp;

        private List<LocationInfo> locationInfoList;
        private WeatherRoot weatherInfoList;

        private const string MESSAGEFAILINFOAPI = "Klucz api nie został przypisany";

        public WeatherViewModel()
        {

        }

        async void Get_Location_Info_Rest()
        {
            var client = new RestClient($"http://api.openweathermap.org/geo/1.0");
            if (await GetApiKey() == false)
                return;

            //QueryString works like GetOrPost, except that it always appends the parameters to the url
            //in the form url?name1=value1&name2=value2, regardless of the request method.
            var request = new RestRequest("direct")
            .AddParameter("q", city)
            .AddParameter("limit", 5.ToString())
            .AddParameter("appid", _apiKey);
            var response = client.Get(request); //GetAsync should be here
            Console.WriteLine(response.Content);

            locationInfoList = JsonConvert.DeserializeObject<List<LocationInfo>>(response.Content.ToString());

            Location = locationInfoList.FirstOrDefault();
        }

        private static async Task<bool> GetApiKey()
        {
            _apiKey = ApiSettings.Instance.WeatherApiAddress;

            if (string.IsNullOrWhiteSpace(_apiKey))
            {
                await ToastSaveFail(MESSAGEFAILINFOAPI);
                return false;
            }
            return true;
        }

        [RelayCommand]
        async Task Get_Weather_InfoAsync2()
        {
            IsBusy = true;
            if (string.IsNullOrEmpty(City))
            {
                await ToastSaveSuccess("Nie podałeś miasta");
                IsBusy = false;
                return;
            }

            else
            {
                //what if we don't have connection to internet?
                try
                {
                    Get_Location_Info_Rest();
                }
                catch (Exception e)
                {
                    await ToastSaveSuccess(e.Message);
                }
            }

            if (Location == null)
            {
                await ToastSaveSuccess("Nie można pozyskać informacji o położeniu");
                IsBusy = false;
                return;
            }

            //var client = new RestClient($"https://api.openweathermap.org/data/3.0/onecall?lat={locationInfoList[0].lat}" +
            //                        $"&lon={locationInfoList[0].lon}&exclude=daily,minutely&units=&appid={_apiKey}");
            var client = new RestClient($"https://api.openweathermap.org/data/3.0/onecall");

            var request = new RestRequest();

            request.AddParameter("lat", locationInfoList.FirstOrDefault().lat);
            request.AddParameter("lon", locationInfoList.FirstOrDefault().lon);
            request.AddParameter("exclude", "daily,minutely");
            request.AddParameter("appid", _apiKey);
            var response = await client.GetAsync(request);

            weatherInfoList = JsonConvert.DeserializeObject<WeatherRoot>(response.Content.ToString());

            if (weatherInfoList == null)
            {
                IsBusy = false;
                return;
            }

            Weather = Convert_Temp_To_Cel(weatherInfoList);

            IsBusy = false;
        }

        private WeatherRoot Convert_Temp_To_Cel(WeatherRoot weather)
        {
            //convert to celcius
            weather.current.temp -= 273;
            foreach (var hourly in weather.hourly)
            {
                // convert hourly temp to celcius
                hourly.temp -= 273;
            }

            //Temp = weather.current.temp;

            return weather;
        }
    }
}
