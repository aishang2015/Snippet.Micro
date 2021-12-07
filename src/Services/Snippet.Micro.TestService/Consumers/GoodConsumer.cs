using MassTransit;
using Microsoft.Extensions.Options;
using Snippet.Micro.MassTransit;
using Snippet.Micro.MassTransit.Messages;

namespace Snippet.Micro.TestService.Consumers
{
    public class GoodConsumer : IConsumer<TestMessage>
    {
        private readonly MassTransitOption massTransitOption;

        public GoodConsumer(IOptions<MassTransitOption> options)
        {
            massTransitOption = options.Value;
        }

        public Task Consume(ConsumeContext<TestMessage> context)
        {
            Console.WriteLine($"rabbit mq's host is {massTransitOption.Host}");
            Console.WriteLine($"GoodConsumer get a message,the id is{context.Message.TestId}");
            return Task.CompletedTask;
        }
    }
}
