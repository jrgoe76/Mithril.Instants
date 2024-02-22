namespace Mithril.Instants;

internal sealed class InstantClock(
    TimeProvider timeProvider,
    IInstantFactory instantFactory) : IInstantClock
{
    public Instant Now
        => instantFactory.Create(timeProvider.GetUtcNow());
}