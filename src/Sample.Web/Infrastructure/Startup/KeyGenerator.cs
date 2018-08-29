using Serilog.Events;
using Serilog.Sinks.AzureTableStorage.KeyGenerator;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Sample.Web.Infrastructure.Startup
{
    public class KeyGenerator : IKeyGenerator
    {
        // Valid RowKey name characters
        static readonly Regex _rowKeyNotAllowedMatch = new Regex(@"(\\|/|#|\?|[\x00-\x1f]|[\x7f-\x9f])");

        private static string GetValidStringForTableKey(string s)
        {
            return _rowKeyNotAllowedMatch.Replace(s, "");
        }

        public string GeneratePartitionKey(LogEvent logEvent)
        {
            return (DateTime.MaxValue.Ticks - logEvent.Timestamp.Ticks).ToString().Substring(0, 9);
        }

        public string GenerateRowKey(LogEvent logEvent, string suffix = null)
        {
            var prefixBuilder = new StringBuilder(512);

            // Join level and message template
            LogEventPropertyValue sourceContext;
            logEvent.Properties.TryGetValue("SourceContext", out sourceContext);
            prefixBuilder.Append(sourceContext).Append('|');

            var postfixBuilder = new StringBuilder(512);

            if (suffix != null)
                postfixBuilder.Append('|').Append(GetValidStringForTableKey(suffix));

            // Append GUID to postfix
            postfixBuilder.Append('|').Append(Guid.NewGuid());

            // Truncate prefix if too long
            var maxPrefixLength = 1024 - postfixBuilder.Length;
            if (prefixBuilder.Length > maxPrefixLength)
            {
                prefixBuilder.Length = maxPrefixLength;
            }

            return prefixBuilder.Append(postfixBuilder).ToString();
        }
    }
}
