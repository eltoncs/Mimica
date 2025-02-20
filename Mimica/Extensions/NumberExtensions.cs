namespace Mimica.Extensions
{
    public static class NumberExtensions
    {
        /// <summary>
        /// Converts a Unix time stamp to DateTime.
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns>DateTime</returns>
        public static DateTime ToDateTime(this long timestamp)
        {
            var offset = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
            return offset.UtcDateTime;
        }
    }
}
