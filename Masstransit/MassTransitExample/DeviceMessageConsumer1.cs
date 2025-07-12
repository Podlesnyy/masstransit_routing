using MassTransit;

namespace MassTransitExample;

public class DeviceMessageConsumer1 : IConsumer<DeviceMessage>
{
    public Task Consume(ConsumeContext<DeviceMessage> context)
    {
        Console.WriteLine($"Consumer1 received: {context.Message.Text}, RoutingKey: DeviceService1");
        return Task.CompletedTask;
    }
}