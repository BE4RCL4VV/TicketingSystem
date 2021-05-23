using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using TicketingSystem.Models;

namespace TicketingSystem.Data
{
    public interface ITicketRepository : IRepository<TicketClass>
    {
        IEnumerable<TicketClass> GetAllByUser(string UserId);
        IEnumerable<TicketClass> GetAllByUser(IdentityUser user);

    }
}
