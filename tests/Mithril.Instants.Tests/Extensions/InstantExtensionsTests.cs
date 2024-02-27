using FluentAssertions;
using Mithril.Instants.Extensions;
using Xunit;

namespace Mithril.Instants.Tests.Extensions;

public sealed class InstantExtensionsTests
{
    [Theory]
    [InlineData("2024-01-01 10:00:00 -05:00", "America/New_York", "2024-01-01T15:00:00.0000000+00:00")]
    [InlineData("2024-01-01 10:00:00 +00:00", "America/New_York", "2024-01-01T10:00:00.0000000+00:00")]
    [InlineData("2024-01-01 10:00:00 +00:00", "Africa/Abidjan", "2024-01-01T10:00:00.0000000+00:00")]
    [Trait(nameof(InstantExtensions.ToUtcIsoString), default)]
    public void Returns_the_UTC_ISO_representation(
        string dateTime, string timeZone, string expected)
    {
        var instant = new Instant(DateTimeOffset.Parse(dateTime), timeZone);

        instant.ToUtcIsoString()
            .Should().Be(expected);
    }

    [Theory]
    [InlineData("2024-01-01 10:00:00 -05:00", "America/New_York", 5, "2024-01-06 10:00:00 -05:00")]
    [InlineData("2024-01-01 10:00:00 +00:00", "America/New_York", -5, "2023-12-27 10:00:00 +00:00")]
    [InlineData("2024-01-01 10:00:00 +00:00", "Africa/Abidjan", 2, "2024-01-03 10:00:00 +00:00")]
    [Trait(nameof(InstantExtensions.AddDays), default)]
    public void Creates_an_Instant_from_this_with_added_days(
        string dateTime, string timeZone, double days, string addedDateTime)
    {
        var instant = new Instant(DateTimeOffset.Parse(dateTime), timeZone);
        var expected = new Instant(DateTimeOffset.Parse(addedDateTime), timeZone);

        instant.AddDays(days)
            .Should().Be(expected);
    }

    [Theory]
    [InlineData("2024-01-01 10:00:00 -05:00", "America/New_York", 5, "2024-01-01 15:00:00 -05:00")]
    [InlineData("2024-01-01 10:00:00 +00:00", "America/New_York", -5, "2024-01-01 05:00:00 +00:00")]
    [InlineData("2024-01-01 10:00:00 +00:00", "Africa/Abidjan", 2, "2024-01-01 12:00:00 +00:00")]
    [Trait(nameof(InstantExtensions.AddHours), default)]
    public void Creates_an_Instant_from_this_with_added_hours(
        string dateTime, string timeZone, double hours, string addedDateTime)
    {
        var instant = new Instant(DateTimeOffset.Parse(dateTime), timeZone);
        var expected = new Instant(DateTimeOffset.Parse(addedDateTime), timeZone);

        instant.AddHours(hours)
            .Should().Be(expected);
    }

    [Theory]
    [InlineData("2024-01-01 10:00:00 -05:00", "America/New_York", 5, "2024-01-01 10:05:00 -05:00")]
    [InlineData("2024-01-01 10:00:00 +00:00", "America/New_York", -5, "2024-01-01 09:55:00 +00:00")]
    [InlineData("2024-01-01 10:00:00 +00:00", "Africa/Abidjan", 2, "2024-01-01 10:02:00 +00:00")]
    [Trait(nameof(InstantExtensions.AddMinutes), default)]
    public void Creates_an_Instant_from_this_with_added_minutes(
        string dateTime, string timeZone, double minutes, string addedDateTime)
    {
        var instant = new Instant(DateTimeOffset.Parse(dateTime), timeZone);
        var expected = new Instant(DateTimeOffset.Parse(addedDateTime), timeZone);

        instant.AddMinutes(minutes)
            .Should().Be(expected);
    }

