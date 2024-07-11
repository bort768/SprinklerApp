

namespace SprinklerApp.Helpers
{
public class ApiSettings
    {
        private static readonly Lazy<ApiSettings> instance = new Lazy<ApiSettings>(() => new ApiSettings());

        public static ApiSettings Instance => instance.Value;

        public string ApiAddress { get; private set; }
        public string WeatherApiAddress { get; private set; }

        private ApiSettings()
        {
            LoadApiAddressFromMemory();
            LoadWeatherApiAddressFromMemory();
        }

        private void LoadApiAddressFromMemory()
        {
            // Za�aduj adres API z pami�ci urz�dzenia
            ApiAddress = Preferences.Get("ApiAddress", string.Empty);
        }

        private void LoadWeatherApiAddressFromMemory()
        {
            // Za�aduj adres API pogodowego z pami�ci urz�dzenia
            WeatherApiAddress = Preferences.Get("WeatherApiAddress", string.Empty);
        }

        public void UpdateApiAddress(string newApiAddress)
        {
            ApiAddress = newApiAddress;
            // Zapisz nowy adres API do pami�ci urz�dzenia
            Preferences.Set("ApiAddress", newApiAddress);
        }

        public void UpdateWeatherApiAddress(string newWeatherApiAddress)
        {
            WeatherApiAddress = newWeatherApiAddress;
            // Zapisz nowy adres API pogodowego do pami�ci urz�dzenia
            Preferences.Set("WeatherApiAddress", newWeatherApiAddress);
        }
    }

}
