using BLL.Services;
using DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.Use(async (context, next) =>
{
    if (context.Request.Headers.TryGetValue("Origin", out var origin))
    {
        app.Logger.LogInformation(
            "CORS request detected from {Origin} for {Method} {Path}",
            origin.ToString(),
            context.Request.Method,
            context.Request.Path);
    }

    await next();
});

app.MapGet("/weatherforecast", (IWeatherForecastService weatherForecastService) =>
    Results.Ok(weatherForecastService.GetForecast()))
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();
