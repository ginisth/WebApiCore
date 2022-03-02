using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly BugsContext db;

        public ProjectsController(BugsContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(db.Projects.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var project = db.Projects.Find(id);
            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [HttpGet]
        [Route("/api/projects/{pid}/tickets")]
        public IActionResult GetProjectTickets(int pid)
        {
            var tickets = db.Tickets.Where(x => x.ProjectId == pid);
            if (tickets == null || tickets.Count() == 0)
                return NotFound();

            return Ok(tickets);
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
