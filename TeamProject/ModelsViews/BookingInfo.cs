using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeamProject.Models;

namespace TeamProject.ModelsViews
{
    public class BookingInfo
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime BookedAt { get; set; }

        public BookingInfo(Booking booking)
        {
            Username = $"{booking.User.Lastname} {booking.User.Firstname}";
            Email = booking.User.Email;
            BookedAt = booking.BookedAt;
        }
    }
}