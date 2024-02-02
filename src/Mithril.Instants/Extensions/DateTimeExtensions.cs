using NodaTime.Extensions;
using NodaTime;

namespace Mithril.Instants.Extensions;

public static class DateTimeExtensions
{
    public static DateTimeOffset ToUtc(this DateTime dateTime, string timeZone)
    {
        var zone = DateTimeZoneProviders.Tzdb[timeZone];
        var asLocal = dateTime.ToLocalDateTime();
        var asZoned = asLocal.InZoneLeniently(zone);
        var instant = asZoned.ToInstant();
        var asZonedInUtc = instant.InUtc();

        return asZonedInUtc.ToDateTimeUtc();
    }
}
