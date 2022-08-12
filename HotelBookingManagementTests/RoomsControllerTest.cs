using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore; // for the in memory db
using HotelBookingManagement.Data;
using HotelBookingManagement.Controllers;
using HotelBookingManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingManagementTests
{
    [TestClass]
    public class RoomsControllerTest
    {
        // When a test uses a database
        // we need to 'Mock' this data
        // use in-memory databases for testing
        private ApplicationDbContext _context;
        private RoomsController _controller;
        private List<Room> _rooms = new List<Room>();

        //Arrange data
        [TestInitialize]
        public void TestInitialize()
        {
            // instantiate in-memory db context
            // similar to registering db in startup.cs
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                .Options;
            _context = new ApplicationDbContext(options);

            // mock some data
            // Hotel
            var hotel = new Hotel { HotelId = 1, Name = "Sky View" };
            _context.Hotels.Add(hotel);
            _context.SaveChanges();

            // list of rooms
            var room1 = new Room { RoomID = 1, RoomType = "Single", Hotel = hotel };
            var room2 = new Room { RoomID = 2, RoomType = "Double", Hotel = hotel };
            var room3 = new Room { RoomID = 3, RoomType = "Tripal", Hotel = hotel };

            // add rooms to mock dbs
            _context.Rooms.Add(room1);
            _context.Rooms.Add(room2);
            _context.Rooms.Add(room3);
            _context.SaveChanges();

            // add rooms to local list
            _rooms.Add(room1);
            _rooms.Add(room2);
            _rooms.Add(room3);

            // instantiate the controller object with mock db context
            _controller = new RoomsController(_context);
        }


        // Test 1 > making sure I get a NotFound result if I try to EDIT from an ID of null value

        [TestMethod]
        public void IdIsNull()
        {

            var actionResult = _controller.Edit(id: null);
            var nullResult = (NotFoundResult)actionResult.Result; // convert generic ActionResult object to the expected result
            // make sure app returns 404 when searching for an id of null value
            Assert.AreEqual(404, nullResult.StatusCode);
        }

        
        // Test 2 > making sure I get a NotFound result if I try to EDIT from a non-existant ID

        [TestMethod]
        public void IdIsNotFound()
        {
            var actionResult = _controller.Edit(id:50);
            var notFoundResult = (NotFoundResult)actionResult.Result; // convert generic ActionResult object to the expected result
            // make sure app returns 404 when searching for an invalid id
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }


        // Test 3 > making sure that EDIT returns edit view
        
        [TestMethod]
        public void EditReturnsEditView()
        {
            var actionResult = _controller.Edit(id: 1);
            var viewResult = (ViewResult)actionResult.Result;
            Assert.IsNotNull(viewResult);
        }
    }
}