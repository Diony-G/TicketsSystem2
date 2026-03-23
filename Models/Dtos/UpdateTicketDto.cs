using SistemTicket.Models.Enums;

namespace TicketsSystem.Models.Dtos
{
    public class UpdateTicketDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }
    }
}
