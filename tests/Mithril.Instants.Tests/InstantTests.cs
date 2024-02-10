using FluentAssertions;
using Xunit;

namespace Mithril.Instants.Tests;

public sealed class InstantTests
{
    private readonly Instant _now = Instant.Now();

    [Fact]
    [Trait("Method", "Constructor")]
    public void Throws_an_error_creating_an_Instant_with_a_null_or_empty_time_zone()
    {
        ((Func<Instant>)(() => new Instant(DateTimeOffset.Now, null!)))
            .Should().Throw<ArgumentException>();
        ((Func<Instant>)(() => new Instant(DateTimeOffset.Now, string.Empty)))
            .Should().Throw<ArgumentException>();
    }

    [Fact]
    [Trait("Method", "Add")]
    public void Creates_an_Instant_with_an_added_interval()
    {
        var oneHour = TimeSpan.FromHours(1);

        _now.Add(oneHour)
            .Should().Be(_now with { UtcValue = _now.UtcValue.Add(oneHour) });
    }

    [Fact]
    [Trait("Method", "Subtract(TimeSpan)")]
    public void Creates_an_Instant_with_a_subtracted_interval()
    {
        var oneHour = TimeSpan.FromHours(1);

        _now.Subtract(oneHour)
            .Should().Be(_now with { UtcValue = _now.UtcValue.Subtract(oneHour) });
    }

    [Fact]
    [Trait("Method", "Subtract(Instant)")]
    public void Returns_interval_between_this_and_another_Instant()
    {
        var oneHour = TimeSpan.FromHours(1);

        _now.Subtract(_now.Subtract(oneHour))
            .Should().Be(oneHour);
    }

    [Fact]
    [Trait("Method", "CompareTo")]
    public void Throws_an_error_comparing_to_a_null_or_no_Instant_type()
    {
        ((Func<int>)(() => _now.CompareTo(null)))
            .Should().Throw<InvalidCastException>();
        ((Func<int>)(() => _now.CompareTo("not an Instant")))
            .Should().Throw<InvalidCastException>();
    }

    [Fact]
    [Trait("Method", "CompareTo")]
    public void Returns_minus1_comparing_to_a_bigger_Instant()
    {
        _now.CompareTo(_now.Add(TimeSpan.FromHours(1)))
            .Should().Be(-1);
    }

    [Fact]
    [Trait("Method", "CompareTo")]
    public void Returns_1_comparing_to_smaller_Instant()
    {
        _now.CompareTo(_now.Add(TimeSpan.FromHours(-1)))
            .Should().Be(1);
    }

    [Fact]
    [Trait("Method", "CompareTo")]
    public void Returns_0_comparing_to_an_equal_Instant()
    {
        _now.CompareTo(_now)
            .Should().Be(0);
    }

    [Fact]
    [Trait("Method", "ToString")]
    public void Returns_the_Instant_representation()
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
