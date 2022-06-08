using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingManagement.Models
{
    public class Room
    {
        public int RoomID { get; set; }

        [Required]
        public string RoomType { get; set; }

        [Required]
        public string Description { get; set; }
        public string Photo { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")] // MS currency format
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
