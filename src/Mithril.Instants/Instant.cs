using Mithril.Instants.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace Mithril.Instants;

public readonly record struct Instant : IComparable
{
    private readonly string _timeZone;

    internal DateTimeOffset UtcValue { get; init; }

    public DayOfWeek DayOfWeek => ToLocal().DayOfWeek;
    public DateTime Date => ToLocal().Date;
    public int Year => ToLocal().Year;
    public int Month => ToLocal().Month;
    public int Day => ToLocal().Day;

    public TimeSpan TimeOfDay => ToLocal().TimeOfDay;
    public int Hour => ToLocal().Hour;
    public int Minute => ToLocal().Minute;
    public int Second => ToLocal().Second;
    public int Millisecond => ToLocal().Millisecond;
    public int Microsecond => ToLocal().Microsecond;
    public int Nanosecond => ToLocal().Nanosecond;

    public long Ticks => ToLocal().Ticks;

    public Instant(DateTimeOffset dateTime, string timeZone)
    {
        if (string.IsNullOrEmpty(timeZone))
        {
            throw new ArgumentException($"The {nameof(timeZone)} provided is required.", nameof(timeZone));
        }

        UtcValue = dateTime.ToUniversalTime();
        _timeZone = timeZone;
    }

    // Use this factory method with caution, IInstantClock.Now is recommended
    [ExcludeFromCodeCoverage]
    public static Instant Now(string? timeZone = null)
        => new (DateTimeOffset.UtcNow, timeZone ?? DefaultTimeZoneProvider.TIME_ZONE);

    // Use this factory method with caution, IInstantFactory.Create(DateTime dateTime) is recommended
    [ExcludeFromCodeCoverage]
    public static Instant FromLocal(DateTime dateTime, string timeZone)
        => new (dateTime.ToUtc(timeZone), timeZone);

    [ExcludeFromCodeCoverage]
    public DateTime ToLocal()
        => UtcValue.ToLocal(_timeZone);

    public Instant Add(TimeSpan timeSpan)
        => this with { UtcValue = UtcValue.Add(timeSpan) };

    public Instant Subtract(TimeSpan timeSpan)
        => this with { UtcValue = UtcValue.Subtract(timeSpan) };

    public TimeSpan Subtract(Instant other)
        => UtcValue.Subtract(other.UtcValue);

    public int CompareTo(object? obj)
    {
        if (obj is not Instant other)
        {
            throw new InvalidCastException("Comparing with null or non-Instant values is not allowed.");
        }

        if (this < other) { return -1; }
        if (this > other) { return 1; }
        return 0;
    }

    public override string ToString()
        => $"{ToLocal():g} ( {_timeZone} )";

    public static bool operator > (Instant left, Instant right)
        => left.UtcValue > right.UtcValue;

    public static bool operator >= (Instant left, Instant right)
        => left > right || left == right;

    public static bool operator < (Instant left, Instant right)
        => left.UtcValue < right.UtcValue;

    public static bool operator <= (Instant left, Instant right)
        => left < right || left == right;

    [ExcludeFromCodeCoverage]
    public static TimeSpan operator - (Instant left, Instant right)
        => left.Subtract(right);
}
