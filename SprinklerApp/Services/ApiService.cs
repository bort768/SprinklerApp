using Model;
using Model.Helpers;
using Newtonsoft.Json;
using System.Text;

namespace SprinklerApp.Services
{
    public class ApiService<T>  where T : IModel
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAddress;

        public ApiService(HttpClient httpClient, string apiAddress)
        {
            _httpClient = httpClient;
            _apiAddress = apiAddress;
        }

        public async Task<Result> SendDataAsync(T data)
        {
            var dataDto = data.ToDto();

            var json = JsonConvert.SerializeObject(dataDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            if (string.IsNullOrEmpty(_apiAddress))
                return Result.Failure(null, "API address is missing.");

            var response = await _httpClient.PostAsync(_apiAddress, content);

            if (response.IsSuccessStatusCode)
                return Result.Success(null);
            else
                return Result.Failure(null, "Failed to save data to the database.");
        }

        public async Task<Result> SendDataBatchAsync(IEnumerable<T> dataBatch)
        {
            var dataDto = dataBatch.Select(x => x.ToDto());
            var json = JsonConvert.SerializeObject(dataDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            if (string.IsNullOrEmpty(_apiAddress))
                return Result.Failure(null, "API address is missing.");

            var response = await _httpClient.PostAsync(_apiAddress, content);

            if (response.IsSuccessStatusCode)
                return Result.Success(null);
            else
                return Result.Failure(null, "Failed to save data to the database.");
        }
    }
}
