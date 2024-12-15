using Model;
using Model.Dto;
using Newtonsoft.Json;
using SprinklerApp.ViewModels;
using Model.Helpers;

namespace SprinklerApp.Services
{
    public class SprinklerApiService(HttpClient httpClient, string apiAddress)
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly string _apiAddress = apiAddress;

        public async Task<Result> GetDataAsync()
        {
            HttpResponseMessage? response = new();
            try
            {
                response = await _httpClient.GetAsync(_apiAddress);
            }
            catch (Exception e)
            {
                return Result.Failure(null, $"Something went wrong: {e.Message}");
            }
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(json))
                    return Result.Failure(null, "Json is empty");
                var sprinklersDto = JsonConvert.DeserializeObject<IEnumerable<SprinklerDto>>(json);
                if (sprinklersDto is null)
                    return Result.Failure(null, "De serialization went wrong");
                var sprinklers = sprinklersDto.Select(s => new SprinklerDisplayModel((Sprinkler)s.ToModel()));
                return Result.Success(sprinklers);
            }
            else
                return Result.Failure(null, "Failed to get data from the database.");
        }
    }
}
