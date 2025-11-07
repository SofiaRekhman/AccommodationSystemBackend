using BLL.Models;
using DAL.Repositories;

namespace BLL.Services;

public class WeatherForecastService(IWeatherForecastRepository repository) : IWeatherForecastService
{
    private readonly IWeatherForecastRepository _repository = repository;

    public IEnumerable<WeatherForecastDto> GetForecast(int numberOfDays = 5)
    {
        return _repository
            .GetForecasts(numberOfDays)
            .Select(entity => new WeatherForecastDto(entity.Date, entity.TemperatureC, entity.Summary));
    }
}

