using FL.Services.Comments.API;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args)
            .Build()
            .Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                    .UseStartup<Startup>();

                webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                {
                    // Add configuration for Swagger in development environment
                    if (hostingContext.HostingEnvironment.IsDevelopment())
                    {
                        config.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
                    }
                });
            });
}