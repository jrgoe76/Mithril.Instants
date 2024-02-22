using FluentAssertions;
using Xunit;

namespace Mithril.Instants.Tests;

public sealed class InstantTests
{
    private readonly Instant _now = Instant.Now();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [Trait("Constructor", nameof(Instant))]
    public void Throws_an_error_creating_an_Instant_with_a_null_or_empty_timeZone(
        string? timeZone)
    {
        ((Func<Instant>)(() => new Instant(DateTimeOffset.Now, timeZone!)))
            .Should().Throw<ArgumentException>();
    }

    [Fact]
    [Trait("Method", nameof(Instant.Add))]
    public void Creates_an_Instant_from_this_with_an_added_interval()
    {
        var oneHour = TimeSpan.FromHours(1);

        _now.Add(oneHour)
            .Should().Be(_now with { UtcValue = _now.UtcValue.Add(oneHour) });
    }

    [Fact]
    [Trait("Method", $"{nameof(Instant.Subtract)}({nameof(TimeSpan)})")]
    public void Creates_an_Instant_from_this_with_a_subtracted_interval()
    {
        var oneHour = TimeSpan.FromHours(1);

        _now.Subtract(oneHour)
            .Should().Be(_now with { UtcValue = _now.UtcValue.Subtract(oneHour) });
    }

    [Fact]
    [Trait("Method", $"{nameof(Instant.Subtract)}({nameof(Instant)})")]
    public void Returns_interval_between_this_and_another_Instant()
    {
        var oneHour = TimeSpan.FromHours(1);

        _now.Subtract(_now.Subtract(oneHour))
            .Should().Be(oneHour);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("not an Instant")]
    [Trait("Method", nameof(Instant.CompareTo))]
    public void Throws_an_error_comparing_to_a_null_or_no_Instant_type(
        object? obj)
    {
        ((Func<int>)(() => _now.CompareTo(obj)))
            .Should().Throw<InvalidCastException>();
    }

    [Fact]
    [Trait("Method", nameof(Instant.CompareTo))]
    public void Returns_minus1_comparing_to_a_bigger_Instant()
    {
        _now.CompareTo(_now.Add(TimeSpan.FromHours(1)))
            .Should().Be(-1);
    }

    [Fact]
    [Trait("Method", nameof(Instant.CompareTo))]
    public void Returns_1_comparing_to_smaller_Instant()
    {
        _now.CompareTo(_now.Add(TimeSpan.FromHours(-1)))
            .Should().Be(1);
    }

    [Fact]
    [Trait("Method", nameof(Instant.CompareTo))]
    public void Returns_0_comparing_to_an_equal_Instant()
    {
        _now.CompareTo(_now)
            .Should().Be(0);
    }

    [Fact]
    [Trait("Method", nameof(Instant.ToString))]
    public void Returns_this_representation()
    {
        const string timeZone = "America/New_York";
        var utc20240101At10Am = DateTimeOffset.Parse("2024-01-01 10:00:00 +00:00");

        new Instant(utc20240101At10Am, timeZone).ToString()
            .Should().Be($"1/1/2024 5:00 AM ( {timeZone} )");
    }

    [Fact]
    [Trait("Operator", ">")]
    public void Asserts_the_left_Instant_is_bigger_than_the_right_one()
    {
        (_now.Add(TimeSpan.FromHours(1)) > _now)
            .Should().BeTrue();
    }

    [Fact]
    [Trait("Operator", ">=")]
    public void Asserts_the_left_Instant_is_bigger_than_or_equal_to_the_right_one()
    {
        var right = _now;

        (_now.Add(TimeSpan.FromHours(1)) >= right)
            .Should().BeTrue();
        (_now >= right)
            .Should().BeTrue();
    }

    [Fact]
    [Trait("Operator", "<")]
    public void Asserts_the_left_Instant_is_smaller_than_the_right_one()
    {
        (_now.Add(TimeSpan.FromHours(-1)) < _now)
            .Should().BeTrue();
    }

    [Fact]
    [Trait("Operator", "<=")]
    public void Asserts_the_left_Instant_is_smaller_than_or_equal_to_the_right_one()
    {
        var right = _now;

        (_now.Add(TimeSpan.FromHours(-1)) <= right)
            .Should().BeTrue();
        (_now <= right)
            .Should().BeTrue();
    }
}
