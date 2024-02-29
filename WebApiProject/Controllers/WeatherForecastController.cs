using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApiProject.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        public class LoggingEvents
        {
            public const int CreateAction = 1000;
            public const int EditAction = 1001;
            public const int GetAction = 1002;
            public const int DeleteAction = 1003;
        }

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {            
            _logger.LogDebug(LoggingEvents.CreateAction, "Log Debug");
            _logger.LogInformation(LoggingEvents.EditAction, "Log Information");
            _logger.LogWarning(LoggingEvents.GetAction, "Log Warning");
            _logger.LogError(LoggingEvents.DeleteAction, "Log Error");
            _logger.LogCritical("Log Critical");
            var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Index = index,
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray();

            foreach (var forecast in forecasts)
            {
                Console.WriteLine($"序號:{forecast.Index}，日期：{forecast.Date}，溫度：{forecast.TemperatureC}°C，摘要：{forecast.Summary}");
            }

            return forecasts;
        }
    }
}