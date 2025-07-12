using MassTransit;
using Microsoft.Extensions.Hosting;

namespace MassTransitExample;

public class PublisherService(IPublishEndpoint publishEndpoint) : BackgroundService
{
    private readonly Random _random = new();

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var counter = 0;
        var toggle = false;

        while (!stoppingToken.IsCancellationRequested)
        {
            toggle = !toggle;
            var routingKey = toggle ? "DeviceService1" : "DeviceService2";
            var message = new DeviceMessage { Text = $"Message {++counter}" };

            await publishEndpoint.Publish(message, ctx =>
            {
                ctx.SetRoutingKey(routingKey);
            });

            Console.WriteLine($"Published: {message.Text}, RoutingKey: {routingKey}");

            var delay = _random.Next(1000, 3001);
            await Task.Delay(delay, stoppingToken);
        }
    }
}