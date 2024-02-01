﻿using FluentAssertions;
using Xunit;

namespace Mithril.Instants.Tests;

public sealed class InstantTests
{
    private readonly Instant _now = Instant.Now();
    private readonly DateTimeOffset _20240101InUtcPlus1 = new (2024, 1, 1, 0, 0, 0, TimeSpan.FromHours(1));

    [Fact]
    public void Throws_error_creating_from_non_Utc_date()
    {
        ((Func<Instant>)(() => new Instant(_20240101InUtcPlus1, string.Empty)))
            .Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Throws_error_creating_from_null_or_empty_time_zone()
    {
        ((Func<Instant>)(() => new Instant(DateTimeOffset.UtcNow, null!)))
            .Should().Throw<ArgumentException>();
        ((Func<Instant>)(() => new Instant(DateTimeOffset.UtcNow, string.Empty)))
            .Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Creates_new_with_added_time_span()
    {
        TimeSpan oneHour = TimeSpan.FromHours(1);

        _now.Add(oneHour).UtcValue
            .Should().Be(_now.UtcValue.Add(oneHour));
    }

    [Fact]
    public void Left_is_bigger_than_right()
    {
        (_now.Add(TimeSpan.FromHours(1)) > _now)
            .Should().BeTrue();
    }

    [Fact]
    public void Left_is_bigger_than_or_equal_to_right()
    {
        var right = _now;

        (_now.Add(TimeSpan.FromHours(1)) >= right)
            .Should().BeTrue();
        (_now >= right)
            .Should().BeTrue();
    }

    [Fact]
    public void Left_is_smaller_than_right()
    {
        (_now.Add(TimeSpan.FromHours(-1)) < _now)
            .Should().BeTrue();
    }

    [Fact]
    public void Left_is_smaller_than_or_equal_to_right()
    {
        var right = _now;

        (_now.Add(TimeSpan.FromHours(-1)) <= right)
            .Should().BeTrue();
        (_now <= right)
            .Should().BeTrue();
    }

    [Fact]
    public void Throws_error_comparing_to_null_or_non_Instant()
    {
        ((Func<int>)(() => _now.CompareTo(null)))
            .Should().Throw<InvalidCastException>();
        ((Func<int>)(() => _now.CompareTo("not an Instant")))
            .Should().Throw<InvalidCastException>();
    }

    [Fact]
    public void Gets_minus1_comparing_to_bigger()
    {
        _now.CompareTo(_now.Add(TimeSpan.FromHours(1)))
            .Should().Be(-1);
    }
}