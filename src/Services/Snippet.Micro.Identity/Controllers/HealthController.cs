using Microsoft.AspNetCore.Mvc;

namespace Snippet.Micro.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult AppHealth()
        {
            return Ok();
        }
    }
}
