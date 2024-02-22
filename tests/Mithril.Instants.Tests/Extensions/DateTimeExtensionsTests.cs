using FluentAssertions;
using Mithril.Instants.Extensions;
using Xunit;

namespace Mithril.Instants.Tests.Extensions;

public sealed class DateTimeExtensionsTests
{
    [Theory]
    [InlineData("2024-01-01 20:00:00", "America/New_York", "2024-01-02 01:00:00 +00:00")]
    [InlineData("2024-01-01 05:00:00", "America/New_York", "2024-01-01 10:00:00 +00:00")]
    [InlineData("2024-01-01 10:00:00", "America/New_York", "2024-01-01 15:00:00 +00:00")]
    [InlineData("2024-01-01 09:00:00", "America/New_York", "2024-01-01 14:00:00 +00:00")]
    [InlineData("2024-01-02 01:00:00", "Africa/Abidjan", "2024-01-02 01:00:00 +00:00")]
    [Trait(nameof(DateTimeExtensions.ToUtc), default)]
    public void Returns_UTC_dateTime_from_a_local_dateTime_and_its_timeZone(
        string dateTime, string timeZone, string utcDateTime)
    {
        DateTime.Parse(dateTime).ToUtc(timeZone)
            .Should().Be(DateTimeOffset.Parse(utcDateTime));
    }
}
