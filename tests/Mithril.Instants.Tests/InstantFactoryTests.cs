using FluentAssertions;
using Xunit;

namespace Mithril.Instants.Tests;

public sealed class InstantFactoryTests
{
    private static readonly DateTimeOffset _utcNow = DateTimeOffset.UtcNow;
    private readonly Instant _now = new (_utcNow, DefaultTimeZoneProvider.TIME_ZONE);

    [Fact]
    [Trait($"{nameof(InstantFactory.Create)}({nameof(DateTimeOffset)})", default)]
    public void Creates_an_Instant_from_a_dateTime_with_offset() 
    {
        GetFactory().Create(_utcNow)
            .Should().Be(_now);
    }

    [Fact]
    [Trait($"{nameof(InstantFactory.Create)}({nameof(String)})", default)]
    public void Creates_an_Instant_from_a_string_dateTime_with_offset()
    {
        GetFactory().Create(_utcNow.ToString("o"))
            .Should().Be(_now);
    }

    [Fact]
    [Trait($"{nameof(InstantFactory.Create)}({nameof(DateTime)})", default)]
    public void Creates_an_Instant_from_a_local_dateTime()
    {
        GetFactory().Create(_utcNow.ToLocalTime())
            .Should().Be(_now);
    }

    private static InstantFactory GetFactory()
        => new (new DefaultTimeZoneProvider());
}
