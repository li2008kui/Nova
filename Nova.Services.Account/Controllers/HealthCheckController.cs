using Microsoft.AspNetCore.Mvc;

namespace Nova.Services.Account.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        public IActionResult Get()
        {
            return Ok("ok");
        }
    }
}