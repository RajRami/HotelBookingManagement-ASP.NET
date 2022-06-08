using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingManagement.Models
{
    public class Guest
    {
        public int GuestId { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string AddressLine1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        //Reference to child object
        public List<Room> Rooms { get; set; }
        public List<Booking> Bookings { get; set; }
        public List<Payment> Payments { get; set; }
    }
}
 