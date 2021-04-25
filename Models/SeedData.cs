using System;
using System.Collections.Generic;
using System.Linq;
using TicketingSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TicketingSystem.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TicketContext(serviceProvider.GetRequiredService<DbContextOptions<TicketContext>>()))
            {
                if (context.Tickets.Any())
                {
                    return;
                }
                context.Tickets.AddRange(
                    new TicketClass
                    {
                        DateOpened = DateTime.Now,
                        DateClosed = DateTime.Now,
                        Description = "Intial Seed",
                        Resolution = 1
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
