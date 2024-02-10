using FluentAssertions;
using Mithril.Instants.Extensions;
using Xunit;

namespace Mithril.Instants.Tests.Extensions;

public sealed class DateTimeExtensionsTests
{
    [Fact]
    [Trait("Method", "ToUtc")]
    public void Returns_UTC_date_from_a_local_date_and_its_time_zone()
    {
        DateTime.Parse("2024-01-01 20:00:00").ToUtc("America/New_York")
            .Should().Be(DateTimeOffset.Parse("2024-01-02 01:00:00 +00:00"));
        DateTime.Parse("2024-01-01 05:00:00").ToUtc("America/New_York")
            .Should().Be(DateTimeOffset.Parse("2024-01-01 10:00:00 +00:00"));

        DateTime.Parse("2024-01-01 10:00:00").ToUtc("America/New_York")
            .Should().Be(DateTimeOffset.Parse("2024-01-01 15:00:00 +00:00"));
        DateTime.Parse("2024-01-01 09:00:00").ToUtc("America/New_York")
            .Should().Be(DateTimeOffset.Parse("2024-01-01 14:00:00 +00:00"));

        DateTime.Parse("2024-01-02 01:00:00").ToUtc("Africa/Abidjan")
            .Should().Be(DateTimeOffset.Parse("2024-01-02 01:00:00 +00:00"));
    }
}
