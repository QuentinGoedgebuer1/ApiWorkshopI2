using Microsoft.AspNetCore.Mvc;
using WorkShopI2.Models.WeatherForecast;

namespace WorkShopI2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly AppDbContext _appDbContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        //[HttpGet(Name = "GetWeatherForecast")]
        //public List<WeatherForecast> Get()
        //{
        //    return _appDbContext.WeatherForecasts.ToList();
        //}

        //[HttpPost]
        //public async Task<IActionResult> CreateWeather([FromQuery] WeatherForecast weather)
        //{
        //    var test = new WeatherForecast()
        //    {
        //        TemperatureC = weather.TemperatureC,
        //        Summary = weather.Summary
        //    };
        //    _appDbContext.WeatherForecasts.Add(test);
        //    await _appDbContext.SaveChangesAsync();
        //    return Ok();
        //}
    }
}