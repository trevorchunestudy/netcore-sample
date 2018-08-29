using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.Core.ValueObjects
{
    public class DateRange : ValueObject<DateRange>
    {
        private DateRange() { }

        private DateRange(DateTime start, DateTime end)
        {
            StartDate = start;
            EndDate = end;
        }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime StartDate { get; private set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime EndDate { get; private set; }

        [NotMapped]
        public bool IsValid
        {
            get
            {
                var now = DateTime.UtcNow;
                return StartDate <= now &&
                       EndDate >= now;
            }
        }

        public static DateRange Create(DateTime start, DateTime end)
        {
            var now = DateTime.UtcNow;
            start = start >= now ? start : now;
            end = end > start ? end : start.AddDays(30);

            return new DateRange(start, end);
        }
    }
}
