using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Task2.Context;
using Task2.Dtos;
using Task2.Entities;
using Task2.Hubs;
using Microsoft.Extensions.Configuration;


namespace Task2.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly WeatherDbContext _dbContext;
        private readonly IHubContext<WeatherHub> _weatherHubContext;
        private readonly IConfiguration _configuration;

        public WeatherService(HttpClient httpClient, WeatherDbContext dbContext, IHubContext<WeatherHub> weatherHubContext, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _dbContext = dbContext;
            _weatherHubContext = weatherHubContext;
            _configuration = configuration;
        }

        public async Task FetchWeatherDataAsync()
        {
            var cities = new List<(string, string)> // country, city
            {
                ("USA", "New York"),
                ("USA", "Los Angeles"),
                ("Canada", "Toronto"),
                ("Canada", "Vancouver"),
                ("United Kingdom", "London"),
                ("United Kingdom", "Manchester"),
                ("France", "Paris"),
                ("France", "Marseille"),
                ("Germany", "Berlin"),
                ("Germany", "Munich")
            };

            foreach (var (country, city) in cities)
            {
                var apiKey = _configuration["OpenWeatherMap:ApiKey"];
                var url = $"https://api.openweathermap.org/data/2.5/weather?q={city},{country}&appid={apiKey}&units=metric";

                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var weatherData = JsonConvert.DeserializeObject<WeatherDataDto>(json);

                    var entity = new WeatherData
                    {
                        Country = country,
                        City = city,
                        Temperature = weatherData.Main.Temp,
                        WindSpeed = weatherData.Wind.Speed,
                        Clouds = weatherData.Clouds.All,
                        LastUpdate = DateTime.Now
                    };

                    _dbContext.WeatherData.Add(entity);
                }
            }

            await _dbContext.SaveChangesAsync();
            await _weatherHubContext.Clients.All.SendAsync("ReceivedWeatherUpdate", GetLatestWeatherData());
        }

        public WeatherData[] GetLatestWeatherData()
        {
            var data = _dbContext.WeatherData
                .GroupBy(w => w.City)
                .Select(g => g.OrderByDescending(w => w.LastUpdate).FirstOrDefault())
                .ToArray();

            return data;
        }

        public WeatherData[] GetHistoricalWeatherData(string city, string country)
        {
            //Last 2 hours data for the trend
            var currentTime = DateTime.Now;
            var startTime = currentTime.AddHours(-2);
            var historicalData = _dbContext.WeatherData
                .Where(data => data.City == city && data.Country == country && data.LastUpdate >= startTime)
                .ToArray();

            return historicalData;
        }
    }
}
