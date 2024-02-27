using FluentAssertions;
using Microsoft.Extensions.Time.Testing;
using Moq;
using Xunit;

namespace Mithril.Instants.Tests;

public sealed class InstantClockTests
{
    private static readonly DateTimeOffset _utcNow = DateTimeOffset.UtcNow;
    private readonly Instant _now = new (_utcNow, DefaultTimeZoneProvider.TIME_ZONE);

    private readonly FakeTimeProvider _timeProvider = new ();
    private readonly Mock<IInstantFactory> _instantFactoryMock = new ();

    [Fact]
    [Trait(nameof(Instant.Now), default)]
    public void Returns_Now_as_an_Instant()
    {
        ArrangeTimeProvider();
        ArrangeInstantFactory();

        GetClock().Now
            .Should().Be(_now);
    }

    private void ArrangeTimeProvider()
        => _timeProvider.SetUtcNow(_utcNow);

    private void ArrangeInstantFactory()
        => _instantFactoryMock
            .Setup(factory => factory.Create(_utcNow))
            .Returns(_now);

    private InstantClock GetClock() 
        => new (_timeProvider, _instantFactoryMock.Object);
}
