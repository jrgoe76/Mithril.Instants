using FluentAssertions;
using Mithril.Instants.Extensions;
using Xunit;

namespace Mithril.Instants.Tests.Extensions;

public sealed class DateTimeOffsetExtensionsTests
{
    [Fact]
    [Trait("Method", "ToLocal")]
    public void Returns_local_date_from_a_date_with_offset()
    {
        DateTimeOffset.Parse("2024-01-02 01:00:00 +00:00").ToLocal("America/New_York")
            .Should().Be(DateTime.Parse("2024-01-01 20:00:00"));
        DateTimeOffset.Parse("2024-01-01 10:00:00 +00:00").ToLocal("America/New_York")
            .Should().Be(DateTime.Parse("2024-01-01 05:00:00"));

        DateTimeOffset.Parse("2024-01-01 10:00:00 -05:00").ToLocal("America/New_York")
            .Should().Be(DateTime.Parse("2024-01-01 10:00:00"));
        DateTimeOffset.Parse("2024-01-01 10:00:00 -04:00").ToLocal("America/New_York")
            .Should().Be(DateTime.Parse("2024-01-01 09:00:00"));

        DateTimeOffset.Parse("2024-01-01 20:00:00 -05:00").ToLocal("Africa/Abidjan")
            .Should().Be(DateTime.Parse("2024-01-02 01:00:00"));
    }
}
