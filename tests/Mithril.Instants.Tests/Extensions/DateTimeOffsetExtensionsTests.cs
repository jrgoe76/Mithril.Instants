using FluentAssertions;
using Mithril.Instants.Extensions;
using Xunit;

namespace Mithril.Instants.Tests.Extensions;

public sealed class DateTimeOffsetExtensionsTests
{
    [Theory]
    [InlineData("2024-01-02 01:00:00 +00:00", "America/New_York", "2024-01-01 20:00:00")]
    [InlineData("2024-01-01 10:00:00 +00:00", "America/New_York", "2024-01-01 05:00:00")]
    [InlineData("2024-01-01 10:00:00 -05:00", "America/New_York", "2024-01-01 10:00:00")]
    [InlineData("2024-01-01 10:00:00 -04:00", "America/New_York", "2024-01-01 09:00:00")]
    [InlineData("2024-01-01 20:00:00 -05:00", "Africa/Abidjan", "2024-01-02 01:00:00")]
    [Trait("Method", nameof(DateTimeOffsetExtensions.ToLocal))]
    public void Returns_local_dateTime_for_a_timeZone_from_a_dateTime_with_offset(
        string dateTimeOffset, string timeZone, string dateTime)
    {
        DateTimeOffset.Parse(dateTimeOffset).ToLocal(timeZone)
            .Should().Be(DateTime.Parse(dateTime));
    }
}
