using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Nova.Services.Ticket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {
            return Ok("所有票都在这里。");
        }

        [Route("{ticketId:int}")]
        [HttpGet]
        public async Task<IActionResult> GetTicket(int ticketId)
        {
            return NotFound("不好意思，没票了。");
        }
    }
}