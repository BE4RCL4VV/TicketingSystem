using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketingSystem.Data;
using TicketingSystem.Models;

namespace TicketingSystem.Controllers
{
    public class TicketingController : Controller
    {
        private readonly TicketContext _context;
        private IUnitOfWork _unitOfWork;
        private ITicketRepository _repo;

        public TicketingController(TicketContext context, ITicketRepository repo, IUnitOfWork unitOfWork)
        {
            _context = context;
            _repo = repo;
            _unitOfWork = unitOfWork;
        }

        // GET: Ticketing
        public IActionResult Index()
        {
            return View(_repo.GetAll());
        }

        // GET: Ticketing/Details/5
        public IActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var ticketClass = _repo.Get(id);

            if (ticketClass == null)
            {
                return NotFound();
            }

            return View(ticketClass);
        }

        // GET: Ticketing/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ticketing/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,DateOpened,DateClosed,Description,Resolution")] TicketClass ticketClass)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(ticketClass);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(ticketClass);
        }

        // GET: Ticketing/Edit/5
        public IActionResult Edit(int id = 0)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var ticketClass = _repo.Get(id);
            if (ticketClass == null)
            {
                return NotFound();
            }
            return View(ticketClass);
        }

        // POST: Ticketing/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,DateOpened,DateClosed,Description,Resolution")] TicketClass ticketClass)
        {
            if (id != ticketClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var UpdatedTicket = _repo.Get(ticketClass.Id);

                UpdatedTicket.DateClosed = ticketClass.DateClosed;
                UpdatedTicket.DateOpened = ticketClass.DateOpened;
                UpdatedTicket.Description = ticketClass.Description;
                UpdatedTicket.Resolution = ticketClass.Resolution;

                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(ticketClass);
        }

        // GET: Ticketing/Delete/5
        public IActionResult Delete(int id = 0)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var ticketClass = _repo.Get(id);
            if (ticketClass == null)
            {
                return NotFound();
            }

            return View(ticketClass);
        }

        // POST: Ticketing/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var ticketClass = _repo.Get(id);
            _repo.Remove(ticketClass);
            _unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketClassExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
