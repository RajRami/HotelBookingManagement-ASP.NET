using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingManagement.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public double TotalCharge { get; set; }
        public int CreditCardNumber { get; set; }
        public string CardHolder { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public int HotelId { get; set; }
        public int BookingID { get; set; }
        public int GuestId { get; set; }

        //Parent model reference
        public Hotel Hotel { get; set; }
        public Booking Booking { get; set; }
        public Guest Guest { get; set; }
    }
}
