namespace Mimica.Extensions
{
    public static class NumberExtensions
    {
        public static DateTime ToDateTime(this long timestamp)
        {
            var offset = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
            return offset.UtcDateTime;
        }
    }
}
