using Microsoft.AspNetCore.Mvc;

namespace Snippet.Micro.Operation.App.Controllers
{
    /// <summary>
    /// 健康检查
    /// </summary>
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
