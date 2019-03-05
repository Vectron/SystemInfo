using System;

namespace SystemInfo.Core
{
    internal class Utils
    {
        // Convert a float value to a rounded percantage
        public static int FloatToPercent(float value)
        {
            var percantage = (value * 100) / 100;
            var truncated = Math.Truncate(percantage);
            return Convert.ToInt32(truncated);
        }
    }
}