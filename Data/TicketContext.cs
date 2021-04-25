using TicketingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace TicketingSystem.Data
{
    public class TicketContext : DbContext
    {
        public TicketContext(DbContextOptions<TicketContext> options) : base(options)
        {

        }

        public DbSet<TicketClass> Tickets { get; set; }
    }
}
