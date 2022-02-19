using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformDemo.Controllers
{
    [ApiController]
    public class TicketsController : ControllerBase
    {
        [HttpGet]
        [Route("api/tickets")]
        public IActionResult Get()
        {
            return Ok("REading all tickets");
        }

        [HttpGet]
        [Route("api/tickets/{id}")]
        public IActionResult Get(int id)
        {
            return Ok($"REading ticket: {id}");
        }

        [HttpPost]
        [Route("api/tickets")]
        public IActionResult Post()
        {
            return Ok("Creating a ticket");
        }

        [HttpPut]
        [Route("api/tickets")]
        public IActionResult Put()
        {
            return Ok("Updating a ticket");
        }

        [HttpDelete]
        [Route("api/tickets/{id}")]
        public IActionResult Delete(int id)
        {
            return Ok($"Deleting ticket: {id}");
        }
    }
}
