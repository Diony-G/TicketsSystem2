using Microsoft.AspNetCore.Mvc;
using SistemTicket.Models.Enums;
using System.Reflection;
using TicketsSystem.Data;
using TicketsSystem.Data.Entities;
using TicketsSystem.Models.Dtos;

namespace SistemTicket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly TicketsSystemDBContext _Context;
        private TicketController(TicketsSystemDBContext Context)
        {
            this._Context = Context;
        }
       

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var ticket = _Context.Tickets.FirstOrDefault(t => t.Id == id);

            if (ticket == null)
                return NotFound();

            var result = new TicketDto
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Status = ticket.Status.ToString(),
                Priority = ticket.Priority.ToString()
            };

            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetTicket()
        {
            var ticketsDto = _Context.Tickets.Select(t => new TicketDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status.ToString(),
                Priority = t.Priority.ToString()
            }).ToList();

            return Ok(ticketsDto);

        }

        [HttpPost]
        public ActionResult Create(CreateTicketDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                return BadRequest("Title is mandatory");
            }
            else if (string.IsNullOrWhiteSpace(request.Description))
            {
                return BadRequest("description is mandatory");
            }
            else if (request.Priority == null)
            {
                return BadRequest("Priority cant be null");
            }
            int id = _Context.Tickets.Any() ? _Context.Tickets.Max(x => x.Id) + 1 : 1;
            var ticket = new Ticket
            {
                Id = id,
                Title = request.Title,
                Description = request.Description,
                Priority = request.Priority.Value,
                Status = TicketStatus.Open
            };

            _Context.Tickets.Add(ticket);
            _Context.SaveChanges();

            return Ok(new { id = ticket.Id });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateTicketDto request) 
        {
            var existing = _Context.Tickets.FirstOrDefault(x => x.Id == id);
            if (existing == null)
            {
                return NotFound();
            }
            existing.Title = request.Title;
            existing.Description = request.Description;
            existing.Priority = request.Priority;
            existing.Status = request.Status;

            _Context.Update(existing);
            _Context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _Context.Tickets.FirstOrDefault(x => x.Id == id);
            if (existing == null)
            {
                return NotFound();
            }
            _Context.Tickets.Remove(existing);
            _Context.SaveChanges();
            return NoContent();

        }
    }
}
