using MassTransit;
using MassTransitExample;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<DeviceMessageConsumer1>();
            x.AddConsumer<DeviceMessageConsumer2>();

            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host("localhost", 5672, "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.Publish<DeviceMessage>(p => 
                {
                    p.ExchangeType = ExchangeType.Direct;
                });
                
                cfg.ReceiveEndpoint("device_service1_queue", e =>
                {
                    e.ConfigureConsumeTopology = false;
                    e.ConfigureConsumer<DeviceMessageConsumer1>(ctx);
                    e.Bind<DeviceMessage>( s =>
                    {
                        s.RoutingKey = "DeviceService1";
                    });
                });

                cfg.ReceiveEndpoint("device_service2_queue", e =>
                {
                    e.ConfigureConsumeTopology = false;
                    e.ConfigureConsumer<DeviceMessageConsumer2>(ctx);
                    e.Bind<DeviceMessage>( s =>
                    {
                        s.RoutingKey = "DeviceService2";
                    });
                });
            });
        });

        services.AddHostedService<PublisherService>();
    });

await builder.RunConsoleAsync();