using Serilog;
using Serilog.Sinks.File;
using StockClient.Extensions;

var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .AddJsonFile($"appsettings.Development.json", optional: true, reloadOnChange: true)
                        .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Host.UseSerilog();
builder.Services.AddHttpClient("TwelveApi", client =>
{
    client.BaseAddress = new Uri("https://twelve-data1.p.rapidapi.com/");
    client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "be0aa279f7msh3b546c10c7a956ap1e30f0jsn733747cd1cd9");
    client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "twelve-data1.p.rapidapi.com");
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();
//builder.Services.AddHttpClientFactory();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
