using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Snippet.Micro.MassTransit.Messages;

namespace Snippet.Micro.TestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpPost("PublishTestMessage")]
        public async Task<IActionResult> PublishTestMessage([FromServices] IBus bus)
        {
            await bus.Publish(new TestMessage
            {
                TestId = new Random(DateTime.Now.Millisecond).Next(0, 999999)
            });

            return Ok();
        }
    }
}
