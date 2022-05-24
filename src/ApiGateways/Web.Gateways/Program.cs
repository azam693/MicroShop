using Web.Gateway;

var builder = Host
    .CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment}");
    })
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
    });

builder.Build().Run();
