using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TicketingSystem.Models;

namespace TicketingSystem.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Ticket.Any())
                {
                    return;
                }
                context.Ticket.AddRange(
                    new TicketClass
                    {
                        DateOpened = DateTime.Now,
                        DateClosed = DateTime.Now,
                        Description = "Intial Seed",
                        Resolution = 1,
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
