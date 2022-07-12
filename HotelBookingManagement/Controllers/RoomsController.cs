using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelBookingManagement.Data;
using HotelBookingManagement.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace HotelBookingManagement.Controllers
{
    public class RoomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rooms
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Rooms.Include(r => r.Hotel);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                //.Include(r => r.Guest)
                .Include(r => r.Hotel)
                .FirstOrDefaultAsync(m => m.RoomID == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            //ViewData["GuestId"] = new SelectList(_context.Guests, "GuestId", "GuestId");
            ViewData["HotelId"] = new SelectList(_context.Hotels, "Name", "Name");
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomID,RoomType,Description,Price,HotelId")] Room room, IFormFile Photo)
        {
            if (ModelState.IsValid)
            {
                //To upload a photo
                if (Photo != null)
                {
                    var fileName = UploadPhoto(Photo);
                    room.Photo = fileName; // set the unique file name before saving it to the daatase

                }

                _context.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["GuestId"] = new SelectList(_context.Guests, "GuestId", "GuestId", room.GuestId);
            ViewData["HotelId"] = new SelectList(_context.Hotels, "HotelId", "HotelId", room.HotelId);
            return View(room);
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            //ViewData["GuestId"] = new SelectList(_context.Guests, "GuestId", "GuestId", room.GuestId);
            ViewData["HotelId"] = new SelectList(_context.Hotels, "HotelId", "HotelId", room.HotelId);
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomID,RoomType,Description,Price,HotelId")] Room room, IFormFile Photo, string CurrentPhoto)
        {
            if (id != room.RoomID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //To upload a photo
                    if (Photo != null)
                    {
                        var fileName = UploadPhoto(Photo);
                        room.Photo = fileName; // set the unique file name before saving it to the daatase
                    }
                    else
                    {
                        //To keep the current photo if no new photo uploaded
                        room.Photo = CurrentPhoto;
                    }

                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.RoomID))
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
            //ViewData["GuestId"] = new SelectList(_context.Guests, "GuestId", "GuestId", room.GuestId);
            ViewData["HotelId"] = new SelectList(_context.Hotels, "HotelId", "HotelId", room.HotelId);
            return View(room);
        }

        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                //.Include(r => r.Guest)
                .Include(r => r.Hotel)
                .FirstOrDefaultAsync(m => m.RoomID == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.RoomID == id);
        }

        private static string UploadPhoto(IFormFile Photo)
        {
            //Get temporary location
            var filePath = Path.GetTempFileName();

            //GUID to get unique image name
            var fileName = Guid.NewGuid().ToString() + "-" + Photo.FileName;

            //Set destination folder dynamically so it works both locally and on the server
            var uploadPath = System.IO.Directory.GetCurrentDirectory() + "\\wwwroot\\img\\" + fileName;

            //copy the file
            using (var stream = new FileStream(uploadPath, FileMode.Create))
            {
                Photo.CopyTo(stream);
            }
            return fileName;
        }
    }
}
