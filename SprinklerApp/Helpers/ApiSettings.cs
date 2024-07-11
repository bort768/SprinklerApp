

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
            // Za³aduj adres API z pamiêci urz¹dzenia
            ApiAddress = Preferences.Get("ApiAddress", string.Empty);
        }

        private void LoadWeatherApiAddressFromMemory()
        {
            // Za³aduj adres API pogodowego z pamiêci urz¹dzenia
            WeatherApiAddress = Preferences.Get("WeatherApiAddress", string.Empty);
        }

        public void UpdateApiAddress(string newApiAddress)
        {
            ApiAddress = newApiAddress;
            // Zapisz nowy adres API do pamiêci urz¹dzenia
            Preferences.Set("ApiAddress", newApiAddress);
        }

        public void UpdateWeatherApiAddress(string newWeatherApiAddress)
        {
            WeatherApiAddress = newWeatherApiAddress;
            // Zapisz nowy adres API pogodowego do pamiêci urz¹dzenia
            Preferences.Set("WeatherApiAddress", newWeatherApiAddress);
        }
    }

}
