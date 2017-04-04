using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoupleActivities.Utils
{
    static class StringUtils
    {
        public static void GetIntFromString(string inString, out int outInt)
        {
            if (!int.TryParse(inString, out outInt))
            {
                // ERROR
            }
        }

        public static DateTime CreateDateFromTextBoxes(string hoursString, string minutesString)
        {
            var hoursInt = 0;
            var minutesInt = 0;

            StringUtils.GetIntFromString(hoursString, out hoursInt);
            StringUtils.GetIntFromString(minutesString, out minutesInt);

            if (hoursInt < 0 || hoursInt > 23 || minutesInt < 0 || minutesInt > 60)
            {
                // ERROR
            }
            
            var now = DateTime.Now;
            var newDate = new DateTime(now.Year, now.Month, now.Day, hoursInt, minutesInt, now.Second);
            return newDate;
        }
    }
}
