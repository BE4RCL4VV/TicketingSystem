using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem.Data;
using TicketingSystem.Models;

namespace TicketingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _ctx;
        private readonly IUnitOfWork _unitOfWork;

        private ITicketRepository _repo;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext applicationDbContext, ITicketRepository repository, IUnitOfWork uow)
        {
            _logger = logger;
            _ctx = applicationDbContext;
            _repo = repository;
            _unitOfWork = uow;
        }

        public IActionResult Index()
        {
            IEnumerable<TicketClass> tickets = _repo.GetAll();

            return View(tickets);
        }

        public IActionResult Privacy(int id)
        {
            TicketClass ticket = _repo.Get(id);

            return View(ticket);
        }

        public IActionResult Ticket()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
