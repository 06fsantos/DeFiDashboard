using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Serilog;
using DashboardUI.Data;
using Binance;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.development.json", optional: true)
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

try
{
    Log.Information("Application Starting Up");

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();
    builder.Services.AddSingleton<WeatherForecastService>(); // creates one instance for entire app
    builder.Services.AddHttpClient();
    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = configuration.GetConnectionString("Redis");
        options.InstanceName = "DeFiDashboard_"; 
    });

    builder.Services.AddTransient<IBinancePriceAPI, BinancePriceAPI>(); // creates new instance when requested

    builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseSerilogRequestLogging();

    app.UseRouting();

    app.MapBlazorHub();
    app.MapFallbackToPage("/_Host");

    app.Run();
}
catch(Exception ex)
{
    Log.Fatal("Fatal error occurred in the application");
}
finally
{
    Log.CloseAndFlush();
}
