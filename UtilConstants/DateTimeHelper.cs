using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilConstants
{
    public static class DateTimeHelper
    {
        private static readonly TimeZoneInfo PeruTimeZone =
            TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
    
        public static DateTime NowInPeru()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, PeruTimeZone);
        }
    }
    

}
