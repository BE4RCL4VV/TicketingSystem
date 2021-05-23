using TicketingSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingSystem.Data
{
    public class EFTicketRepository : Repository<TicketClass>, ITicketRepository
    {
    
        public EFTicketRepository(ApplicationDbContext context) : base(context)
        {

        }

        public override TicketClass Get(int id)
        {
            return _context.Ticket
                .Where(p => p.TicketId == id)
                .Include(p => p.User)
                .SingleOrDefault();
        }

        public override IEnumerable<TicketClass> GetAll()
        {
            return _context.Ticket
                .Include(p => p.User)
                .ToList();
        }

        public IEnumerable<TicketClass> GetAllByUser(string UserId)
        {
            return _context.Ticket
                .Where(p => p.UserId == UserId)
                .Include(p => p.User)
                .ToList();
        }

        public IEnumerable<TicketClass> GetAllByUser(IdentityUser user)
        {
            return _context.Ticket.Where(p => p.UserId == user.Id)
                .Include(p => p.User)
                .ToList();
        }
    }

}

