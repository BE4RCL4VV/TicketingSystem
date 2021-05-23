using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketingSystem.Data;
using TicketingSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace TicketingSystem.Controllers
{
    public class TicketingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private IAuthorizationService _authService;
        private ITicketRepository _repo;

        public TicketingController(ApplicationDbContext context, UserManager<IdentityUser> usr, ITicketRepository repo, IUnitOfWork unitOfWork, IAuthorizationService auth)
        {
            _context = context;
            _repo = repo;
            _unitOfWork = unitOfWork;
            _userManager = usr;
            _authService = auth;
        }

        // GET: Ticketing
        public IActionResult Index()
        {
            return View(_repo.GetAllByUser(_userManager.GetUserId(User)));
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
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            TicketClass ticket = new TicketClass();
            ticket.User = user;
            ticket.UserId = user.Id;
            ticket.DateOpened = DateTime.Now;

            TempData["userid"] = user.Id;

            return View(ticket);
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
        public IActionResult Edit(int id, [Bind("TicketId,DateOpened,DateClosed,Description,Resolution,UserId")] TicketClass ticketClass)
        {
            if (id != ticketClass.TicketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var UpdatedTicket = _repo.Get(ticketClass.TicketId);

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
            return _repo.Get(id) == null;
            //return _context.Tickets.Any(e => e.TicketId == id);
        }
    }
}
