﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingManagement.Models
{
    public class Room
    {
        public int RoomID { get; set; }
        public string RoomType { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int HotelId { get; set; }
        public int GuestId { get; set; }

        //Parent model reference
        public Hotel Hotel { get; set; }
        public Guest Guest { get; set; }

        //Reference to child object
        public List<Booking> Bookings { get; set; }
    }
}
