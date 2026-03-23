using Microsoft.EntityFrameworkCore;

namespace TicketsSystem.Data
{
    public class TicketsSystemDBContext : DbContext
    {
        public TicketsSystemDBContext(DbContextOptions<TicketsSystemDBContext> options) : base(options)
        {

        }

        public DbSet<Entities.Ticket> Tickets { get; set; }
    }
}
