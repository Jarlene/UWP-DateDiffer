using System;
using System.Runtime.Serialization;
using TimeZones;

namespace DateDiffer.Models
{
    [DataContract]
    public class TimeZoneSelectItem
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string DisplayName { get; set; }

        [DataMember]
        public TimeSpan UtcOffset { get; set; }
    }

    public static class TimeZoneExtensions
    {
        public static TimeZoneSelectItem ToTimeZoneSelectItem(this ITimeZoneEx tz)
        {
            return new TimeZoneSelectItem()
            {
                Id = tz.Id,
                DisplayName = string.Format("(UTC{0}{1}) {2}",
                                    tz.BaseUtcOffset.Hours > 0 ? "+" : string.Empty,
                                    tz.BaseUtcOffset,
                                    tz.DaylightName),
                UtcOffset = tz.BaseUtcOffset
            };
        }
    }
}
