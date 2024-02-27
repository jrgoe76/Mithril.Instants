using Mithril.Instants.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace Mithril.Instants;

public readonly record struct Instant : IComparable
{
    private readonly DateTimeOffset _utcDateTime;
    private readonly string _timeZone;
    private readonly DateTime _localDateTime;

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

        _utcDateTime = dateTime.ToUniversalTime();
        _timeZone = timeZone;
        _localDateTime = _utcDateTime.ToLocal(_timeZone);
    }

    // Use this factory method with caution, IInstantClock.Now is recommended
    [ExcludeFromCodeCoverage]
    public static Instant Now(string? timeZone = null)
        => new (DateTimeOffset.UtcNow, timeZone ?? DefaultTimeZoneProvider.TIME_ZONE);

    [ExcludeFromCodeCoverage]
    public static Instant FromLocal(DateTime dateTime, string timeZone)
        => new (dateTime.ToUtc(timeZone), timeZone);

    public Instant New(DateTimeOffset dateTime)
        => new (dateTime, _timeZone);

    [ExcludeFromCodeCoverage]
    public DateTimeOffset ToUtc()
        => _utcDateTime;

    [ExcludeFromCodeCoverage]
    public DateTime ToLocal()
        => _localDateTime;

    public Instant StartOfDay()
        => FromLocal(_localDateTime.Date, _timeZone);

    public Instant Add(TimeSpan timeSpan)
        => New(_utcDateTime.Add(timeSpan));

    public Instant Subtract(TimeSpan timeSpan)
        => New(_utcDateTime.Subtract(timeSpan));

    public TimeSpan Subtract(Instant other)
        => _utcDateTime.Subtract(other._utcDateTime);

    public Instant AddMonths(int months)
        => New(_utcDateTime.AddMonths(months));

    public Instant AddYears(int years)
        => New(_utcDateTime.AddYears(years));

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
        => left._utcDateTime > right._utcDateTime;

    public static bool operator >= (Instant left, Instant right)
        => left > right || left == right;

    public static bool operator < (Instant left, Instant right)
        => left._utcDateTime < right._utcDateTime;

    public static bool operator <= (Instant left, Instant right)
        => left < right || left == right;

    [ExcludeFromCodeCoverage]
    public static TimeSpan operator - (Instant left, Instant right)
        => left.Subtract(right);
}
