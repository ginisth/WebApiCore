using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Filters.V2;
using WebApi.QueryFilters;

namespace WebApi.Controllers
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/tickets")]
    public class TicketsV2Controller : ControllerBase
    {
        private readonly BugsContext db;

        public TicketsV2Controller(BugsContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] TicketQueryFilter ticketQueryFilter)
        {
            IQueryable<Ticket> tickets = db.Tickets;

            if (ticketQueryFilter != null)
            {
                if (ticketQueryFilter.Id.HasValue)
                    tickets = tickets.Where(x => x.TicketId == ticketQueryFilter.Id);
                if (!string.IsNullOrWhiteSpace(ticketQueryFilter.Title))
                    tickets = tickets.Where(x => x.Title.Contains(ticketQueryFilter.Title, StringComparison.OrdinalIgnoreCase));
                if (!string.IsNullOrWhiteSpace(ticketQueryFilter.Description))
                    tickets = tickets.Where(x => x.Description.Contains(ticketQueryFilter.Description, StringComparison.OrdinalIgnoreCase));
            }
            return Ok(await tickets.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var ticket = await db.Tickets.FindAsync(id);
            if (ticket == null)
                return NotFound();

            return Ok(ticket);
        }

        [HttpPost]
        [Ensure_TicketDescriptionPresentActionFilter]
        public async Task<ActionResult> Post([FromBody] Ticket ticket)
        {
            db.Tickets.Add(ticket);
            await db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = ticket.TicketId }, ticket);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Ticket ticket)
        {
            if (id != ticket.TicketId) return BadRequest();

            db.Entry(ticket).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch
            {
                if (await db.Tickets.FindAsync(id) == null)
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var ticket = await db.Tickets.FindAsync(id);
            if (ticket == null) return NotFound();

            db.Tickets.Remove(ticket);
            await db.SaveChangesAsync();

            return Ok(ticket);
        }
    }
}
