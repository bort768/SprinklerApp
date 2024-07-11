namespace GateApiSpirinklerApp.Services
{
    public class WeatherCheckService : BackgroundService
    {
        private readonly ILogger<WeatherCheckService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public WeatherCheckService(IConfiguration configuration, ILogger<WeatherCheckService> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await CheckWeatherAsync();
                await Task.Delay(TimeSpan.FromHours(3), stoppingToken);
            }
        }

        private async Task CheckWeatherAsync()
        {
            // Przykład wywołania API pogodowego (należy dostosować)
            var apiKey = _configuration.GetValue<string>("WeatherApiKey");
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("http://api.openweathermap.org/data/2.5/weather?q=TwojeMiasto&appid=TwojKluczAPI");
            if (response.IsSuccessStatusCode)
            {
                var weatherData = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"Dane pogodowe: {weatherData}");

                // Tutaj dodaj logikę zapisu do bazy danych i decyzji o podlaniu
            }
            else
            {
                _logger.LogError("Nie udało się pobrać danych pogodowych.");
            }
        }
    }
}
