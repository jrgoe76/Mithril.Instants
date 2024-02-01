namespace Mithril.Instants;

public readonly record struct Instant : IComparable
{
    private readonly string _timeZone;

    internal DateTimeOffset UtcValue { get; init; }

    internal Instant(DateTimeOffset utcValue, string timeZone)
    {
        if (!utcValue.Offset.Equals(TimeSpan.Zero))
        {
            throw new ArgumentException($"The {nameof(utcValue)} provided is not UTC.", nameof(utcValue));
        }
        if (string.IsNullOrEmpty(timeZone))
        {
            throw new ArgumentException($"The {nameof(timeZone)} provided is required.", nameof(timeZone));
        }

        UtcValue = utcValue;
        _timeZone = timeZone;
    }

    // Use this factory method with caution, IInstantFactory.Create(DateTimeOffset utcValue) is recommended
    public static Instant FromUtc(DateTimeOffset utcValue, string timeZone)
        => new (utcValue, timeZone);

    // Use this factory method with caution, IInstantClock.Now is recommended
    public static Instant Now(string? timeZone = null)
        => FromUtc(DateTimeOffset.UtcNow, timeZone ?? TimeZoneProviderDefault.TIME_ZONE);

    public Instant Add(TimeSpan timeSpan)
        => FromUtc(UtcValue.Add(timeSpan), _timeZone);

    public static bool operator > (Instant left, Instant right)
        => left.UtcValue > right.UtcValue;

    public static bool operator >= (Instant left, Instant right)
        => left > right || left == right;

    public static bool operator < (Instant left, Instant right)
        => left.UtcValue < right.UtcValue;

    public static bool operator <=(Instant left, Instant right)
        => left < right || left == right;

    public int CompareTo(object? obj)
    {
        if (obj is not Instant)
        {
            throw new InvalidCastException();
        }

        throw new NotImplementedException();
    }
}