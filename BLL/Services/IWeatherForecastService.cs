using BLL.Models;

namespace BLL.Services;

public interface IWeatherForecastService
{
    IEnumerable<WeatherForecastDto> GetForecast(int numberOfDays = 5);
}

