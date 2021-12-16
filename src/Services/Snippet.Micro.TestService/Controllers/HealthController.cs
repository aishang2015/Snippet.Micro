using Microsoft.AspNetCore.Mvc;

namespace Snippet.Micro.TestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult ServiceHealth()
        {
            return Ok();
        }
    }
}
