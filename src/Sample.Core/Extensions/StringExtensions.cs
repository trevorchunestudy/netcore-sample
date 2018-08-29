using System;

namespace Sample.Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Truncates string so that it is no longer than the specified number of characters.
        /// </summary>
        /// <param name="str">String to truncate.</param>
        /// <param name="length">Maximum string length.</param>
        /// <returns>Original string or a truncated one if the original was too long.</returns>
        /// <remarks>https://stackoverflow.com/questions/17249593/how-can-i-limit-a-string-to-no-more-than-a-certain-length</remarks>
        public static string Truncate(this string str, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length", "Length must be >= 0");
            }

            if (str == null)
            {
                return null;
            }

            int maxLength = Math.Min(str.Length, length);
            return str.Substring(0, maxLength);
        }
    }
}
