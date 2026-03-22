using SistemTicket.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using SistemTicket.Models.Enums;
using System.Reflection;

namespace SistemTicket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private static readonly List<Ticket> _ticketList = new List<Ticket>
        {
            new Ticket
    {
        Id = 1,
        Title = "Error al iniciar sesión",
        Description = "El usuario no puede acceder con sus credenciales",
        Status = TicketStatus.Open,
        Priority = TicketPriority.High
    },

            new Ticket
    {
        Id = 2,
        Title = "Fallo en carga de página",
        Description = "La página principal tarda mucho en cargar",
        Status = TicketStatus.OnGoing,
        Priority = TicketPriority.Medium
    },

            new Ticket
    {
        Id = 3,
        Title = "Error en registro de usuario",
        Description = "No se guarda la información al registrarse",
        Status = TicketStatus.Open,
        Priority = TicketPriority.High
    },

            new Ticket
    {
        Id = 4,
        Title = "Actualización de perfil",
        Description = "El usuario no puede actualizar su información personal",
        Status = TicketStatus.Closed,
        Priority = TicketPriority.Low
    }
        };

        [HttpGet]
        public IActionResult GetTicket()
        {
            var ticket = new List<Ticket>
            {
                new Ticket {Id = 1, Title = "Programas ", Status = TicketStatus.Open, Priority = TicketPriority.Medium}
            };

            var Tickets = new List<Ticket>();
            Tickets.Add(new Ticket { Id = 1, Title = "Programas ", Description = "trabajo", Status = TicketStatus.Open, Priority = TicketPriority.Medium });
            Tickets.Add(new Ticket { Id = 2, Title = "Programas ", Description = "trabajo", Status = TicketStatus.Open, Priority = TicketPriority.Medium });

            return Ok(_ticketList);

        }

        [HttpPost]
        public ActionResult<Ticket> Create(Ticket ticket)
        {
            if (string.IsNullOrWhiteSpace(ticket.Title))
            {
                return BadRequest("Title is mandatory");
            }
            else if (string.IsNullOrWhiteSpace(ticket.Description))
            {
                return BadRequest("description is mandatory");
            }
            else if (ticket.Priority == null)
            {
                return BadRequest("Priority cant be null");
            }
            int id = _ticketList.Any() ? _ticketList.Max(x => x.Id) + 1 : 1;
            ticket.Id = id;
            ticket.Status = TicketStatus.Open;
            _ticketList.Add(ticket);

            return Ok(ticket);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Ticket ticket) 
        {
            var existing = _ticketList.FirstOrDefault(x => x.Id == id);
            if (existing == null)
            {
                return NotFound();
            }
            existing.Title = ticket.Title;
            existing.Description = ticket.Description;
            existing.Priority = ticket.Priority;
            return Ok(_ticketList);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _ticketList.FirstOrDefault(x => x.Id == id);
            if (existing == null)
            {
                return NotFound();
            }
            _ticketList.Remove(existing);
            return Ok(_ticketList);

        }
    }
}
