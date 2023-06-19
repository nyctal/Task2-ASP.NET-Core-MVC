namespace Task2.Dtos
{
    public class WeatherDataDto
    {
        public MainDto? Main { get; set; }
        public WindDto? Wind { get; set; }
        public CloudsDto? Clouds { get; set; }
    }
    public class MainDto
    {
        public double Temp { get; set; }
    }

    public class WindDto
    {
        public double Speed { get; set; }
    }

    public class CloudsDto
    {
        public double All { get; set; }
    }
}
