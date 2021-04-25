using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TicketingSystem.Models;

namespace TicketingSystem.Data
{
    public class EFTicketRepository : Repository<TicketClass>, ITicketRepository
    {
        public EFTicketRepository(TicketContext context) : base(context)
        {
        }
         
    }
}
