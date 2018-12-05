using System;

namespace Kururu.Extensions
{
    public static class KMath
    {
        public static TimeData CalculateTime(DateTime time1, DateTime time2)
        {
            var Time = time2 - time1;
            var data = new TimeData();
            double Sec = Time.TotalSeconds;
            double min = Sec > 59 ? Math.Floor(Sec/60) : 0;
            double hour = min > 59 ? Math.Floor(min/60) : 0;
            double day = hour > 23 ? Math.Floor(hour/24) : 0;
            data.Days = (int)day;
            data.Hours = (int)(hour - 24*day);
            data.Minutes = (int)(min - hour*60);
            data.Seconds = (int)(Math.Floor(Sec - min*60));

            return data;
        }
    }
}