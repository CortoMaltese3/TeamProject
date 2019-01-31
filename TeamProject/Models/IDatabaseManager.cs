using System;
using System.Collections.Generic;

namespace TeamProject.Models
{
    public interface IDatabaseManager
    {
        IEnumerable<TimeSlot> GetTimeSlots(DateTime date, int pitchId);
    }
}