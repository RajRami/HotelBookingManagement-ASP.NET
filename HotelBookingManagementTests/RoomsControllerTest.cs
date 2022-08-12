using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore; // for the in memory db
using HotelBookingManagement.Data;
using HotelBookingManagement.Controllers;
using HotelBookingManagement.Models;

namespace GymzillaTests
{
    [TestClass]
    public class ProductsControllerTest
    {
        // When a test uses a database
        // we need to 'Mock' this data
        // use in-memory databases for testing
        private ApplicationDbContext _context;
        private RoomsController _controller;
        private List<Room> _products = new List<Room>();


        [TestInitialize]
        public void TestInitialize()
        {
            // instantiate in-memory db context
            // similar to registering your db in startup.cs
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                .Options;
            _context = new ApplicationDbContext(options);

            // mock some data
            // Hotel
            var hotel = new Hotel { HotelId = 1, Name = "Sky View" };
            _context.Hotels.Add(hotel);
            _context.SaveChanges();

            // list of products
            var room1 = new Room { RoomID = 1, RoomType = "Single", Hotel = hotel };
            var room2 = new Room { RoomID = 2, RoomType = "Bicyle", Hotel = hotel };
            var room3 = new Room { RoomID = 3, RoomType = "Treadmill", Hotel = hotel };

            // add products to mock dbs
            _context.Rooms.Add(room1);
            _context.Rooms.Add(room2);
            _context.Rooms.Add(room3);
            _context.SaveChanges();

            // add products to local list
            _products.Add(room1);
            _products.Add(room2);
            _products.Add(room3);

            // instantiate the controller object with mock db context
            _controller = new RoomsController(_context);
        }

    }
}