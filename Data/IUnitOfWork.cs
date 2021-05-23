using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingSystem.Data
{
    public interface IUnitOfWork
    {
        ITicketRepository Ticket { get; }
        int Complete();
    }
}
