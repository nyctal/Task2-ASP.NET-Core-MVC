using Microsoft.AspNetCore.Mvc;
using Task2.Services;
using Task2.Models;

namespace Task2.Controllers
{
    public class WeatherController : Controller
    {
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public IActionResult Weather()
        {
            var latestWeatherData = _weatherService.GetLatestWeatherData();

            var viewModel = new WeatherViewModel
            {
                LatestWeatherData = latestWeatherData
            };

            return View(viewModel);
        }

        [HttpGet("weather/historical")]
        public IActionResult GetHistoricalWeatherData(string city, string country)
        {
            // Fetch historical data from the last 2 hours for the city and country
            var historicalWeatherData = _weatherService.GetHistoricalWeatherData(city, country);

            return Ok(historicalWeatherData);
        }
    }
}
