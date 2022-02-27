using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("REading all projects");
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok($"REading project: {id}");
        }

        [HttpGet]
        [Route("/api/projects/{pid}/tickets")]
        public IActionResult GetProjectTicket(int pid, [FromQuery] int tid)
        {
            if (tid == 0)
                return Ok($"Reading all tickets belong to project #{pid}");

            return Ok($"Reading project #{pid}, ticket #{tid}");
        }

        //[HttpGet]
        //[Route("/api/projects/{pid}/tickets")]
        //public IActionResult GetProjectTicket([FromQuery] Ticket ticket)
        //{
        //    if (ticket == null) return BadRequest("Ticket not provided");

        //    if (ticket.TicketId == 0)
        //        return Ok($"Reading all tickets belong to project #{ticket.ProjectId}");

        //    return Ok($"Reading project #{ticket.ProjectId}, ticket #{ticket.TicketId}, title #{ticket.Title}, description #{ticket.Description}");
        //}

        [HttpPost]
        public IActionResult Post()
        {
            return Ok("Creating a project");
        }

        [HttpPut]
        public IActionResult Put()
        {
            return Ok("Updating a project");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok($"Deleting project: {id}");
        }
    }
}
