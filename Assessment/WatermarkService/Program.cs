using RabbitMQ.Client;
using WatermarkService;
using WatermarkService.Services;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;

        services.AddSingleton<RabbitMQClientService>();
        services.AddSingleton(sp => new ConnectionFactory()
        {
            Uri = new Uri(configuration.GetConnectionString("RabbitMQ")) ,
            DispatchConsumersAsync = true
        });

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
