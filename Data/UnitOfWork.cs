using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingSystem.Data
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly TicketContext _context;
        public ITicketRepository Tickets { get; private set; }
        public UnitOfWork(TicketContext context)
        {
            _context = context;
            Tickets = new EFTicketRepository(_context);
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
