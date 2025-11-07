namespace DAL.Entities;

public class WeatherForecast
{
    public WeatherForecast(DateOnly date, int temperatureC, string summary)
    {
        Date = date;
        TemperatureC = temperatureC;
        Summary = summary;
    }

    public DateOnly Date { get; }

    public int TemperatureC { get; }

    public string Summary { get; }
}

