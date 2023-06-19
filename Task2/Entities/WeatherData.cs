namespace Task2.Entities
{
    public class WeatherData
    {
        public int Id { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public double Temperature { get; set; }
        public double WindSpeed { get; set; }
        public double Clouds { get; set; }
        public DateTime LastUpdate { get; set; }
    }

}
