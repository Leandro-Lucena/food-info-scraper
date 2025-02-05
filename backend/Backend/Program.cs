using Backend.Data;
using Backend.Services;

class Program
{
    /// <summary>
    /// Starts the web application.
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    static async Task Main(string[] args)
    {
        // Configure the application host with dependency injection
        using IHost host = CreateHostBuilder(args).Build();

        // Get the DatabaseInitializer service from DI container
        var dbInitializer = host.Services.GetRequiredService<DatabaseInitializer>();

        // Initialize the database
        dbInitializer.Initialize();

        // Starts the server and waits for requests
        await host.RunAsync();
    }

    /// <summary>
    /// Configures the application host and services.
    /// </summary>
    /// <param name="args">Command Line Arguments</param>
    /// <returns>Host constructor</returns>
    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.Configure((context, app) =>
                {
                    app.UseCors(builder =>
                        builder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod());
                    app.UseRouting();
                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });
                });
            })
            .ConfigureServices((context, services) =>
            {
                string foodsUrl = context.Configuration["FoodsUrl"]
                    ?? throw new InvalidOperationException("FoodsUrl is missing in appsettings.json");
                string foodDetailUrl = context.Configuration["FoodDetailUrl"]
                    ?? throw new InvalidOperationException("FoodDetailUrl is missing in appsettings.json");
                string connectionString = context.Configuration["ConnectionString"]
                    ?? throw new InvalidOperationException("ConnectionString is missing in appsettings.json");

                services.AddSingleton(new Repository(connectionString));
                services.AddSingleton(provider =>
                {
                    var repository = provider.GetRequiredService<Repository>();
                    return new Scraper(foodsUrl, foodDetailUrl, repository);
                });

                services.AddSingleton(provider => new DatabaseInitializer(connectionString));
                services.AddControllers();

            });
}
