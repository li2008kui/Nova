using Microsoft.AspNetCore.Mvc;

namespace Nova.Services.Ticket.API.Controllers
{
    /// <summary>
    /// 健康检查控制器。
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        /// <summary>
        /// 获取服务健康状态。
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Get()
        {
            return Ok("ok");
        }
    }
}