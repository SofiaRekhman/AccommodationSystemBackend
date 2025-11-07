using DAL.Entities;

namespace DAL.Repositories;

public interface IWeatherForecastRepository
{
    IEnumerable<WeatherForecast> GetForecasts(int count);
}

