using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingSystem.Models
{
    public class TicketClass
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOpened { get; set; }
        public DateTime DateClosed { get; set; }
        public string Description { get; set; }
        public int Resolution { get; set; }

    }
}
