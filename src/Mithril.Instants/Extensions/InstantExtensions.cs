namespace Mithril.Instants.Extensions;

public static class InstantExtensions
{
    public static string ToUtcIsoString(this Instant instant)
        => instant.ToUtc().ToString("o");

    public static Instant AddDays(this Instant instant, double days)
        => instant.Add(TimeSpan.FromDays(days));

    public static Instant AddHours(this Instant instant, double hours)
        => instant.Add(TimeSpan.FromHours(hours));

    public static Instant AddMinutes(this Instant instant, double minutes)
        => instant.Add(TimeSpan.FromMinutes(minutes));

    public static Instant AddSeconds(this Instant instant, double seconds)
        => instant.Add(TimeSpan.FromSeconds(seconds));

    public static Instant NextStartOfDay(this Instant instant, double days = 1)
        => instant.StartOfDay().AddDays(days);

    public static Instant NextStartOfWeekday(this Instant instant, DayOfWeek dayOfWeek = DayOfWeek.Sunday)
    {
        var daysToAdd = (dayOfWeek - instant.DayOfWeek + 7) % 7;

        return instant.AddDays(daysToAdd > 0 ? daysToAdd : 7).StartOfDay();
    }

    public static Instant StartOfWeek(this Instant instant, DayOfWeek dayOfWeek = DayOfWeek.Sunday)
        => instant.NextStartOfWeekday(dayOfWeek).AddDays(-7);
}