using Microsoft.AspNetCore.Mvc;

namespace Snippet.Micro.RBAC.Controllers
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
