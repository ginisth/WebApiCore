using Microsoft.AspNetCore.Mvc;
using PlatformDemo.Filters;
using PlatformDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("REading all tickets");
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok($"REading ticket: {id}");
        }

        [HttpPost]
        public IActionResult PostV1([FromBody] Ticket ticket)
        {
            return Ok(ticket);
        }

        [HttpPost]
        [Route("/api/v2/tickets")]
        [Ticket_ValidateDatesActionFilter]
        public IActionResult PostV2([FromBody] Ticket ticket)
        {
            return Ok(ticket);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Ticket ticket)
        {
            return Ok(ticket);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok($"Deleting ticket: {id}");
        }
    }
}
