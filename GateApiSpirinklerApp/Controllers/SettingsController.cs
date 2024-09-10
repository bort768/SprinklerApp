using Microsoft.AspNetCore.Mvc;

namespace GateApiSpirinklerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

    public SettingsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("UpdateWeatherApiKey")]
    public IActionResult UpdateWeatherApiKey([FromBody] string newApiKey)
    {
        var filePath = "appsettings.json";
        var json = System.IO.File.ReadAllText(filePath);
        dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
        jsonObj["WeatherApiKey"] = newApiKey;
        string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
        System.IO.File.WriteAllText(filePath, output);

        return Ok();
    }
    }
}
