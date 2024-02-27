using FluentAssertions;
using Xunit;

namespace Mithril.Instants.Tests;

public sealed class InstantTests
{
    private readonly Instant _now = Instant.Now();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [Trait("Constructor", default)]
    public void Throws_an_error_creating_an_Instant_with_a_null_or_empty_timeZone(
        string? timeZone)
    {
        ((Func<Instant>)(() => new Instant(DateTimeOffset.Now, timeZone!)))
            .Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData("2024-01-01 00:00:00 -05:00", "America/New_York", "2024-01-01 5:00:00 +00:00")]
    [InlineData("2024-01-02 01:00:00 +00:00", "America/New_York", "2024-01-01 5:00:00 +00:00")]
    [InlineData("2024-01-01 20:00:00 +00:00", "Africa/Abidjan", "2024-01-01 00:00:00 +00:00")]
    [Trait(nameof(Instant.StartOfDay), default)]
    public void Creates_an_Instant_from_this_local_start_of_day(
        string dateTime, string timeZone, string startOfDay)
    {
        var instant = new Instant(DateTimeOffset.Parse(dateTime), timeZone);
        var expected = new Instant(DateTimeOffset.Parse(startOfDay), timeZone);

        instant.StartOfDay()
            .Should().Be(expected);
    }

    [Theory]
    [InlineData("2024-01-01 10:00:00 +00:00", "America/New_York", 5, "2024-01-01 10:05:00 +00:00")]
    [InlineData("2024-01-01 00:00:00 +00:00", "America/New_York", -5, "2023-12-31 23:55:00 +00:00")]
    [InlineData("2024-01-01 20:00:00 +00:00", "Africa/Abidjan", 60, "2024-01-01 21:00:00 +00:00")]
    [Trait(nameof(Instant.Add), default)]
    public void Creates_an_Instant_from_this_with_an_added_interval(
        string dateTime, string timeZone, double minutes, string addedDateTime)
    {
        var instant = new Instant(DateTimeOffset.Parse(dateTime), timeZone);
        var expected = new Instant(DateTimeOffset.Parse(addedDateTime), timeZone);

        instant.Add(TimeSpan.FromMinutes(minutes))
            .Should().Be(expected);
    }

    [Theory]
    [InlineData("2024-01-01 10:00:00 +00:00", "America/New_York", 5, "2024-01-01 09:55:00 +00:00")]
    [InlineData("2024-01-01 00:00:00 +00:00", "America/New_York", -5, "2024-01-01 00:05:00 +00:00")]
    [InlineData("2024-01-01 20:00:00 +00:00", "Africa/Abidjan", 60, "2024-01-01 19:00:00 +00:00")]
    [Trait($"{nameof(Instant.Subtract)}({nameof(TimeSpan)})", default)]
    public void Creates_an_Instant_from_this_with_a_subtracted_interval(
        string dateTime, string timeZone, double minutes, string subtractedDateTime)
    {
        var instant = new Instant(DateTimeOffset.Parse(dateTime), timeZone);
        var expected = new Instant(DateTimeOffset.Parse(subtractedDateTime), timeZone);

        instant.Subtract(TimeSpan.FromMinutes(minutes))
            .Should().Be(expected);
    }

    [Theory]
    [InlineData("2024-01-01 10:00:00 +00:00", "America/New_York", "2024-01-01 09:55:00 +00:00", 5)]
    [InlineData("2024-01-01 00:00:00 +00:00", "America/New_York", "2024-01-01 00:05:00 +00:00", -5)]
    [InlineData("2024-01-01 20:00:00 +00:00", "Africa/Abidjan", "2024-01-01 19:00:00 +00:00", 60)]
    [Trait($"{nameof(Instant.Subtract)}({nameof(Instant)})", default)]
    public void Returns_interval_between_this_and_another_Instant(
        string dateTime, string timeZone, string otherDateTime, int minutes)
    {
        var instant = new Instant(DateTimeOffset.Parse(dateTime), timeZone);
        var otherInstant = new Instant(DateTimeOffset.Parse(otherDateTime), timeZone);
        var expected = TimeSpan.FromMinutes(minutes);

        instant.Subtract(otherInstant)
            .Should().Be(expected);
    }

