using Task2.Context;
using Task2.Hubs;
using Task2.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<WeatherService>();
builder.Services.AddDbContext<WeatherDbContext>();
builder.Services.AddHttpClient();
builder.Services.AddSignalR();

var app = builder.Build();

// Esegui FetchWeatherDataAsync all'avvio dell'app
using (var scope = app.Services.CreateScope())
{
    var weatherService = scope.ServiceProvider.GetRequiredService<WeatherService>();
    await weatherService.FetchWeatherDataAsync();
}

// Avvia il timer per richiamare FetchWeatherDataAsync periodicamente
var timer = new System.Timers.Timer(60000); // 1 minuto
timer.Elapsed += async (sender, args) =>
{
    using (var scope = app.Services.CreateScope())
    {
        var weatherService = scope.ServiceProvider.GetRequiredService<WeatherService>();
        await weatherService.FetchWeatherDataAsync();
    }
};
timer.AutoReset = true;
timer.Start();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<WeatherHub>("/weatherHub");
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Weather}/{action=Weather}/{id?}");
});

app.Run();
