using NodaTime;
using NodaTime.Extensions;

namespace Mithril.Instants.Extensions;

public static class DateTimeOffsetExtensions
{
    public static DateTime ToLocal(this DateTimeOffset value, string timeZone)
    {
        var zone = DateTimeZoneProviders.Tzdb[timeZone];
        var instant = value.ToUniversalTime().ToInstant();
        var inZone = instant.InZone(zone);

        return inZone.ToDateTimeUnspecified();
    }
}
