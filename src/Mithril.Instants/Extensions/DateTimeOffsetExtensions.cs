using NodaTime.Extensions;
using NodaTime;

namespace Mithril.Instants.Extensions;

public static class DateTimeOffsetExtensions
{
    public static DateTime ToLocal(this DateTimeOffset utcValue, string timeZone)
    {
        if (!utcValue.Offset.Equals(TimeSpan.Zero))
        {
            throw new ArgumentException("Expected UTC value", nameof(utcValue));
        }

        var zone = DateTimeZoneProviders.Tzdb[timeZone];
        var instant = utcValue.ToInstant();
        var inZone = instant.InZone(zone);

        return inZone.ToDateTimeUnspecified();
    }
}
