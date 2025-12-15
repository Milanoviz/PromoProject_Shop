using System;

namespace Modules.VIP.Helpers
{
    public static class TimeSpanExtensions
    {
        public static string ToPrettyTimeWithLetters(this TimeSpan time)
        {
            if (time <= TimeSpan.Zero)
                return "0 сек";

            if (time.TotalMinutes < 1)
            {
                return $"{time.Seconds} сек";
            }

            if (time.TotalHours < 1)
            {
                return $"{time.Minutes:D2}:{time.Seconds:D2}";
            }

            if (time.TotalDays < 1)
            {
                return $"{time.Hours:D2}:{time.Minutes:D2}:{time.Seconds:D2}";
            }

            return $"{(int)time.TotalDays} д {time.Hours:D2}:{time.Minutes:D2}:{time.Seconds:D2}";
        }
    }
}