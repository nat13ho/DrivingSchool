using System;
using System.Linq;
using System.Collections.Generic;

namespace DrivingSchool
{
    public class DaysHelper
    {
        public static string GetDays(IEnumerable<DayOfWeek> dayOfWeeks) => string.Join(", ", dayOfWeeks.Select(d => DayToString(d)));

        static string DayToString(DayOfWeek dayOfWeek)
        {
            return dayOfWeek switch
            {
                DayOfWeek.Monday => "пн",
                DayOfWeek.Tuesday => "вт",
                DayOfWeek.Wednesday => "ср",
                DayOfWeek.Thursday => "чт",
                DayOfWeek.Friday => "пт",
                DayOfWeek.Saturday => "сб",
                DayOfWeek.Sunday => "вс",
                _ => throw new ArgumentException("Not a day of week")
            };
        }
    }
}