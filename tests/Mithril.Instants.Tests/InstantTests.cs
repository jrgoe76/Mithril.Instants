using FluentAssertions;
using Xunit;

namespace Mithril.Instants.Tests;

public sealed class InstantTests
{
    private readonly Instant _now = Instant.Now();

    [Fact]
    [Trait("Method", "Constructor")]
    public void Throws_an_error_creating_an_instance_with_a_null_or_empty_time_zone()
    {
        ((Func<Instant>)(() => new Instant(DateTimeOffset.Now, null!)))
            .Should().Throw<ArgumentException>();
        ((Func<Instant>)(() => new Instant(DateTimeOffset.Now, string.Empty)))
            .Should().Throw<ArgumentException>();
    }

    [Fact]
    [Trait("Method", "Add")]
    public void Creates_an_instance_with_the_added_time_span()
    {
        TimeSpan oneHour = TimeSpan.FromHours(1);

        _now.Add(oneHour).UtcValue
            .Should().Be(_now.UtcValue.Add(oneHour));
    }

    [Fact]
    [Trait("Operator", ">")]
    public void Asserts_that_the_left_instance_is_bigger_than_the_right_one()
    {
        (_now.Add(TimeSpan.FromHours(1)) > _now)
            .Should().BeTrue();
    }

    [Fact]
    [Trait("Operator", ">=")]
    public void Asserts_that_the_left_instance_is_bigger_than_or_equal_to_the_right_one()
    {
        var right = _now;

        (_now.Add(TimeSpan.FromHours(1)) >= right)
            .Should().BeTrue();
        (_now >= right)
            .Should().BeTrue();
    }

    [Fact]
    [Trait("Operator", "<")]
    public void Asserts_that_the_left_instance_is_smaller_than_the_right_one()
    {
        (_now.Add(TimeSpan.FromHours(-1)) < _now)
            .Should().BeTrue();
    }

    [Fact]
    [Trait("Operator", "<=")]
    public void Asserts_that_the_left_instance_is_smaller_than_or_equal_to_the_right_one()
    {
        var right = _now;

        (_now.Add(TimeSpan.FromHours(-1)) <= right)
            .Should().BeTrue();
        (_now <= right)
            .Should().BeTrue();
    }

    [Fact]
    [Trait("Method", "CompareTo")]
    public void Throws_an_error_comparing_to_a_null_or_one_of_another_type()
    {
        ((Func<int>)(() => _now.CompareTo(null)))
            .Should().Throw<InvalidCastException>();
        ((Func<int>)(() => _now.CompareTo("not an Instant")))
            .Should().Throw<InvalidCastException>();
    }

    [Fact]
    [Trait("Method", "CompareTo")]
    public void Gets_a_minus_one_comparing_to_a_bigger_instance()
    {
        _now.CompareTo(_now.Add(TimeSpan.FromHours(1)))
            .Should().Be(-1);
    }

    [Fact]
    [Trait("Method", "CompareTo")]
    public void Gets_a_one_comparing_to_smaller_instance()
    {
        _now.CompareTo(_now.Add(TimeSpan.FromHours(-1)))
            .Should().Be(1);
    }

    [Fact]
    [Trait("Method", "CompareTo")]
    public void Gets_a_zero_comparing_to_an_equal_instance()
    {
        _now.CompareTo(_now)
            .Should().Be(0);
    }

    [Fact]
    [Trait("Method", "ToString")]
    public void Gets_the_instance_representation()
    {
        const string timeZone = "America/New_York";
        var utc20240101At10Am = DateTimeOffset.Parse("2024-01-01 10:00:00 +00:00");

        new Instant(utc20240101At10Am, timeZone).ToString()
            .Should().Be($"1/1/2024 5:00 AM ( {timeZone} )");
    }
}
