using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingManagement.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public int GuestId { get; set; }

        //Parent model reference
        public Hotel Hotel { get; set; }
        public Guest Guest { get; set; }
        public Room Room { get; set; }

        //Reference to child object
        public List<Payment> Payments { get; set; }
    }
}
