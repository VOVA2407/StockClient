using Serilog;

namespace StockClient.Extensions;

public static class Extensions
{
    public static IServiceCollection AddLogger(IServiceCollection services)
    {

        var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .AddJsonFile($"appsettings.Development.json", optional: true, reloadOnChange: true)
                        .Build();

        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        return services;
    }

    public static IServiceCollection AddHttpClientFactory(IServiceCollection services)
    {
        services.AddHttpClient("TwelveApi", client =>
        {
            client.BaseAddress = new Uri("https://twelve-data1.p.rapidapi.com/");
            client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "be0aa279f7msh3b546c10c7a956ap1e30f0jsn733747cd1cd9");
            client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "twelve-data1.p.rapidapi.com");
        });
        return services;
    }

}
