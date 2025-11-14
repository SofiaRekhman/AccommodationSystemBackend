using AccommodationSystemApi.MappingProfiles;
using BLL.Abstract;
using BLL.Services;
using DAL.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
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

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(AppDbContext)));
});


builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<ReservationProfile>();
});

builder.Services.AddScoped<IReservationService, ReservationService>();

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

app.MapControllers();

app.Run();
