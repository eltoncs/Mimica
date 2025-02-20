namespace Mimica.Extensions
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// Converts a DateTime to Unix time stamp.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns>Unix time stamp</returns>
        public static long ToUnixTimeStamp(this DateTime dateTime)
        {
            return (long)(dateTime.ToUniversalTime()
                .Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                .TotalMilliseconds);
        }

        /// <summary>
        /// Converts a DateTime to Unix Date Stamp.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns>Date stamp</returns>
        public static long ToUnixDateStamp(this DateTime dateTime)
        {
            return (long)(dateTime.ToUniversalTime()
                .Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                .TotalDays);
        }
    }
}
