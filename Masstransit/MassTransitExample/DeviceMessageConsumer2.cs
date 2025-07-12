using MassTransit;

namespace MassTransitExample;

public class DeviceMessageConsumer2 : IConsumer<DeviceMessage>
{
    public Task Consume(ConsumeContext<DeviceMessage> context)
    {
        Console.WriteLine($"Consumer2 received: {context.Message.Text}, RoutingKey: DeviceService2");
        return Task.CompletedTask;
    }
}