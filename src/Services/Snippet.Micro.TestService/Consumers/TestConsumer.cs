using MassTransit;
using Microsoft.Extensions.Options;
using Snippet.Micro.MassTransit;
using Snippet.Micro.MassTransit.Messages;

namespace Snippet.Micro.TestService.Consumers
{
    public class TestConsumer : IConsumer<TestMessage>
    {
        private readonly MassTransitOption massTransitOption;

        public TestConsumer(IOptions<MassTransitOption> options)
        {
            massTransitOption = options.Value;
        }

        public async Task Consume(ConsumeContext<TestMessage> context)
        {
            await Task.Delay(5000);
            Console.WriteLine($"TestConsumer get a message,the id is{context.Message.TestId}");
        }
    }
}