    [Theory]
    [InlineData("2024-01-01 10:00:00 +00:00", "America/New_York", 2, "2024-03-01 10:00:00 +00:00")]
    [InlineData("2024-01-01 00:00:00 +00:00", "America/New_York", -1, "2023-12-01 00:00:00 +00:00")]
    [InlineData("2024-01-01 20:00:00 +00:00", "Africa/Abidjan", 1, "2024-02-01 20:00:00 +00:00")]
    [Trait(nameof(Instant.AddMonths), default)]
    public void Creates_an_Instant_from_this_with_added_months(
        string dateTime, string timeZone, int months, string addedDateTime)
    {
        var instant = new Instant(DateTimeOffset.Parse(dateTime), timeZone);
        var expected = new Instant(DateTimeOffset.Parse(addedDateTime), timeZone);

        instant.AddMonths(months)
            .Should().Be(expected);
    }

    [Theory]
    [InlineData("2024-01-01 10:00:00 +00:00", "America/New_York", 2, "2026-01-01 10:00:00 +00:00")]
    [InlineData("2024-01-01 00:00:00 +00:00", "America/New_York", -1, "2023-01-01 00:00:00 +00:00")]
    [InlineData("2024-01-01 20:00:00 +00:00", "Africa/Abidjan", 1, "2025-01-01 20:00:00 +00:00")]
    [Trait(nameof(Instant.AddYears), default)]
    public void Creates_an_Instant_from_this_with_added_years(
        string dateTime, string timeZone, int years, string addedDateTime)
    {
        var instant = new Instant(DateTimeOffset.Parse(dateTime), timeZone);
        var expected = new Instant(DateTimeOffset.Parse(addedDateTime), timeZone);

        instant.AddYears(years)
            .Should().Be(expected);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("not an Instant")]
    [Trait(nameof(Instant.CompareTo), default)]
    public void Throws_an_error_comparing_to_a_null_or_no_Instant_type(
        object? obj)
    {
        ((Func<int>)(() => _now.CompareTo(obj)))
            .Should().Throw<InvalidCastException>();
    }

    [Fact]
    [Trait(nameof(Instant.CompareTo), default)]
    public void Returns_minus1_comparing_to_a_bigger_Instant()
    {
        _now.CompareTo(_now.Add(TimeSpan.FromHours(1)))
            .Should().Be(-1);
    }

    [Fact]
    [Trait(nameof(Instant.CompareTo), default)]
    public void Returns_1_comparing_to_smaller_Instant()
    {
        _now.CompareTo(_now.Add(TimeSpan.FromHours(-1)))
            .Should().Be(1);
    }

    [Fact]
    [Trait(nameof(Instant.CompareTo), default)]
    public void Returns_0_comparing_to_an_equal_Instant()
    {
        _now.CompareTo(_now)
            .Should().Be(0);
    }

    [Theory]
    [InlineData("2024-01-02 01:00:00 +00:00", "America/New_York", "1/1/2024 8:00 PM ( America/New_York )")]
    [InlineData("2024-01-01 10:00:00 -04:00", "America/New_York", "1/1/2024 9:00 AM ( America/New_York )")]
    [InlineData("2024-01-01 20:00:00 -05:00", "Africa/Abidjan", "1/2/2024 1:00 AM ( Africa/Abidjan )")]
    [Trait(nameof(Instant.ToString), default)]
    public void Returns_this_representation(
        string dateTime, string timeZone, string representation)
    {
        new Instant(DateTimeOffset.Parse(dateTime), timeZone).ToString()
            .Should().Be(representation);
    }

    [Fact]
    [Trait(">", default)]
    public void Asserts_the_left_Instant_is_bigger_than_the_right_one()
    {
        (_now.Add(TimeSpan.FromHours(1)) > _now)
            .Should().BeTrue();
    }

    [Fact]
    [Trait(">=", default)]
    public void Asserts_the_left_Instant_is_bigger_than_or_equal_to_the_right_one()
    {
        var right = _now;

        (_now.Add(TimeSpan.FromHours(1)) >= right)
            .Should().BeTrue();
        (_now >= right)
            .Should().BeTrue();
    }

    [Fact]
    [Trait("<", default)]
    public void Asserts_the_left_Instant_is_smaller_than_the_right_one()
    {
        (_now.Add(TimeSpan.FromHours(-1)) < _now)
            .Should().BeTrue();
    }

    [Fact]
    [Trait("<=", default)]
    public void Asserts_the_left_Instant_is_smaller_than_or_equal_to_the_right_one()
    {
        var right = _now;

        (_now.Add(TimeSpan.FromHours(-1)) <= right)
            .Should().BeTrue();
        (_now <= right)
            .Should().BeTrue();
    }
}
