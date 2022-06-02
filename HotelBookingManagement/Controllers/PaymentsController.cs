using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelBookingManagement.Data;
using HotelBookingManagement.Models;

namespace HotelBookingManagement.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Payments.Include(p => p.Booking).Include(p => p.Guest).Include(p => p.Hotel);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Booking)
                .Include(p => p.Guest)
                .Include(p => p.Hotel)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            ViewData["BookingID"] = new SelectList(_context.Bookings, "BookingId", "BookingId");
            ViewData["GuestId"] = new SelectList(_context.Guests, "GuestId", "GuestId");
            ViewData["HotelId"] = new SelectList(_context.Hotels, "HotelId", "HotelId");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentId,TotalCharge,CreditCardNumber,CardHolder,ExpiryDate,PaymentDate,HotelId,BookingID,GuestId")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookingID"] = new SelectList(_context.Bookings, "BookingId", "BookingId", payment.BookingID);
            ViewData["GuestId"] = new SelectList(_context.Guests, "GuestId", "GuestId", payment.GuestId);
            ViewData["HotelId"] = new SelectList(_context.Hotels, "HotelId", "HotelId", payment.HotelId);
            return View(payment);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["BookingID"] = new SelectList(_context.Bookings, "BookingId", "BookingId", payment.BookingID);
            ViewData["GuestId"] = new SelectList(_context.Guests, "GuestId", "GuestId", payment.GuestId);
            ViewData["HotelId"] = new SelectList(_context.Hotels, "HotelId", "HotelId", payment.HotelId);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentId,TotalCharge,CreditCardNumber,CardHolder,ExpiryDate,PaymentDate,HotelId,BookingID,GuestId")] Payment payment)
        {
            if (id != payment.PaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.PaymentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookingID"] = new SelectList(_context.Bookings, "BookingId", "BookingId", payment.BookingID);
            ViewData["GuestId"] = new SelectList(_context.Guests, "GuestId", "GuestId", payment.GuestId);
            ViewData["HotelId"] = new SelectList(_context.Hotels, "HotelId", "HotelId", payment.HotelId);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Booking)
                .Include(p => p.Guest)
                .Include(p => p.Hotel)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.PaymentId == id);
        }
    }
}
