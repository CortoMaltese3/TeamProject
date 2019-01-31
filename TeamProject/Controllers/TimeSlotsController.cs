using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeamProject.Models;

namespace TeamProject.Controllers
{
    public class TimeSlotsController : ApiController
    {
        private TimeSlot[] timeSlots = new TimeSlot[]
        {
            new TimeSlot(){ Pitch = "Γηπεδο 1", Time = "10:00", IsBooked = true},
            new TimeSlot(){ Pitch = "Γηπεδο 1", Time = "11:00"},
            new TimeSlot(){ Pitch = "Γηπεδο 1", Time = "12:00", IsBooked = true},
            new TimeSlot(){ Pitch = "Γηπεδο 1", Time = "13:00"},
            new TimeSlot(){ Pitch = "Γηπεδο 1", Time = "14:00"},
            new TimeSlot(){ Pitch = "Γηπεδο 1", Time = "15:00", IsBooked = true},
            new TimeSlot(){ Pitch = "Γηπεδο 1", Time = "16:00"},
            new TimeSlot(){ Pitch = "Γηπεδο 1", Time = "17:00"},
            new TimeSlot(){ Pitch = "Γηπεδο 1", Time = "18:00"},

            new TimeSlot(){ Pitch = "Γηπεδο 2", Time = "10:00", IsBooked = true},
            new TimeSlot(){ Pitch = "Γηπεδο 2", Time = "11:00"},
            new TimeSlot(){ Pitch = "Γηπεδο 2", Time = "12:00"},
            new TimeSlot(){ Pitch = "Γηπεδο 2", Time = "13:00", IsBooked = true},
            new TimeSlot(){ Pitch = "Γηπεδο 2", Time = "14:00"},
            new TimeSlot(){ Pitch = "Γηπεδο 2", Time = "15:00"},
            new TimeSlot(){ Pitch = "Γηπεδο 2", Time = "16:00", IsBooked = true},
            new TimeSlot(){ Pitch = "Γηπεδο 2", Time = "17:00"},
            new TimeSlot(){ Pitch = "Γηπεδο 2", Time = "18:00"},

        };
        //[HttpGet]
        public IEnumerable<TimeSlot> GetTimeSlots()
        {
            return timeSlots;
        }
    }
}
