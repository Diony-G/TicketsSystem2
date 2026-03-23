using SistemTicket.Models.Enums;
using System.ComponentModel.DataAnnotations;
namespace TicketsSystem.Data.Entities
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        [StringLength(150)]
        public string Title { get; set; }
        public string Description { get; set; }
        public TicketStatus Status { get; set; }
        public TicketPriority? Priority { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
