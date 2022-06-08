using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingManagement.Models
{
    public class Hotel
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public int NumberOfRooms { get; set; }
        public string ContactNum { get; set; }
        public string AddressLine1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public double StarRating { get; set; }

        //Reference to child object
        public List<Room> Rooms { get; set; }
        public List<Booking> Bookings { get; set; }
        public List<Payment> Payments { get; set; }
    }
}
