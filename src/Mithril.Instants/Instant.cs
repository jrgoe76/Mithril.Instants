using Mithril.Instants.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace Mithril.Instants;

public readonly record struct Instant : IComparable
{
    private readonly string _timeZone;

    internal DateTimeOffset UtcValue { get; init; }

    public Instant(DateTimeOffset value, string timeZone)
    {
        if (string.IsNullOrEmpty(timeZone))
        {
            throw new ArgumentException($"The {nameof(timeZone)} provided is required.", nameof(timeZone));
        }

        UtcValue = value.ToUniversalTime();
        _timeZone = timeZone;
    }

    // Use this factory method with caution, IInstantClock.Now is recommended
    public static Instant Now(string? timeZone = null)
        => new (DateTimeOffset.UtcNow, timeZone ?? TimeZoneProviderDefault.TIME_ZONE);

    public Instant Add(TimeSpan timeSpan)
        => new (UtcValue.Add(timeSpan), _timeZone);

    [ExcludeFromCodeCoverage]
    public DateTime ToLocal()
        => UtcValue.ToLocal(_timeZone);

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
}