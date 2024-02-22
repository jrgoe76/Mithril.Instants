using Mithril.Instants.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace Mithril.Instants;

public readonly record struct Instant : IComparable
{
    private readonly string _timeZone;
    private readonly DateTime _localDateTime;
    
    internal DateTimeOffset UtcDateTime { get; init; }

    public DayOfWeek DayOfWeek => _localDateTime.DayOfWeek;
    public DateTime Date => _localDateTime.Date;
    public int Year => _localDateTime.Year;
    public int Month => _localDateTime.Month;
    public int Day => _localDateTime.Day;

    public TimeSpan TimeOfDay => _localDateTime.TimeOfDay;
    public int Hour => _localDateTime.Hour;
    public int Minute => _localDateTime.Minute;
    public int Second => _localDateTime.Second;
    public int Millisecond => _localDateTime.Millisecond;
    public int Microsecond => _localDateTime.Microsecond;
    public int Nanosecond => _localDateTime.Nanosecond;

    public long Ticks => _localDateTime.Ticks;

    public Instant(DateTimeOffset dateTime, string timeZone)
    {
        if (string.IsNullOrEmpty(timeZone))
        {
            throw new ArgumentException($"The {nameof(timeZone)} provided is required.", nameof(timeZone));
        }

        _timeZone = timeZone;
        UtcDateTime = dateTime.ToUniversalTime();
        _localDateTime = UtcDateTime.ToLocal(_timeZone);
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
    public DateTimeOffset ToUtc()
        => UtcDateTime;

    [ExcludeFromCodeCoverage]
    public DateTime ToLocal()
        => _localDateTime;

    public Instant Add(TimeSpan timeSpan)
        => new (UtcDateTime.Add(timeSpan), _timeZone);

    public Instant Subtract(TimeSpan timeSpan)
        => new (UtcDateTime.Subtract(timeSpan), _timeZone);

    public TimeSpan Subtract(Instant other)
        => UtcDateTime.Subtract(other.UtcDateTime);

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
        => $"{_localDateTime:g} ( {_timeZone} )";

    public static bool operator > (Instant left, Instant right)
        => left.UtcDateTime > right.UtcDateTime;

    public static bool operator >= (Instant left, Instant right)
        => left > right || left == right;

    public static bool operator < (Instant left, Instant right)
        => left.UtcDateTime < right.UtcDateTime;

    public static bool operator <= (Instant left, Instant right)
        => left < right || left == right;

    [ExcludeFromCodeCoverage]
    public static TimeSpan operator - (Instant left, Instant right)
        => left.Subtract(right);
}