    [Theory]
    [InlineData("2024-01-01 10:00:00 -05:00", "America/New_York", 5, "2024-01-01 10:00:05 -05:00")]
    [InlineData("2024-01-01 10:00:00 +00:00", "America/New_York", -5, "2024-01-01 09:59:55 +00:00")]
    [InlineData("2024-01-01 10:00:00 +00:00", "Africa/Abidjan", 2, "2024-01-01 10:00:02 +00:00")]
    [Trait(nameof(InstantExtensions.AddSeconds), default)]
    public void Creates_an_Instant_from_this_with_added_seconds(
        string dateTime, string timeZone, double seconds, string addedDateTime)
    {
        var instant = new Instant(DateTimeOffset.Parse(dateTime), timeZone);
        var expected = new Instant(DateTimeOffset.Parse(addedDateTime), timeZone);

        instant.AddSeconds(seconds)
            .Should().Be(expected);
    }

    [Theory]
    [InlineData("2024-01-01 10:00:00 -05:00", "America/New_York", 5, "2024-01-06 00:00:00 -05:00")]
    [InlineData("2024-01-01 10:00:00 +00:00", "America/New_York", -5, "2023-12-27 05:00:00 +00:00")]
    [InlineData("2024-01-01 10:00:00 +00:00", "Africa/Abidjan", 2, "2024-01-03 00:00:00 +00:00")]
    [Trait(nameof(InstantExtensions.NextStartOfDay), default)]
    public void Creates_an_Instant_from_this_local_start_of_day_within_days(
        string dateTime, string timeZone, double days, string nextDateTime)
    {
        var instant = new Instant(DateTimeOffset.Parse(dateTime), timeZone);
        var expected = new Instant(DateTimeOffset.Parse(nextDateTime), timeZone);

        instant.NextStartOfDay(days)
            .Should().Be(expected);
    }
    
    [Theory]
    [InlineData("2024-01-01 10:00:00 -05:00", "America/New_York", DayOfWeek.Sunday, "2024-01-07 00:00:00 -05:00")]
    [InlineData("2024-01-02 10:00:00 +00:00", "America/New_York", DayOfWeek.Monday, "2024-01-08 5:00:00 +00:00")]
    [InlineData("2024-01-01 10:00:00 +00:00", "Africa/Abidjan", DayOfWeek.Friday, "2024-01-05 00:00:00 +00:00")]
    [Trait(nameof(InstantExtensions.NextStartOfWeekday), default)]
    public void Creates_an_Instant_from_this_local_start_of_weekday(
        string dateTime, string timeZone, DayOfWeek dayOfWeek, string nextDateTime)
    {
        var instant = new Instant(DateTimeOffset.Parse(dateTime), timeZone);
        var expected = new Instant(DateTimeOffset.Parse(nextDateTime), timeZone);

        instant.NextStartOfWeekday(dayOfWeek)
            .Should().Be(expected);
    }

    [Theory]
    [InlineData("2024-01-02 01:00:00 +00:00", "America/New_York", DayOfWeek.Sunday, "2023-12-31 05:00:00 +00:00")]
    [InlineData("2024-01-02 01:00:00 +00:00", "America/New_York", DayOfWeek.Tuesday, "2023-12-26 05:00:00 +00:00")]
    [InlineData("2024-01-01 20:00:00 +00:00", "Africa/Abidjan", DayOfWeek.Sunday, "2023-12-31 00:00:00 +00:00")]
    [Trait(nameof(InstantExtensions.StartOfWeek), default)]
    public void Creates_an_Instant_from_this_local_start_of_week(
        string dateTime, string timeZone, DayOfWeek dayOfWeek, string startOfWeek)
    {
        var instant = new Instant(DateTimeOffset.Parse(dateTime), timeZone);
        var expected = new Instant(DateTimeOffset.Parse(startOfWeek), timeZone);

        instant.StartOfWeek(dayOfWeek)
            .Should().Be(expected);
    }
}
